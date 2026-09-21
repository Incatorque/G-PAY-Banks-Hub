using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderOfPaymentGrandTotalsPerMonth
{
    public Guid? PkSupplierClientId { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkSupplierId { get; set; }

    public decimal? Gt { get; set; }

    public decimal? LastPaymentAmount { get; set; }

    public DateTime? LastPaymentDate { get; set; }

    public string? ValidDateRange { get; set; }
}
