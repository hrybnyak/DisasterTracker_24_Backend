using AutoMapper;
using DisasterTracker.BL.Extensions;
using DisasterTracker.Data.Event;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    public class EventSeverityResolver : IValueResolver<HazardBean, Event, Severity>
    {
        Severity IValueResolver<HazardBean, Event, Severity>.Resolve(HazardBean source, Event destination, Severity destMember, ResolutionContext context)
        {
            var eventSeverityStringFormatted = source.SeverityId.FirstCharToUpper();
            return Enum.TryParse(eventSeverityStringFormatted, out Severity severity) ? severity : Severity.Unknown;
        }
    }
}
