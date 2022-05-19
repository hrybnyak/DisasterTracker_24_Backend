using AutoMapper;
using DisasterTracker.Data.Disaster;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    internal class DisasterProfile : Profile
    {
        public DisasterProfile()
        {
            CreateMap<HazardBean, Disaster>()
                .ForMember(e => e.Name, opt => opt.MapFrom(hb => hb.HazardName))
                .ForMember(e => e.AutoExpire, opt => opt.MapFrom(hb => hb.AutoExpire == "Y"))
                .ForMember(e => e.LastUpdateDate, opt => opt.MapFrom(hb => DateTime.SpecifyKind(hb.LastUpdate, DateTimeKind.Utc)))
                .ForMember(e => e.StartDate, opt => opt.MapFrom(hb => DateTime.SpecifyKind(hb.StartDate, DateTimeKind.Utc)))
                .ForMember(e => e.EndDate, opt => opt.MapFrom(hb => DateTime.SpecifyKind(hb.EndDate, DateTimeKind.Utc)))
                .ForMember(e => e.Type, opt => opt.MapFrom(new DisasterTypeResolver()))
                .ForMember(e => e.Severity, opt => opt.MapFrom(new DisasterSeverityResolver()));
        }
    }
}
