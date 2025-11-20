using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Employee
{
    public long Id { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string? Avatar { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public long DepartmentId { get; set; }

    public DateOnly HireDate { get; set; }

    public string Position { get; set; } = null!;

    public string? Note { get; set; }

    public long? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
