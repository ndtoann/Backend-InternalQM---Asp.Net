using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class MachineMaintenance
{
    public long Id { get; set; }

    public string MachineCode { get; set; } = null!;

    public DateOnly DateMonth { get; set; }

    public string? MaintenanceStaff { get; set; }

    public string MaintenanceContent { get; set; } = null!;

    public byte IsComplete { get; set; }

    public string? Note { get; set; }

    public long? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
