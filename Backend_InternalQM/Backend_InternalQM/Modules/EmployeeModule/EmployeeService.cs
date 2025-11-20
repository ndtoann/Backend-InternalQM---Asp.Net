using Backend_InternalQM.Entities;
using Backend_InternalQM.Models;
using Backend_InternalQM.Utils;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend_InternalQM.Modules.EmployeeModule
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<EmployeeService> _logger;
        private DateOnly CURRENT_DATE = DateOnly.FromDateTime(DateTime.UtcNow);

        public EmployeeService(DatabaseContext context, ILogger<EmployeeService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<EmployeeDto>> GetAllEmployees(EmployeeFilterDto filters)
        {
            try
            {
                var query = from e in _context.Employee
                            join d in _context.Department on e.DepartmentId equals d.Id
                            where e.DeleteBy == null
                            select new EmployeeDto
                            {
                                Id = e.Id,
                                EmployeeCode = e.EmployeeCode,
                                EmployeeName = e.EmployeeName,
                                Avatar = e.Avatar,
                                DateOfBirth = e.DateOfBirth,
                                Gender = e.Gender,
                                DepartmentId = e.DepartmentId,
                                DepartmentName = d.DepartmentName,
                                HireDate = e.HireDate,
                                Position = e.Position,
                                Note = e.Note,
                            };

                if (!string.IsNullOrEmpty(filters.Key))
                {
                    query = query.Where(e => (e.EmployeeCode.ToLower().Contains(filters.Key.ToLower())) ||
                                            (e.EmployeeName.ToLower().Contains(filters.Key.ToLower())));
                }
                if (filters.DepartmentId.HasValue && filters.DepartmentId.Value > 0)
                {
                    query = query.Where(e => e.DepartmentId == filters.DepartmentId.Value);
                }

                var employees = await query.ToListAsync();
                return employees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách nhân viên");
                throw;
            }
        }

        public async Task<EmployeeDto> GetEmployeeById(long id)
        {
            try
            {
                var employee = await (from e in _context.Employee
                                       join d in _context.Department on e.DepartmentId equals d.Id
                                       where e.DeleteBy == null && e.Id == id
                                       select new EmployeeDto
                                       {
                                           Id = e.Id,
                                           EmployeeCode = e.EmployeeCode,
                                           EmployeeName = e.EmployeeName,
                                           Avatar = e.Avatar,
                                           DateOfBirth = e.DateOfBirth,
                                           Gender = e.Gender,
                                           DepartmentName = d.DepartmentName,
                                           HireDate = e.HireDate,
                                           Position = e.Position,
                                           Note = e.Note,
                                       })
                                        .FirstOrDefaultAsync();
                if (employee == null)
                {
                    _logger.LogError("Không tìm thấy nhân viên với ID: {Id}", id);
                    return null;
                }
                return employee;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy thông tin nhân viên");
                return null;
            }
        }

        public async Task<Employee> Create(EmployeeDto req, long createdBy)
        {
            if(ExistEmployee(req.EmployeeCode))
            {
                _logger.LogError("Lỗi: Mã NV đã tồn tại");
                throw new BusinessException($"Mã nhân viên '{req.EmployeeCode}' đã tồn tại.");
            }
            if (ExistDepartment(req.DepartmentId))
            {
                _logger.LogError("Lỗi: Bộ phận không hợp lệ");
                throw new BusinessException($"Bộ phận không hợp lệ");
            }

            var employee = new Employee
            {
                EmployeeCode = req.EmployeeCode,
                EmployeeName = req.EmployeeName,
                Avatar = req.Avatar,
                DateOfBirth = req.DateOfBirth,
                Gender = req.Gender,
                DepartmentId = req.DepartmentId,
                HireDate = req.HireDate,
                Position = req.Position,
                Note = req.Note,
                CreatedBy = createdBy,
                CreatedAt = CURRENT_DATE
            };
            try
            {
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi kết nối cơ sở dữ liệu");
                throw new BusinessException("Lỗi khi lưu dữ liệu");
            }
            return employee;
        }

        public async Task<Employee> Update(long id, EmployeeDto req, long updatedBy)
        {
            if (ExistEmployee(req.EmployeeCode, id))
            {
                _logger.LogError("Lỗi: Mã NV đã tồn tại");
                throw new BusinessException($"Mã nhân viên '{req.EmployeeCode}' đã tồn tại.");
            }
            if (ExistDepartment(req.DepartmentId))
            {
                _logger.LogError("Lỗi: Bộ phận không hợp lệ");
                throw new BusinessException($"Bộ phận không hợp lệ");
            }

            var existingEmployee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == id && e.DeleteBy == null);
            if(existingEmployee == null)
            {
                _logger.LogError("Lỗi: Không tìm thấy nhân viên");
                throw new BusinessException("Nhân viên không tồn tại");
            }

            existingEmployee.EmployeeCode = req.EmployeeCode;
            existingEmployee.EmployeeName = req.EmployeeName;
            existingEmployee.Avatar = req.Avatar;
            existingEmployee.DateOfBirth = req.DateOfBirth;
            existingEmployee.Gender = req.Gender;
            existingEmployee.DepartmentId = req.DepartmentId;
            existingEmployee.HireDate = req.HireDate;
            existingEmployee.Position = req.Position;
            existingEmployee.Note = req.Note;
            existingEmployee.UpdatedBy = updatedBy;
            existingEmployee.UpdatedAt = CURRENT_DATE;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi kết nối cơ sở dữ liệu");
                throw new BusinessException("Lỗi khi lưu dữ liệu");
            }
            return existingEmployee;
        }

        public async Task<bool> SoftDelete(long id, long deletedBy)
        {
            var existingEmployee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == id && e.DeleteBy == null);
            if (existingEmployee == null)
            {
                _logger.LogError("Lỗi: Không tìm thấy nhân viên");
                throw new BusinessException("Nhân viên không tồn tại");
            }
            existingEmployee.DeleteBy = deletedBy;
            existingEmployee.DeleteAt = CURRENT_DATE;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi kết nối cơ sở dữ liệu");
                throw new BusinessException("Lỗi khi lưu dữ liệu");
            }
        }

        private bool ExistEmployee(string emplCode, long currentId = 0)
        {
            var isCodeExist = _context.Employee.Any(e => e.EmployeeCode == emplCode && e.Id != currentId);
            return isCodeExist;
        }

        private bool ExistDepartment(long deptId)
        {
            var isDeptExist = _context.Department.Any(d => d.Id == deptId && d.IsDelete == 0);
            return !isDeptExist;
        }
    }
}
