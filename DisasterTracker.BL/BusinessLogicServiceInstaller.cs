using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DisasterTracker.BL.HttpClients;
using DisasterTracker.BL.MapperConfiguration;
using DisasterTracker.DataServices;
using DisasterTracker.BL.Services;
using DisasterTracker.BL.BackgroundServices;

namespace DisasterTracker.BL
{
    public static class BusinessLogicServiceInstaller
    {
        public static void AddBusinessLogicServices(this IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            serviceCollection.AddDataServices(configuration);

            serviceCollection.AddHttpClient<IPdcClient, PdcClient>();

            serviceCollection.AddAutoMapper(mc =>
            {
                mc.AddProfile<DisasterProfile>();
                mc.AddProfile<DisasterStatisticsProfile>();
                mc.AddProfile<DisasterDtosProfile>();
            });

            serviceCollection.AddScoped<IDisasterCreationService, DisasterCreationService>();
            serviceCollection.AddScoped<IDisasterService, DisasterService>();
            serviceCollection.AddHostedService<TimedUpdateDisastersService>();
        }
    }
}
