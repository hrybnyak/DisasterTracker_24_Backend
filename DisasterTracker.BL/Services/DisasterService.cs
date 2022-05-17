using AutoMapper;
using DisasterTracker.BL.Dtos;
using DisasterTracker.DataServices.Repository;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.BL.Services
{
    public class DisasterService : IDisasterService
    {
        private readonly ILogger<DisasterService> _logger;
        private readonly IDisasterRepository _disasterRepository;
        private readonly IMapper _mapper;

        public DisasterService(
            ILogger<DisasterService> logger, 
            IDisasterRepository eventRepository, 
            IMapper mapper)
        {
            _logger = logger;
            _disasterRepository = eventRepository;
            _mapper = mapper;
        }

        public List<DisasterDto> GetRelevantDisasters()
        {
            try
            {
                var entities = _disasterRepository.GetRelevantDisasters();
                return _mapper.Map<List<DisasterDto>>(entities);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public DisasterDto GetDisasterById(Guid id)
        {
            try
            {
                var entity = _disasterRepository.GetFullDisaster(id);
                return _mapper.Map<DisasterDto>(entity);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
