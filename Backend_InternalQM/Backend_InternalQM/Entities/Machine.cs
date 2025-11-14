using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Machine
{
    public long Id { get; set; }

    public string? TypeMachine { get; set; }

    public string MachineCode { get; set; } = null!;

    public string MachineName { get; set; } = null!;

    public string? Department { get; set; }

    public string? Version { get; set; }

    public int? NumberOfAxes { get; set; }

    public int? NumberOfKnifeSocket { get; set; }

    public string? SpindleSpeed { get; set; }

    public string? BottleTaper { get; set; }

    public decimal? MachineTableSizeX { get; set; }

    public decimal? MachineTableSizeY { get; set; }

    public decimal? MachineJourneyX { get; set; }

    public decimal? MachineJourneyY { get; set; }

    public decimal? MachineJourneyZ { get; set; }

    public decimal? WideSize { get; set; }

    public decimal? DeepSize { get; set; }

    public decimal? HighSize { get; set; }

    public decimal? OuterPairX { get; set; }

    public decimal? OuterPairZ { get; set; }

    public decimal? PairInsideX { get; set; }

    public decimal? PairInsideZ { get; set; }

    public decimal? TailstockX { get; set; }

    public decimal? TailstockZ { get; set; }

    public decimal? Weight { get; set; }

    public string? Picture { get; set; }

    public decimal? Price { get; set; }

    public decimal? MachineCapacity { get; set; }

    public string? MachineOrigin { get; set; }

    public string? Place { get; set; }

    public string? Note { get; set; }

    public byte Status { get; set; }

    public string? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public string? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
