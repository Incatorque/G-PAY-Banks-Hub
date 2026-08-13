using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VClientTotal
{
    public decimal? Expr1 { get; set; }

    public decimal? Expr2 { get; set; }

    public Guid FkClientId { get; set; }

    public Guid? FkOrderTypeId { get; set; }

    public Guid? FkFromAccountId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid FkSupplierId { get; set; }

    public decimal MarkupValue { get; set; }

    public decimal ContribValue { get; set; }

    public string Code { get; set; } = null!;

    public string? MarkupType { get; set; }

    public string? ContribType { get; set; }

    public bool? MarkupInclVat { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? InclVat { get; set; }

    public decimal? Quantity { get; set; }

    public decimal? DeliveredQuantity { get; set; }

    public bool? Vatexemption { get; set; }

    public bool? Vatrule { get; set; }

    public decimal? BasePrice { get; set; }
}
