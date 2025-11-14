using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class EmployeeWorkHistory
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public string Department { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Note { get; set; }
}
