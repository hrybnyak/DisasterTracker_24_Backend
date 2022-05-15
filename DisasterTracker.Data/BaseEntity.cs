namespace DisasterTracker.Data
{
    public class BaseEntity
    {
        public Guid? Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
