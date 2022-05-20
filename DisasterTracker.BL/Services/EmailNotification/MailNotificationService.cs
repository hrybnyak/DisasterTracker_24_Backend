using DisasterTracker.BL.Dtos;
using DisasterTracker.DataServices.Repository;
using Microsoft.Extensions.Logging;
namespace DisasterTracker.BL.Services.EmailNotification
{
    public class MailNotificationService : IMailNotificationService
    {
        private readonly ILogger<MailNotificationService> _logger;
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IMailService _mailService;

        public MailNotificationService(
            ILogger<MailNotificationService> logger,
            IEmailTemplateRepository emailTemplateRepository, 
            IMailService mailService)
        {
            _logger = logger;
            _emailTemplateRepository = emailTemplateRepository;
            _mailService = mailService;
        }

        public async Task SendNewDisasterEmail(UserDto userDto, UserLocationDto userLocation, DisasterDto disasterDto)
        {
            try
            {
                var request = new MailRequest
                {
                    Body = BuildEmailBody(userLocation, disasterDto),
                    ToEmail = userDto.Email,
                    Subject = "[no-reply] New Disaster Alert"
                };
                await _mailService.SendEmailAsync(request);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task SendUpdatedDisasterEmail(UserDto userDto, UserLocationDto userLocation, DisasterDto disasterDto)
        {
            try
            {
                var request = new MailRequest
                {
                    Body = BuildEmailBody(userLocation, disasterDto),
                    ToEmail = userDto.Email,
                    Subject = "[no-reply] Disaster Updated Alert"
                };
                await _mailService.SendEmailAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private string BuildEmailBody(UserLocationDto userLocation, DisasterDto disasterDto)
        {
            var template = _emailTemplateRepository.GetEmailTemplateByName(DisasterTemplateConstants.DisasterTemplateName);

            var disasterUrl = DisasterTemplateConstants.DisasterUrlTemplate.Replace("{id}", disasterDto.Id.ToString());
            var disasterDescription = string.Concat(disasterDto.Description.AsSpan(0, 97), "...");

            var body = template.HtmlTemplate.Replace(DisasterTemplateConstants.DisasterUrlKey, disasterUrl)
                .Replace(DisasterTemplateConstants.DisasterMapUrlKey, disasterDto.DisasterImage?.MapImageAddress)
                .Replace(DisasterTemplateConstants.DisasterDescriptionKey, disasterDescription)
                .Replace(DisasterTemplateConstants.LocationLabelKey, userLocation.Label ?? userLocation.Title ?? $"{userLocation.Latitude}{userLocation.Longitude}")
                .Replace(DisasterTemplateConstants.DisasterNameKey, disasterDto.Name)
                .Replace(DisasterTemplateConstants.DisasterUrlKey, disasterUrl);

            return body;
        }
    }
}
