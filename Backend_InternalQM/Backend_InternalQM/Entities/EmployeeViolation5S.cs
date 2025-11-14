using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class EmployeeViolation5S
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public long Violation5Sid { get; set; }

    public DateOnly DateMonth { get; set; }

    public int Qty { get; set; }
}
