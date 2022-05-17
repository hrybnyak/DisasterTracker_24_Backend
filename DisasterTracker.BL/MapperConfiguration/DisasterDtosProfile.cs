using AutoMapper;
using DisasterTracker.BL.Dtos;
using DisasterTracker.Data.Disaster;

namespace DisasterTracker.BL.MapperConfiguration
{
    public class DisasterDtosProfile : Profile
    {
        public DisasterDtosProfile()
        {
            CreateMap<DisasterImage, DisasterImageDto>();
            CreateMap<DisasterStatistics, DisasterStatisticsDto>();
            CreateMap<Disaster, DisasterDto>();
        }
    }
}
