using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DisasterTracker.BL.HttpClients;
using DisasterTracker.BL.MapperConfiguration;
using DisasterTracker.DataServices;

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
                mc.AddProfile<EventProfile>();
                mc.AddProfile<EventStatisticsProfile>();
            });
        }
    }
}
