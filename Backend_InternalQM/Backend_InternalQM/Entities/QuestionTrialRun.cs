using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class QuestionTrialRun
{
    public long Id { get; set; }

    public string QuestionText { get; set; } = null!;

    public string OptionA { get; set; } = null!;

    public string OptionB { get; set; } = null!;

    public string OptionC { get; set; } = null!;

    public string OptionD { get; set; } = null!;

    public string CorrectOption { get; set; } = null!;

    public byte IsCritical { get; set; }

    public long ExamTrialRunId { get; set; }

    public long? DisplayOrder { get; set; }

    public long? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
