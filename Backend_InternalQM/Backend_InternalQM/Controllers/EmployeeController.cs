using Backend_InternalQM.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Backend_InternalQM.Controllers
{
    public class EmployeeController : BaseController
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(DatabaseContext context, ILogger<EmployeeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "ViewEmployee")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _context.Employee
                    .Where(e => e.DeleteAt == null)
                    .Select(e => new
                    {
                        e.Id,
                        e.EmployeeCode,
                        e.EmployeeName,
                        e.Avatar,
                        e.DateOfBirth,
                        e.Gender,
                        e.DepartmentId,
                        e.HireDate,
                        e.Position
                    })
                    .ToListAsync();

                return OkResponse(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy danh sách nhân viên: {ex.Message}");
                return ErrorResponse("Lỗi hệ thống", 500);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var employee = await _context.Employee
                    .FirstOrDefaultAsync(e => e.Id == id && e.DeleteAt == null);

                if (employee == null)
                    return ErrorResponse("Không tìm thấy nhân viên", 404);

                return OkResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi lấy thông tin nhân viên: {ex.Message}");
                return ErrorResponse("Lỗi hệ thống", 500);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] Employee request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return ErrorResponse("Dữ liệu không hợp lệ");

                var employee = new Employee
                {
                    EmployeeCode = request.EmployeeCode,
                    EmployeeName = request.EmployeeName,
                    Avatar = request.Avatar,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    DepartmentId = request.DepartmentId,
                    HireDate = request.HireDate,
                    Position = request.Position,
                    Note = request.Note,
                    CreatedAt = DateOnly.FromDateTime(DateTime.Now),
                    CreatedBy = GetUserName()
                };

                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Thêm nhân viên: {employee.EmployeeName} - {GetUserName()}");

                return OkResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi thêm nhân viên: {ex.Message}");
                return ErrorResponse("Lỗi hệ thống", 500);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(long id, [FromBody] Employee request)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);
                if (employee == null)
                    return ErrorResponse("Không tìm thấy nhân viên", 404);

                employee.EmployeeName = request.EmployeeName ?? employee.EmployeeName;
                employee.Avatar = request.Avatar ?? employee.Avatar;
                employee.DateOfBirth = request.DateOfBirth ?? employee.DateOfBirth;
                employee.Gender = request.Gender ?? employee.Gender;
                employee.DepartmentId = request.DepartmentId;
                employee.Position = request.Position ?? employee.Position;
                employee.Note = request.Note ?? employee.Note;
                employee.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
                employee.UpdatedBy = GetUserName();

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Cập nhật nhân viên: {employee.EmployeeName} - {GetUserName()}");

                return OkResponse(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi cập nhật nhân viên: {ex.Message}");
                return ErrorResponse("Lỗi hệ thống", 500);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);
                if (employee == null)
                    return ErrorResponse("Không tìm thấy nhân viên", 404);

                employee.DeleteAt = DateOnly.FromDateTime(DateTime.Now);
                employee.DeleteBy = GetUserName();

                await _context.SaveChangesAsync();

                _logger.LogInformation($"Xóa nhân viên: {employee.EmployeeName} - {GetUserName()}");

                return OkResponse(true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi xóa nhân viên: {ex.Message}");
                return ErrorResponse("Lỗi hệ thống", 500);
            }
        }
    }
}
