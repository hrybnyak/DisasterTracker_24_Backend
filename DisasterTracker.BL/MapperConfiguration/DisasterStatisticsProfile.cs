using AutoMapper;
using DisasterTracker.Data.Disaster;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    public class DisasterStatisticsProfile : Profile
    {
        public DisasterStatisticsProfile()
        {
            CreateMap<Value, DisasterStatistics>();
        }
    }
}
