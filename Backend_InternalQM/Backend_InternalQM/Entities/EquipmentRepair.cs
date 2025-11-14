using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class EquipmentRepair
{
    public long Id { get; set; }

    public string? EquipmentCode { get; set; }

    public string EquipmentName { get; set; } = null!;

    public int Qty { get; set; }

    public long? DepartmentId { get; set; }

    public string ErrorCondition { get; set; } = null!;

    public string? Reason { get; set; }

    public string? ProcessingMethod { get; set; }

    public DateOnly DateMonth { get; set; }

    public DateOnly? CompletionDate { get; set; }

    public DateOnly? ConfirmCompletionDate { get; set; }

    public string? RemedialStaff { get; set; }

    public string? RecipientOfRepairedDevice { get; set; }

    public string? RepairCosts { get; set; }

    public string? Note { get; set; }
}
