using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Violation5S
{
    public long Id { get; set; }

    public string Content5S { get; set; } = null!;

    public decimal Penaty { get; set; }

    public string? Note { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public string? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
