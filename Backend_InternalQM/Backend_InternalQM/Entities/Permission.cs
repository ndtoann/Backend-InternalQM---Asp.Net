using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Permission
{
    public long Id { get; set; }

    public string ClaimValue { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Module { get; set; } = null!;
}
