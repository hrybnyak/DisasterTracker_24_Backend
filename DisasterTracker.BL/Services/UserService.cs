using AutoMapper;
using DisasterTracker.BL.Dtos;
using DisasterTracker.Data.User;
using DisasterTracker.DataServices.Repository;
using Google.Apis.Auth;
using Microsoft.Extensions.Logging;

namespace DisasterTracker.BL.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public UserDto? GetUserById(Guid id)
        {
            try
            {
                var entity = _userRepository.GetFullUser(id);
                return _mapper.Map<UserDto?>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<UserDto?> LoginUser(GoogleJsonWebSignature.Payload googlePayload)
        {
            try
            {
                var entity = _userRepository.GetFullUserByEmail(googlePayload.Email);
                if (entity == null)
                {
                    return await CreateUser(new CreateUserDto
                    {
                        Email = googlePayload.Email,
                        UserName = googlePayload.Name
                    });
                }
                else
                {
                    return _mapper.Map<UserDto?>(entity);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<UserDto?> UpdateUser(Guid id, UserDto userDto)
        {
            try
            {
                var entityMapped = _mapper.Map<User>(userDto);

                var entity = _userRepository.GetFullUser(id);
                if (entity == null)
                {
                    return null;
                }

                entity.RecievePushNotifications = entityMapped.RecievePushNotifications;
                entity.ReceiveEmails = entityMapped.ReceiveEmails;
                entity.Email = entityMapped.Email;
                UpdateUserLocations(entity, entityMapped);

                await _userRepository.Update(entity, true);
                return GetUserById(entity.Id!.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private User UpdateUserLocations(User entity, User entityMapped)
        {
            foreach (var location in entityMapped.Locations)
            {
                if (location.Id != null)
                {
                    var entityLocation = entity.Locations.FirstOrDefault(l => l.Id == location.Id);
                    if (entityLocation != null)
                    {
                        entityLocation.Distance = location.Distance;
                        entityLocation.Latitude = location.Latitude;
                        entityLocation.Longitude = location.Longitude;
                    }
                    else
                    {
                        location.Id = null;
                        entity.Locations.Add(location);
                    }
                }
                else
                {
                    entity.Locations.Add(location);
                }
            }

            entity.Locations.RemoveAll(el => entityMapped.Locations.FirstOrDefault(l => l.Id == el.Id) == null);

            return entity;
        }

        private async Task<UserDto> CreateUser(CreateUserDto createUserDto)
        {
            try
            {
                var entity = new User
                {
                    Id = Guid.NewGuid(),
                    Email = createUserDto.Email,
                    UserName = createUserDto.UserName,
                    RecievePushNotifications = false,
                    ReceiveEmails = false
                };
                await _userRepository.Insert(entity, true);
                return GetUserById(entity.Id.Value)!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}
