using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VallProduct
{
    public string Category { get; set; } = null!;

    public string GridZone { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? Size { get; set; }

    public string Unit { get; set; } = null!;

    public string? Specification { get; set; }

    public int? NumberInPackage { get; set; }

    public decimal? Price { get; set; }

    public int RoundingDecimals { get; set; }

    public bool? AlwaysCustomPrice { get; set; }

    public bool? CustomPriceLess { get; set; }

    public bool? CustomPriceMore { get; set; }

    public decimal MinOrder { get; set; }

    public decimal MaxOrder { get; set; }

    public string? Supplier { get; set; }

    public string? Client { get; set; }

    public decimal? Differential { get; set; }

    public string? DifferentialType { get; set; }

    public string ProductStatus { get; set; } = null!;

    public string? ShortName { get; set; }

    public decimal ProductAllocationQty { get; set; }

    public string ProductAllocationTerms { get; set; } = null!;

    public decimal OverridgeQty { get; set; }

    public string OverridgeTerms { get; set; } = null!;

    public bool? Vatexemption { get; set; }

    public string VatText { get; set; } = null!;

    public string? ClientAddress { get; set; }

    public string? ImageLocation { get; set; }

    public Guid SupplierId { get; set; }

    public Guid ClientId { get; set; }

    public Guid ClientAddressId { get; set; }

    public Guid ProductSupplierId { get; set; }

    public Guid ProductId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid? FkDifferentialTypeId { get; set; }

    public Guid ProductZoneId { get; set; }

    public Guid PkSupplierClientId { get; set; }

    public Guid? FkEntityTypeId { get; set; }

    public decimal ContribValue { get; set; }

    public decimal MarkupValue { get; set; }

    public bool ContributionInclVat { get; set; }

    public bool MarkupInclVat { get; set; }

    public bool? Vatincl { get; set; }

    public bool? Vatrule { get; set; }

    public string? ContribType { get; set; }

    public string? MarkupType { get; set; }
}
