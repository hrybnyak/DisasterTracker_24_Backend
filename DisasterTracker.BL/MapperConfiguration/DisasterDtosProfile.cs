using AutoMapper;
using DisasterTracker.BL.Dtos;
using DisasterTracker.Data.Country;
using DisasterTracker.Data.Disaster;

namespace DisasterTracker.BL.MapperConfiguration
{
    internal class DisasterDtosProfile : Profile
    {
        public DisasterDtosProfile()
        {
            CreateMap<DisasterImage, DisasterImageDto>();
            CreateMap<DisasterStatistics, DisasterStatisticsDto>();
            CreateMap<Disaster, DisasterDto>();
            CreateMap<CountryDisaster, DisasterCountryDto>()
                .ForMember(dc => dc.PopulationAffected, opt => opt.MapFrom(cd => cd.AffectedPopulation))
                .ForMember(dc => dc.Name, opt => opt.MapFrom(cd => cd.Country.Name));
        }
    }
}
