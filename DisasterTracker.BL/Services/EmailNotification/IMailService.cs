namespace DisasterTracker.BL.Services.EmailNotification
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
