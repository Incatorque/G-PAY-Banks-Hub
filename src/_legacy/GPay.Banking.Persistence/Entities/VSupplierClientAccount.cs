using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VSupplierClientAccount
{
    public Guid PkSupplierClientAccountId { get; set; }

    public string? SupplierAccountNumber { get; set; }

    public string? ClientAccountNumber { get; set; }

    public Guid FkSupplierClientId { get; set; }

    public Guid FkClientAccountId { get; set; }

    public Guid FkSupplierAccountId { get; set; }
}
