using AutoMapper;
using DisasterTracker.Data.Disaster;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    public class DisasterSeverityResolver : IValueResolver<HazardBean, Disaster, Severity>
    {
        Severity IValueResolver<HazardBean, Disaster, Severity>.Resolve(HazardBean source, Disaster destination, Severity destMember, ResolutionContext context)
        {
            return Enum.TryParse(source.SeverityId, true, out Severity severity) ? severity : Severity.Unknown;
        }
    }
}
