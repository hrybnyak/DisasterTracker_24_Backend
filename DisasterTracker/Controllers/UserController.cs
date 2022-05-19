using DisasterTracker.BL.Dtos;
using DisasterTracker.BL.Services;
using DisasterTracker.Filters;
using Google.Apis.Auth;
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
        private readonly IConfiguration _configuration;

        public UserController(
            ILogger<UserController> logger, 
            IUserService userService,
            IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
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
