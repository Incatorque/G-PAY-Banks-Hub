using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VSupplierClientList
{
    public Guid ClientId { get; set; }

    public string ClientName { get; set; } = null!;

    public Guid SupplierId { get; set; }

    public string SupplierName { get; set; } = null!;

    public Guid FkUserId { get; set; }
}
