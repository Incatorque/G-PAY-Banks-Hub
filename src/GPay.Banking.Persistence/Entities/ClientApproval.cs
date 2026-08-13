using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ClientApproval
{
    public Guid PkClientApprovalId { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid? FkOrderStatusId { get; set; }

    public Guid? FkOrderUserId { get; set; }

    public int IOrder { get; set; }

    public virtual Entity FkClient { get; set; } = null!;

    public virtual OrderStatus? FkOrderStatus { get; set; }

    public virtual User? FkOrderUser { get; set; }

    public virtual User FkUser { get; set; } = null!;
}
