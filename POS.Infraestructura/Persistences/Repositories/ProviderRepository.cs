using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infraestructura.Commons.Bases.Request;
using POS.Infraestructura.Commons.Bases.Response;
using POS.Infraestructura.Persistences.Context;
using POS.Infraestructura.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Infraestructura.Persistences.Repositories
{
    internal class ProviderRepository :GenericRepository<Provider>,IProviderRepository
    {

        public ProviderRepository(POSContext context) : base(context) 
        {
        }

        public async Task<BaseEntityResponse<Provider>> ListProvider(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Provider>();

            var provider = GetEntityQuery(x => x.AuditDeleteUser ==null && x.AuditDeleteDate==null)
                .Include(x=> x.DocumentType)
                .AsNoTracking();

            if(filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch(filters.NumFilter) 
                { 
                    case 1:
                        provider = provider.Where(x => x.Name!.Contains(filters.TextFilter));
                        break;
                    case 2:
                        provider = provider.Where(x => x.Email!.Contains(filters.TextFilter));
                        break;
                    case 3:
                        provider = provider.Where(x => x.DocumentNumber.Contains(filters.TextFilter));
                        break;

                }
            }

            if (filters.StateFilter is not null)
            {
                provider = provider.Where(x => x.State.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                provider = provider.Where(x => x.AuditCreateDate > Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate < Convert.ToDateTime(filters.EndDate).AddDays(1));
            }
            
           
            //ORDENAR POR SU ID DEL PROVEEDOR
            //hemos echo un cambio por el ProviderId a Id
            if (filters.Sort is null) filters.Sort = "Id";


            response.TotalRecords = await provider.CountAsync();
            response.Items = await Ordering(filters, provider, !(bool)filters.Download!).ToListAsync();
            return response;

        }
    }
}
