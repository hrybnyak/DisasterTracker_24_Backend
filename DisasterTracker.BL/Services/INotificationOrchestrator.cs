namespace DisasterTracker.BL.Services
{
    internal interface INotificationOrchestrator
    {
        Task CoordinateUserNotification(CancellationToken stoppingToken);
    }
}
