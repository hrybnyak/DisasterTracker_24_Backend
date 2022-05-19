using AutoMapper;
using DisasterTracker.Data.Disaster;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    internal class DisasterStatisticsProfile : Profile
    {
        public DisasterStatisticsProfile()
        {
            CreateMap<Value, DisasterStatistics>();
        }
    }
}
