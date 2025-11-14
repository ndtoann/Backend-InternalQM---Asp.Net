using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class AccountPermission
{
    public long AccountId { get; set; }

    public long PermissionId { get; set; }
}
