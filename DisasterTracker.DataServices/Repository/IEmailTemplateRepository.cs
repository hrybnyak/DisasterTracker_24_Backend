using DisasterTracker.Data.EmailTemplate;

namespace DisasterTracker.DataServices.Repository
{
    public interface IEmailTemplateRepository : IGenericRepository<EmailTemplate>
    {
        EmailTemplate? GetEmailTemplateByName(string name);
    }
}
