using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderStatementRelation
{
    public Guid PkOrderStatementId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid? FkClientStatementId { get; set; }

    public Guid? FkSupplierStatementId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }
}
