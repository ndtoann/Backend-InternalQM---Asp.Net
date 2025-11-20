using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class ManufacturingDefect
{
    public long Id { get; set; }

    public string? OrderNo { get; set; }

    public string? PartName { get; set; }

    public int QtyOrder { get; set; }

    public int QtyNg { get; set; }

    public DateOnly DateMonth { get; set; }

    public string ErrorDetected { get; set; } = null!;

    public string ErrorType { get; set; } = null!;

    public string? ErrorCause { get; set; }

    public string ErrorContent { get; set; } = null!;

    public string? ToleranceAssessment { get; set; }

    public string? Reason { get; set; }

    public string? Countermeasure { get; set; }

    public string Ncc { get; set; } = null!;

    public string? EmployeeCode { get; set; }

    public string? Department { get; set; }

    public DateOnly? ErrorCompletionDate { get; set; }

    public string? RemedialMeasures { get; set; }

    public string? Note { get; set; }

    public string? TimeWriteError { get; set; }

    public string? ReviewNnds { get; set; }

    public long? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
