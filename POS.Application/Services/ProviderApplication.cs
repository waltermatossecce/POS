using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infraestructura.Commons.Bases.Request;
using POS.Infraestructura.Commons.Bases.Response;
using POS.Infraestructura.Persistences.Interfaces;
using POS.Utilities.Static;
using WatchDog;

namespace POS.Application.Services
{
    public class ProviderApplication : IProviderApplication
    {
        private readonly IUnitOfWorK _unitOfWork;
        private readonly IMapper _mapper;

        public ProviderApplication(IUnitOfWorK unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<BaseEntityResponse<ProviderResponseDto>>> ListProvider(BaseFiltersRequest filters)
        {
            return await HandleRequestAsync(async () =>
            {
                var providers = await _unitOfWork.Provider.ListProvider(filters);
                return _mapper.Map<BaseEntityResponse<ProviderResponseDto>>(providers);
            });
        }

        public async Task<BaseResponse<ProviderResponseDto>> ProviderById(int providerId)
        {
            return await HandleRequestAsync(async () =>
            {
                var provider = await _unitOfWork.Provider.GetByIdAsync(providerId);
                return _mapper.Map<ProviderResponseDto>(provider);
            });
        }

        public async Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto request)
        {
            return await HandleRequestAsync(async () =>
            {
                var provider = _mapper.Map<Provider>(request);
                return await _unitOfWork.Provider.RegisterAsync(provider);
            }, ReplyMessage.MESSAGE_SAVE);
        }

        public async Task<BaseResponse<bool>> EditProvider(int providerId, ProviderRequestDto requestDto)
        {
            return await HandleRequestAsync(async () =>
            {
                var providerEdit = await _unitOfWork.Provider.GetByIdAsync(providerId);
                if (providerEdit == null) 
                    return false;

                _mapper.Map(requestDto, providerEdit);
                providerEdit.Id = providerId;

                return await _unitOfWork.Provider.EditAsync(providerEdit);
            }, ReplyMessage.MESSAGE_UPDATE);
        }

        public async Task<BaseResponse<bool>> RemoveProvider(int providerId)
        {
            return await HandleRequestAsync(async () =>
            {
                var providerExists = await _unitOfWork.Provider.GetByIdAsync(providerId);
                if (providerExists == null) return false;

                return await _unitOfWork.Provider.RemoveAsync(providerId);
            }, ReplyMessage.MESSAGE_DELETE);
        }

        private async Task<BaseResponse<T>> HandleRequestAsync<T>(Func<Task<T>> func, string successMessage = ReplyMessage.MESSAGE_QUERY)
        {
            var response = new BaseResponse<T>();
            try
            {
                var result = await func();
                response.IsSuccess = result != null;
                response.Data = result;
                response.Message = response.IsSuccess ? successMessage : ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            catch (Exception ex)
            {
                LogException(response, ex);
            }
            return response;
        }

        private void LogException<T>(BaseResponse<T> response, Exception ex)
        {
            response.IsSuccess = false;
            response.Message = ReplyMessage.MESSAGE_EXCEPTION;
            WatchLogger.Log(ex.Message);
        }
    }
}
