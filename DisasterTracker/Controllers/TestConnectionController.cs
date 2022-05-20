using DisasterTracker.BL.Services;
using DisasterTracker.BL.Services.EmailNotification;
using DisasterTracker.BL.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DisasterTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestConnectionController : ControllerBase
    {
        private readonly IHubContext<DisasterNotificationHub> _hubContext;
        private readonly IUserService _userService;
        private readonly IDisasterService _disasterService;
        private readonly IMailNotificationService _mailNotificationService;

        public TestConnectionController(
            IHubContext<DisasterNotificationHub> hubContext, 
            IUserService userService, 
            IDisasterService disasterService, 
            IMailNotificationService mailNotificationService)
        {
            _hubContext = hubContext;
            _userService = userService;
            _disasterService = disasterService;
            _mailNotificationService = mailNotificationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TestConnection(string id)
        {
            try
            {
                await _hubContext.Clients.User(id).SendAsync("TestConnection", "The connection with user is successful");
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sendEmail/{userId}/{disasterId}")]
        public async Task<IActionResult> TestSendEmail(Guid userId, Guid disasterId)
        {
            try
            {
                var user =  _userService.GetUserById(userId);
                var disaster = _disasterService.GetDisasterById(disasterId);
                var userLocation = user.Locations.FirstOrDefault();
                await _mailNotificationService.SendNewDisasterEmail(user, userLocation, disaster);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
