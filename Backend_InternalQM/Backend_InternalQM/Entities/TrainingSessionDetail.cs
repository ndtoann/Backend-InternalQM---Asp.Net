using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class TrainingSessionDetail
{
    public long Id { get; set; }

    public long TrainingSessionId { get; set; }

    public long TrainingId { get; set; }

    public int Status { get; set; }
}
