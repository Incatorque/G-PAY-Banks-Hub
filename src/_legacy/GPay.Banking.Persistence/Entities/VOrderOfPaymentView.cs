using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderOfPaymentView
{
    public Guid PkSupplierClientId { get; set; }

    public Guid ClientId { get; set; }

    public Guid SupplierId { get; set; }

    public string? ClientName { get; set; }

    public string? SupplierName { get; set; }

    public decimal LastPaymentAmount { get; set; }

    public DateTime? LastPaymentDate { get; set; }

    public string ValidDateRange { get; set; } = null!;

    public decimal MonthToDate { get; set; }

    public int? Pporder { get; set; }

    public decimal? Ppamount { get; set; }

    public bool Active { get; set; }
}
