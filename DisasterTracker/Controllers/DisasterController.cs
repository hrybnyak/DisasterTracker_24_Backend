using DisasterTracker.BL.Dtos;
using DisasterTracker.BL.Services;
using DisasterTracker.Data.Disaster;
using Microsoft.AspNetCore.Mvc;

namespace DisasterTracker.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DisasterController : ControllerBase
    {
        private readonly IDisasterService _disasterService;
        private readonly ILogger<DisasterController> _logger;

        public DisasterController(
            IDisasterService disasterService, 
            ILogger<DisasterController> logger)
        {
            _disasterService = disasterService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DisasterDto>), StatusCodes.Status200OK)]
        public IActionResult GetDisasters()
        {
            try
            {
                var disasters = _disasterService.GetRelevantDisasters();
                return Ok(disasters);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DisasterDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDisasterById(Guid id)
        {
            try
            {
                var disaster = _disasterService.GetDisasterById(id);
                if (disaster == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(disaster);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

    }
}
