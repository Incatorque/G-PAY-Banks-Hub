using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Vatamount
{
    public Guid PkVatId { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? Expired { get; set; }
}
