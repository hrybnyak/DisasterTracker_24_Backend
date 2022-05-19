using DisasterTracker.BL.Dtos;
using Google.Apis.Auth;

namespace DisasterTracker.BL.Services
{
    public interface IUserService
    {
        UserDto? GetUserById(Guid id);
        Task<UserDto?> UpdateUser(Guid id, UserDto userDto);
        Task<UserDto?> LoginUser(GoogleJsonWebSignature.Payload googlePayload);
    }
}
