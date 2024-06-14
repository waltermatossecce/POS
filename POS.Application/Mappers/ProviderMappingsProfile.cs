using AutoMapper;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Dtos.Provider.Response;
using POS.Domain.Entities;
using POS.Infraestructura.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class ProviderMappingsProfile : Profile
    {
        public ProviderMappingsProfile()
        {
            //lista de proveedores
            CreateMap<Provider, ProviderResponseDto>()
                .ForMember(x => x.ProviderId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateProvider, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ForMember(x => x.DocumentType,x => x.MapFrom(y =>y.DocumentType.Abbreviation))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Provider>, BaseEntityResponse<ProviderResponseDto>>()
                .ReverseMap();
            //registrar proveedor
            CreateMap<ProviderRequestDto, Provider>();

        
        }
    }
}
