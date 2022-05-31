using DisasterTracker.BL.Services;
using DisasterTracker.BL.Services.EmailNotification;
using DisasterTracker.BL.Services.PushNotification;
using DisasterTracker.BL.Services.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace DisasterTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDisasterService _disasterService;
        private readonly IMailNotificationService _mailNotificationService;
        private readonly ISignalRNotificationService _signalRNotificationService;
        private readonly IPushNotificationService _pushNotificationService;

        public TestController(
            IUserService userService, 
            IDisasterService disasterService, 
            IMailNotificationService mailNotificationService,
            ISignalRNotificationService signalRNotificationService,
            IPushNotificationService pushNotificationService)
        {
            _userService = userService;
            _disasterService = disasterService;
            _mailNotificationService = mailNotificationService;
            _signalRNotificationService = signalRNotificationService;
            _pushNotificationService = pushNotificationService;
        }

        [HttpGet("sendSocketPush/{disasterId}/created/{userId}")]
        public async Task<IActionResult> TestCreatedPushNotification(Guid disasterId, Guid userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);
                var disaster = _disasterService.GetDisasterById(disasterId);
                var userLocation = user.Locations.FirstOrDefault();
                await _signalRNotificationService.NotifyAboutNewDisaster(user, userLocation, disaster);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sendSocketPush/{disasterId}/updated/{userId}")]
        public async Task<IActionResult> TestUpdatedPushNotification(Guid disasterId, Guid userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);
                var disaster = _disasterService.GetDisasterById(disasterId);
                var userLocation = user.Locations.FirstOrDefault();
                await _signalRNotificationService.NotifyAboutUpdatedDisaster(user, userLocation, disaster);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sendPush/{disasterId}/{userId}")]
        public async Task<IActionResult> TestPushNotification(Guid disasterId, Guid userId)
        {
            try
            {
                var user = _userService.GetUserById(userId);
                var disaster = _disasterService.GetDisasterById(disasterId);
                var userLocation = user.Locations.FirstOrDefault();
                await _pushNotificationService.NotifyAboutDisaster(user, userLocation, disaster);
                return Ok();
            }
            catch (Exception ex)
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
