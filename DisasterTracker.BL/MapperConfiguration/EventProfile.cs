using AutoMapper;
using DisasterTracker.Data.Event;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<HazardBean, Event>()
                .ForMember(e => e.Name, opt => opt.MapFrom(hb => hb.HazardName))
                .ForMember(e => e.AutoExpire, opt => opt.MapFrom(hb => hb.AutoExpire == "Y"))
                .ForMember(e => e.LastUpdateDate, opt => opt.MapFrom(hb => hb.LastUpdate))
                .ForMember(e => e.Type, opt => opt.MapFrom(new EventTypeResolver()))
                .ForMember(e => e.Severity, opt => opt.MapFrom(new EventSeverityResolver()));
        }
    }
}
