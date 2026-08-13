using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VProductEntityWithExtra
{
    public Guid PkProductId { get; set; }

    public Guid? FkCategoryId { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid? FkBaseProductId { get; set; }

    public string Code { get; set; } = null!;

    public decimal? Price { get; set; }

    public string? ShortName { get; set; }

    public string Description { get; set; } = null!;

    public string? Specification { get; set; }

    public decimal? Size { get; set; }

    public int? NumberInPackage { get; set; }

    public string? ImageLocation { get; set; }

    public bool? Active { get; set; }

    public string? Supplier { get; set; }

    public Guid SupplierStatus { get; set; }

    public Guid FkProductStatusId { get; set; }

    public string Name { get; set; } = null!;

    public string? MarkupType { get; set; }

    public string? ContribType { get; set; }

    public decimal? MarkupValue { get; set; }

    public decimal? ContribValue { get; set; }

    public string Unit { get; set; } = null!;

    public int? RoundingDecimals { get; set; }

    public bool? Vatincl { get; set; }

    public bool? Vatrule { get; set; }

    public bool? AlwaysCustomPrice { get; set; }

    public bool? CustomPriceMore { get; set; }

    public bool? CustomPriceLess { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? MarkupInclVat { get; set; }
}
