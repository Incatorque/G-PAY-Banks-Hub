using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VProductEntityAllAvailableProduct
{
    public Guid PkProductId { get; set; }

    public string Code { get; set; } = null!;

    public string? ShortName { get; set; }

    public string Description { get; set; } = null!;

    public bool? AlwaysCustomPrice { get; set; }

    public decimal? Price { get; set; }

    public string? MarkupType { get; set; }

    public string? ContribType { get; set; }

    public decimal? MarkupValue { get; set; }

    public decimal? ContribValue { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid FkClientId { get; set; }

    public string Unit { get; set; } = null!;

    public string? Supplier { get; set; }

    public int? RoundingDecimals { get; set; }

    public bool? Vatincl { get; set; }

    public bool? Vatrule { get; set; }

    public bool? CustomPriceMore { get; set; }

    public bool? CustomPriceLess { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? MarkupInclVat { get; set; }
}
