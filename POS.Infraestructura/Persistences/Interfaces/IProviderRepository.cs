using POS.Domain.Entities;
using POS.Infraestructura.Commons.Bases.Request;
using POS.Infraestructura.Commons.Bases.Response;

namespace POS.Infraestructura.Persistences.Interfaces
{
    public interface IProviderRepository :IGenericRepository<Provider>
    { 
        Task<BaseEntityResponse<Provider>> ListProvider(BaseFiltersRequest filters);
    }
}
