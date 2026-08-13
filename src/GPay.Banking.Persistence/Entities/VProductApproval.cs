using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VProductApproval
{
    public string Supplier { get; set; } = null!;

    public Guid PkSupplierId { get; set; }

    public string? Product { get; set; }

    public int IOrder { get; set; }

    public string? Username { get; set; }

    public string Status { get; set; } = null!;

    public string Client { get; set; } = null!;

    public Guid PkClientId { get; set; }

    public bool? Active { get; set; }

    public Guid PkClientProductApprovalId { get; set; }
}
