using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class SupplierClientAccount
{
    public Guid PkSupplierClientAccountId { get; set; }

    public Guid FkClientAccountId { get; set; }

    public Guid FkSupplierAccountId { get; set; }

    public Guid FkSupplierClientId { get; set; }

    public virtual Account FkClientAccount { get; set; } = null!;

    public virtual Account FkSupplierAccount { get; set; } = null!;

    public virtual SupplierClient FkSupplierClient { get; set; } = null!;
}
