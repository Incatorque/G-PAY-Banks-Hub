using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VClientDataApproval
{
    public Guid PkEntityApprovalId { get; set; }

    public string? Client { get; set; }

    public string? UserName { get; set; }

    public string? IOrder { get; set; }

    public bool Active { get; set; }

    public Guid? PkEntityId { get; set; }

    public string? RequestedUserName { get; set; }

    public Guid? RequestedUserId { get; set; }

    public bool? New { get; set; }

    public bool? Edit { get; set; }
}
