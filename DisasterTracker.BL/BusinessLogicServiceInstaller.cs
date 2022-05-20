using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DisasterTracker.BL.HttpClients;
using DisasterTracker.BL.MapperConfiguration;
using DisasterTracker.DataServices;
using DisasterTracker.BL.Services;
using DisasterTracker.BL.BackgroundServices;
using Microsoft.AspNetCore.SignalR;
using DisasterTracker.BL.SignalR;
using DisasterTracker.BL.Services.EmailNotification;

namespace DisasterTracker.BL
{
    public static class BusinessLogicServiceInstaller
    {
        public static void AddBusinessLogicServices(this IServiceCollection serviceCollection, IConfigurationRoot configuration)
        {
            serviceCollection.AddSignalR();
            serviceCollection.AddSingleton<IUserIdProvider, UserIdProvider>();

            serviceCollection.AddDataServices(configuration);

            serviceCollection.AddHttpClient<IPdcClient, PdcClient>();

            serviceCollection.AddAutoMapper(mc =>
            {
                mc.AddProfile<DisasterProfile>();
                mc.AddProfile<DisasterStatisticsProfile>();
                mc.AddProfile<DisasterDtosProfile>();
                mc.AddProfile<UserDtosProfile>();
            });

            serviceCollection.AddScoped<IDisasterRetrievalService, DisasterRetrievalService>();
            serviceCollection.AddScoped<IDisasterService, DisasterService>();
            serviceCollection.AddScoped<IUserService, UserService>();

            serviceCollection.AddScoped<ISignalRNotificationService, SignalRNotificationService>();
            serviceCollection.AddScoped<IMailService, MailService>();
            serviceCollection.AddScoped<IMailNotificationService, MailNotificationService>();

            serviceCollection.AddScoped<INotificationService, NotificationService>();
            serviceCollection.AddScoped<INotificationOrchestrator, NotificationOrchestrator>();

            serviceCollection.AddHostedService<TimedUpdateOnDisastersService>();
        }
    }
}
