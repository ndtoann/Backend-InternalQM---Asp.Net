using Backend_InternalQM.Entities;
using Backend_InternalQM.Models;
using Backend_InternalQM.Modules.EmployeeModule;
using Backend_InternalQM.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend_InternalQM.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        private readonly DatabaseContext _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, DatabaseContext context, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "ViewEmployee")]
        public async Task<IActionResult> GetAll([FromQuery] EmployeeFilterDto filters)
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees(filters);
                return OkResponse(employees);
            }
            catch (Exception ex)
            {
                return ErrorResponse("Lỗi hệ thống", 500);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ViewEmployee")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                if (id <= 0 || id == null)
                {
                    return ErrorResponse("ID không hợp lệ", 400);
                }
                var employee = await _employeeService.GetEmployeeById(id);
                if (employee == null)
                    return ErrorResponse("Không tìm thấy nhân viên", 404);
                return OkResponse(employee);
            }
            catch (Exception ex)
            {
                return ErrorResponse("Lỗi hệ thống", 500);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] EmployeeDto req)
        {
            if (!ModelState.IsValid)
            {
                return ErrorResponse("Dữ liệu không hợp lệ", 400);
            }

            try
            {
                var currentUserId = User?.FindFirst(ClaimTypes.NameIdentifier).Value;
                var newEmployee = await _employeeService.Create(req, GetUserId());
                return OkResponse(newEmployee);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning($"Lỗi nghiệp vụ: {ex.Message}");
                return ErrorResponse(ex.Message, 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi hệ thống khi thêm nhân viên");
                return ErrorResponse("Lỗi hệ thống, vui lòng thử lại", 500);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(long id, [FromBody] EmployeeDto req)
        {
            if (!ModelState.IsValid)
            {
                return ErrorResponse("Dữ liệu không hợp lệ", 400);
            }
            if(id <= 0)
            {
                return ErrorResponse("Thông tin không hợp lệ", 400);
            }
            try
            {
                var currentUserId = User?.FindFirst(ClaimTypes.NameIdentifier).Value;
                var newEmployee = await _employeeService.Update(id, req, GetUserId());
                return OkResponse(newEmployee);
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning($"Lỗi nghiệp vụ: {ex.Message}");
                return ErrorResponse(ex.Message, 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi hệ thống khi thêm nhân viên");
                return ErrorResponse("Lỗi hệ thống, vui lòng thử lại", 500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0)
            {
                return ErrorResponse("Thông tin không hợp lệ", 400);
            }
            try
            {
                var deleteEmployee = await _employeeService.SoftDelete(id, GetUserId());
                return OkResponse("Xoá nhân viên thành công");
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning($"Lỗi nghiệp vụ: {ex.Message}");
                return ErrorResponse(ex.Message, 400);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi hệ thống khi thêm nhân viên");
                return ErrorResponse("Lỗi hệ thống, vui lòng thử lại", 500);
            }
        }
    }
}
