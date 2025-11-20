using Backend_InternalQM.Entities;
using Backend_InternalQM.Models;

namespace Backend_InternalQM.Modules.EmployeeModule
{
    public interface IEmployeeService
    {
        public Task<List<EmployeeDto>> GetAllEmployees(EmployeeFilterDto filters);

        public Task<EmployeeDto> GetEmployeeById(long id);

        public Task<Employee> Create(EmployeeDto employee, long createdBy);

        public Task<Employee> Update(long id, EmployeeDto employee, long updatedBy);

        public Task<bool> SoftDelete(long id, long deletedBy);
    }
}
