using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Department
{
    public long Id { get; set; }

    public string DepartmentName { get; set; } = null!;

    public byte? IsDelete { get; set; }

    public string? Note { get; set; }
}
