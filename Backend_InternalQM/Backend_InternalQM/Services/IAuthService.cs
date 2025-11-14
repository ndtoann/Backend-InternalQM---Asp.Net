using Backend_InternalQM.Entities;
using Backend_InternalQM.Models;

namespace Backend_InternalQM.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginRequest request);
        Task<LoginResponse> Register(RegisterRequest request);
        Task<UserDto> GetUserById(long userId);
        string GenerateJwtToken(UserDto user);
    }
}
