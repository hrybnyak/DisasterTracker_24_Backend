using AutoMapper;
using DisasterTracker.Data.Disaster;
using DisasterTracker.PdcApiModels;

namespace DisasterTracker.BL.MapperConfiguration
{
    public class DisasterTypeResolver : IValueResolver<HazardBean, Disaster, DisasterType>
    {
        public DisasterType Resolve(HazardBean source, Disaster destination, DisasterType destMember, ResolutionContext context)
        {
            return Enum.TryParse(source.TypeId, true, out DisasterType eventType) ? eventType : DisasterType.Unknown;
        }
    }
}
