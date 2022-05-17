using System.ComponentModel.DataAnnotations.Schema;

namespace DisasterTracker.Data
{
    public class BaseEntity
    {
        public Guid? Id { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
