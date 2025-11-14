using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Training
{
    public long Id { get; set; }

    public string TrainingName { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public string? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
