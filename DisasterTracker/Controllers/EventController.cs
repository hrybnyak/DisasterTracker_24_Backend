using AutoMapper;
using DisasterTracker.BL.HttpClients;
using DisasterTracker.Data.Event;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisasterTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IPdcClient _pdcClient;
        private readonly IMapper _mapper;

        public EventController(IPdcClient pdcClient, IMapper mapper)
        {
            _pdcClient = pdcClient;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var hazardBeans = (await _pdcClient.GetHazardBeans()).HazardBean;
            return Ok(_mapper.Map<List<Event>>(hazardBeans));
        }

        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(Guid id)
        {
            var eventHistory = await _pdcClient.GetEventHistory(id);
            return Ok(eventHistory);
        }

        [HttpGet("{id}/update/{updateId}")]
        public async Task<IActionResult> GetHistory(Guid id, string updateId)
        {
            var eventHistory = await _pdcClient.GetEventSpecifics(id, updateId);
            return Ok(_mapper.Map<EventStatistics>(eventHistory.Aims.Data[0].Values));
        }
    }
}
