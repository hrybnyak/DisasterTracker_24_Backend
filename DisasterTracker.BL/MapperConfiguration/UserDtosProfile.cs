using AutoMapper;
using DisasterTracker.BL.Dtos;
using DisasterTracker.Data.User;

namespace DisasterTracker.BL.MapperConfiguration
{
    internal class UserDtosProfile : Profile
    {
        public UserDtosProfile()
        {
            CreateMap<UserLocation, UserLocationDto>()
                .ReverseMap()
                .ForMember(ul => ul.UserId, opt => opt.Ignore());

            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
