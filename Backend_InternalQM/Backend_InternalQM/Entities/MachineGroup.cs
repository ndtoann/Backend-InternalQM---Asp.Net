using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class MachineGroup
{
    public long Id { get; set; }

    public string? GroupName { get; set; }

    public string MachineType { get; set; } = null!;

    public string? Standard { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public string? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
