using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderCommissionStatementRelation
{
    public Guid PkOrderCommissionStatementId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid FkCommissionStatementid { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool? IsActive { get; set; }
}
