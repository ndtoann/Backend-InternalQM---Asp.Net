using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class ExamTrialRunAnswer
{
    public long Id { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string EmployeeCode { get; set; } = null!;

    public string ListAnswer { get; set; } = null!;

    public byte IsShow { get; set; }

    public byte MultipleChoiceCorrect { get; set; }

    public byte MultipleChoiceInCorrect { get; set; }

    public byte MultipleChoiceFail { get; set; }

    public byte EssayCorrect { get; set; }

    public byte EssayInCorrect { get; set; }

    public byte EssayFail { get; set; }

    public string? Note { get; set; }

    public long ExamTrialRunId { get; set; }

    public string? EssayResultPdf { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
