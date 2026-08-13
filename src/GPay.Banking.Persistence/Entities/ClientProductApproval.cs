using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ClientProductApproval
{
    public Guid PkClientProductApprovalId { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid? FkProductId { get; set; }

    public Guid? FkOrderStatusId { get; set; }

    public int IOrder { get; set; }

    public virtual Entity FkClient { get; set; } = null!;

    public virtual OrderStatus? FkOrderStatus { get; set; }

    public virtual Product? FkProduct { get; set; }

    public virtual User FkUser { get; set; } = null!;
}
