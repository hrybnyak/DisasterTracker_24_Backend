namespace DisasterTracker.BL.Dtos
{
    public class UserLocationDto
    {
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int Distance { get; set; }
        public string? Title { get; set; }
        public string? Label { get; set; }
    }
}
