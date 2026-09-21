using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderItemListSupplier
{
    public Guid PkOrderItemId { get; set; }

    public Guid FkOrderId { get; set; }

    public decimal Quantity { get; set; }

    public string? ShortName { get; set; }

    public Guid FkSupplierId { get; set; }

    public decimal DeliveredQuantity { get; set; }

    public decimal? Price { get; set; }

    public decimal Total { get; set; }

    public string Description { get; set; } = null!;

    public decimal Vat { get; set; }

    public decimal InclVat { get; set; }

    public bool? Active { get; set; }

    public Guid PkProductId { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public string? Taname { get; set; }

    public int? RoundingDecimals { get; set; }

    public decimal OverridgeQty { get; set; }

    public string? OverridgeTerms { get; set; }

    public string? ImageLocation { get; set; }

    public Guid? FkLinkedOrderItemId { get; set; }

    public string? Reference { get; set; }

    public decimal? ContribValue { get; set; }

    public string? ContribType { get; set; }

    public bool? ContributionInclVat { get; set; }

    public decimal? MarkupValue { get; set; }

    public string? MarkupType { get; set; }

    public bool? MarkupInclVat { get; set; }

    public bool? Vatincl { get; set; }

    public bool? Vatrule { get; set; }

    public string Code { get; set; } = null!;

    public Guid? ProductCategory { get; set; }

    public string ServiceType { get; set; } = null!;

    public string Bopcode { get; set; } = null!;
}
