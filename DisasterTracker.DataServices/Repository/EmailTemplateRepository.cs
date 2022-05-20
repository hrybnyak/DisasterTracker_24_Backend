using DisasterTracker.Data.EmailTemplate;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.DataServices.Repository
{
    internal class EmailTemplateRepository : GenericRepository<EmailTemplate>, IEmailTemplateRepository
    {
        private readonly ILogger<EmailTemplateRepository> _logger;

        public EmailTemplateRepository(ApplicationDbContext context, 
            ILogger<EmailTemplateRepository> logger) 
            : base(context)
        {
            _logger = logger;
        }

        public EmailTemplate? GetEmailTemplateByName(string name)
        {
            try
            {
                return _entities.FirstOrDefault(et => et.Name == name);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
