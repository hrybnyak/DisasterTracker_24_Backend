using AutoMapper;
using DisasterTracker.Data.User;
using Lib.Net.Http.WebPush;

namespace DisasterTracker.BL.MapperConfiguration
{
    internal class PushSubscriptionProfile : Profile
    {
        public PushSubscriptionProfile()
        {
            CreateMap<PushSubscription, UserPushSubscription>()
                .ForMember(ups => ups.Endpoint, mc => mc.MapFrom(ps => ps.Endpoint))
                .ForMember(ups => ups.PushSubscriptionKeys, mc => mc.MapFrom(ps => ps.Keys.Select(pair => new PushSubscriptionKey
                {
                    Name = pair.Key,
                    Value = pair.Value
                })))
                .ReverseMap()
                .ForMember(ps => ps.Keys, mc => mc.MapFrom(ups => ups.PushSubscriptionKeys.ToDictionary(psk => psk.Name, psk => psk.Value)))
                .ForMember(ps => ps.Endpoint, mc => mc.MapFrom(ups => ups.Endpoint));
        }
    }
}
