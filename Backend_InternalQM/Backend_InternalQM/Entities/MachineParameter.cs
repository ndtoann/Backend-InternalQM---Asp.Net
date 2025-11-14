using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class MachineParameter
{
    public long Id { get; set; }

    public long MachineId { get; set; }

    public string Type { get; set; } = null!;

    public decimal Parameters { get; set; }

    public DateOnly DateMonth { get; set; }
}
