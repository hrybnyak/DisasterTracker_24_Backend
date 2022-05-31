using DisasterTracker.BL.Dtos;
using DisasterTracker.BL.Services;
using DisasterTracker.Filters;
using Google.Apis.Auth;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Mvc;

namespace DisasterTracker.Controllers
{
    [Route("api/v1/[controller]")]
    [GoogleAuthorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IPushSubscriptionService _pushSubscriptionService;
        private readonly IConfiguration _configuration;

        public UserController(
            ILogger<UserController> logger, 
            IUserService userService,
            IConfiguration configuration,
            IPushSubscriptionService pushSubscriptionService)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
            _pushSubscriptionService = pushSubscriptionService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpGet("login")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login()
        {
            try
            {
                var googleJsonWebSignaturePayload = await GetGoogleAuthentificationPayload();
                var user = await _userService.LoginUser(googleJsonWebSignaturePayload);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserDto updateUserDto)
        {
            try
            {
                var user = await _userService.UpdateUser(id, updateUserDto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpPost("{id}/pushSubscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSubscriptionToUser(Guid id, [FromBody]PushSubscription pushSubscription)
        {
            try
            {
                await _pushSubscriptionService.AddPushSubscriptionToUser(id, pushSubscription);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        [HttpDelete("{id}/pushSubscription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSubscription(Guid id)
        {
            try
            {
                await _pushSubscriptionService.DeleteSubscription(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private async Task<GoogleJsonWebSignature.Payload> GetGoogleAuthentificationPayload()
        {
            var token = HttpContext.Request.Headers["Authorization"]
                .ToString()
                .Remove(0, 7);

            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string>() { _configuration["Authentication:Google:ClientId"] }
            };

            return await GoogleJsonWebSignature.ValidateAsync(token, settings);
        }
    }
}
