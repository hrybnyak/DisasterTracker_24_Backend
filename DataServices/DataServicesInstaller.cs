using DisasterTracker.DataServices.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DisasterTracker.DataServices
{
    public static class DataServicesInstaller
    {
        public static void AddDataServices(this IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("ApplicationDbConnectionString")));

            serviceCollection.AddScoped<IDisasterRepository, DisasterRepository>();
        }
    }
}
