using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class TrainingSession
{
    public long Id { get; set; }

    public long EmployeeId { get; set; }

    public long TrainingId { get; set; }

    public int Status { get; set; }

    public string EvaluationPeriod { get; set; } = null!;

    public byte Score { get; set; }
}
