using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUserSuppliersClient
{
    public Guid PkSupplierClientId { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid FkClientId { get; set; }

    public string? SupplierClientRefrence { get; set; }

    public Guid FkUserId { get; set; }
}
