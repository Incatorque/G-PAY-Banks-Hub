using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderItemGboclist
{
    public Guid FkClientId { get; set; }

    public Guid? FkFromAccountId { get; set; }

    public decimal? SumVatincl { get; set; }
}
