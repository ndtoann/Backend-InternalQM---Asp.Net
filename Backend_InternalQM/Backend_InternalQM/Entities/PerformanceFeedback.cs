using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class PerformanceFeedback
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public string FeedbackContent { get; set; } = null!;

    public byte Status { get; set; }

    public long? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
