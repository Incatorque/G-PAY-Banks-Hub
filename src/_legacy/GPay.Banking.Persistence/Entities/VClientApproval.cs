using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VClientApproval
{
    public Guid PkClientApprovalId { get; set; }

    public string Status { get; set; } = null!;

    public string Client { get; set; } = null!;

    public string? UserName { get; set; }

    public string? IOrder { get; set; }

    public bool? Active { get; set; }

    public Guid PkEntityId { get; set; }

    public string? UserTo { get; set; }
}
