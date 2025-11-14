using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class TestRunPractice
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public string TestRunName { get; set; } = null!;

    public string TestRunLevel { get; set; } = null!;

    public string PartName { get; set; } = null!;

    public string Result { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }
}
