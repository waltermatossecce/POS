using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchDog;
using WatchDog.src.Enums;

namespace POS.Application.Extensions.WatchDog
{
    public static class WatchDogExtensions
    {
        public static IServiceCollection AddWatchDog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWatchDogServices(options =>
            {
                options.SetExternalDbConnString = configuration.GetConnectionString("POSConection");
                //estamos cambiando de base de datos a sql server el watchdog traera siempre 3 tablas
                options.DbDriverOption = WatchDogDbDriverEnum.MSSQL;

                // Eliminar los logs automáticamente
                options.IsAutoClear = true;
                // Eliminar los logs diariamente
                options.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;
            });
            return services;
        }
    }
}
