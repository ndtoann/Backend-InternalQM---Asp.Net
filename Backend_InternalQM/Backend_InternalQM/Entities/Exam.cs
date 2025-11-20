using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Exam
{
    public long Id { get; set; }

    public string ExamCategory { get; set; }

    public string ExamName { get; set; } = null!;

    public int DurationMinute { get; set; }

    public byte IsActive { get; set; }

    public byte TlTotal { get; set; }

    public string? EssayQuestion { get; set; }

    public long? DepartmentID { get; set; }

    public long? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
