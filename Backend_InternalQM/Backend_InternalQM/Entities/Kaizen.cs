using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Kaizen
{
    public long Id { get; set; }

    public DateOnly DateMonth { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string EmployeeName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public string? AppliedDepartment { get; set; }

    public string? ImprovementGoal { get; set; }

    public string ImprovementTitle { get; set; } = null!;

    public string? CurrentSituation { get; set; }

    public string ProposedIdea { get; set; } = null!;

    public string? EstimatedBenefit { get; set; }

    public string? TeamLeaderRating { get; set; }

    public string? ManagementReview { get; set; }

    public string? Picture { get; set; }

    public string? Deadline { get; set; }

    public string? StartTime { get; set; }

    public string? CurrentStatus { get; set; }

    public string? Note { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public string? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
