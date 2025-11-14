using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Productivity
{
    public long Id { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public decimal Score { get; set; }

    public byte ReportingMonth { get; set; }

    public int ReportingYear { get; set; }
}
