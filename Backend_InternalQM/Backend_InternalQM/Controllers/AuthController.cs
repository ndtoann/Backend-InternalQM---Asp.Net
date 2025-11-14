using Backend_InternalQM.Models;
using Backend_InternalQM.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend_InternalQM.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse("Dữ liệu không hợp lệ", 400);

            var result = await _authService.Login(request);
            return result.Success ? Ok(result) : Unauthorized(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return ErrorResponse("Dữ liệu không hợp lệ", 400);

            var result = await _authService.Register(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            if (userId == 0)
                return ErrorResponse("Không tìm thấy thông tin người dùng", 401);

            var user = await _authService.GetUserById(userId);
            if (user == null)
                return ErrorResponse("Không tìm thấy người dùng", 404);

            return OkResponse(user);
        }
    }
}
