using Backend_InternalQM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend_InternalQM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected long GetUserId()
        {
            var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out var userId))
                return 0;
            return userId;
        }

        protected string GetUserName()
        {
            return User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        }

        protected long GetEmployeeId()
        {
            var empIdClaim = User?.FindFirst("EmployeeId");
            if (empIdClaim == null || !long.TryParse(empIdClaim.Value, out var empId))
                return 0;
            return empId;
        }

        protected IActionResult OkResponse<T>(T data)
        {
            return Ok(new ApiResponse<T>
            {
                Status = true,
                Data = data
            });
        }

        protected IActionResult ErrorResponse(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, new ApiResponse<object>
            {
                Status = false,
                Data = null
            });
        }
    }
}
