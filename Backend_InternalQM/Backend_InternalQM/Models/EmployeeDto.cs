using System.ComponentModel.DataAnnotations;

namespace Backend_InternalQM.Models
{
    public class EmployeeDto
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string EmployeeName { get; set; }

        public string? Avatar { get; set; }

        public DateOnly? DateOfBirth { get; set; }

        [StringLength(5)]
        public string? Gender { get; set; }

        public long DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        [Required]
        public DateOnly HireDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        public string? Note { get; set; }
    }

    public class EmployeeFilterDto
    {
        public string? Key { get; set; }
        public long? DepartmentId { get; set; }
    }
}
