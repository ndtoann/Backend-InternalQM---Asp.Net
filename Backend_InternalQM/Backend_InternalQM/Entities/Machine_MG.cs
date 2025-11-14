using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Machine_MG
{
    public string MachineCode { get; set; } = null!;

    public long MachineGroupId { get; set; }

    public string Material { get; set; } = null!;
}
