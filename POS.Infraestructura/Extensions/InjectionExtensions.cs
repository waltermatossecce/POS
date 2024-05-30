using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infraestructura.Persistences.Context;
using POS.Infraestructura.Persistences.Interfaces;
using POS.Infraestructura.Persistences.Repositories;
namespace POS.Infraestructura.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfraestructura(this IServiceCollection services,IConfiguration configuration)
        {
            var assembly = typeof(POSContext).Assembly.FullName;

            services.AddDbContext<POSContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("POSConection"), b => b.MigrationsAssembly(assembly)),ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWorK, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
