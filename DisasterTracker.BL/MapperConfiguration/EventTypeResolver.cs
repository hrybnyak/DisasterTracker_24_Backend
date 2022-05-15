using AutoMapper;
using DisasterTracker.BL.Extensions;
using DisasterTracker.Data.Event;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    public class EventTypeResolver : IValueResolver<HazardBean, Event, EventType>
    {
        public EventType Resolve(HazardBean source, Event destination, EventType destMember, ResolutionContext context)
        {
            var eventTypeStringFormatted = source.TypeId.FirstCharToUpper();
            return Enum.TryParse(eventTypeStringFormatted, out EventType eventType) ? eventType : EventType.Unknown;
        }
    }
}
