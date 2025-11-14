using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class TestRunPracticeDetail
{
    public long Id { get; set; }

    public long TestRunPracticeId { get; set; }

    public string OperationName { get; set; } = null!;

    public int PrepTimeStandard { get; set; }

    public int? PrepTimeActual { get; set; }

    public int OffsetCountStandard { get; set; }

    public int? OffsetCountActual { get; set; }

    public string? Note { get; set; }
}
