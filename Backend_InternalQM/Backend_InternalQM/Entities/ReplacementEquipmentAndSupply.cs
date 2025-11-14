using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class ReplacementEquipmentAndSupply
{
    public long Id { get; set; }

    public string EquipmentAndlSupplies { get; set; } = null!;

    public string? FilePdf { get; set; }

    public long EquipmentRepairId { get; set; }
}
