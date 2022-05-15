using AutoMapper;
using DisasterTracker.Data.Event;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    public class EventStatisticsProfile : Profile
    {
        public EventStatisticsProfile()
        {
            CreateMap<Value, EventStatistics>();
        }
    }
}
