using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class GetProductTransactionCodeId
{
    public Guid PkOrderId { get; set; }

    public Guid PkProductId { get; set; }

    public int? TransactionCodeId { get; set; }

    public string? TransactionDescription { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;
}
