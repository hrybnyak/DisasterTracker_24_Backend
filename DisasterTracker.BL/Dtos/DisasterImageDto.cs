namespace DisasterTracker.BL.Dtos
{
    public class DisasterImageDto
    {
        public Guid Id { get; set; }
        public Guid DisasterId { get; set; }
        public string? MapImageAddress { get; set; }
        public string? InfrastructureMapImageAddress { get; set; }
        public string? OverviewMapImageAddress { get; set; }
    }
}
