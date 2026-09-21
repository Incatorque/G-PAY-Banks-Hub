using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VSupplierClientForPaymentPriorityOop
{
    public Guid PkSupplierClientId { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid FkClientId { get; set; }

    public string? SupplierClientRefrence { get; set; }

    public string ClientName { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public int? Pporder { get; set; }

    public decimal? Ppamount { get; set; }

    public string? EntityType { get; set; }
}
