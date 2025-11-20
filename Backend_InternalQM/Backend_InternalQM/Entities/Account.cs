using System;
using System.Collections.Generic;

namespace Backend_InternalQM.Entities;

public partial class Account
{
    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public long EmployeeId { get; set; }

    public long? CreatedBy { get; set; }

    public DateOnly? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateOnly? UpdatedAt { get; set; }

    public long? DeleteBy { get; set; }

    public DateOnly? DeleteAt { get; set; }
}
