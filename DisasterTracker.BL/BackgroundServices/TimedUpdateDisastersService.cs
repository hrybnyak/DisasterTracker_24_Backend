using DisasterTracker.BL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.BL.BackgroundServices
{
    public class TimedUpdateDisastersService : BackgroundService
    {
        private readonly ILogger<TimedUpdateDisastersService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public TimedUpdateDisastersService(
            ILogger<TimedUpdateDisastersService> logger, 
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Update Disasters Service Hosted Service running.");
            await UpdateDisasters(stoppingToken);
        }

        private async Task UpdateDisasters(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var scopedProcessingService =
                        scope.ServiceProvider
                            .GetRequiredService<IDisasterCreationService>();

                    await scopedProcessingService.CreateOrEditDisasters(stoppingToken);
                }
                
                await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Timed Update Disasters Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
