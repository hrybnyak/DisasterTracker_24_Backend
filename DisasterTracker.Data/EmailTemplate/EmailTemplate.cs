namespace DisasterTracker.Data.EmailTemplate
{
    public class EmailTemplate : BaseEntity
    {
        public string Name { get; set; }
        public string HtmlTemplate { get; set; }
    }
}
