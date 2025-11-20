using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class ExamResult
{
    public long Id { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string ListAnswer { get; set; } = null!;

    public byte IsShow { get; set; }

    public byte? MultipleChoiceScore { get; set; }

    public byte? EssayScore { get; set; }

    public string? Note { get; set; }

    public long ExamId { get; set; }

    public string? EssayResultPdf { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
