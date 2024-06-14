using POS.Application.Commons.Bases;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Infraestructura.Commons.Bases.Request;
using POS.Infraestructura.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface IProviderApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProviderResponseDto>>> ListProvider(BaseFiltersRequest filters);
        Task<BaseResponse<ProviderResponseDto>> ProviderById(int providerId);
        Task<BaseResponse<bool>> RegisterProvider(ProviderRequestDto request);
        Task<BaseResponse<bool>> EditProvider(int providerId,ProviderRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProvider(int providerId);
    }
}
