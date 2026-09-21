using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Product
{
    public Guid PkProductId { get; set; }

    public Guid? FkCategoryId { get; set; }

    public Guid? FkBaseProductId { get; set; }

    public string Code { get; set; } = null!;

    public decimal? Price { get; set; }

    public string? ShortName { get; set; }

    public string Description { get; set; } = null!;

    public string? Specification { get; set; }

    public decimal? Size { get; set; }

    public int? NumberInPackage { get; set; }

    public Guid? FkUnitId { get; set; }

    public bool? Active { get; set; }

    public bool? Vatincl { get; set; }

    public bool? Vatrule { get; set; }

    public bool? AlwaysCustomPrice { get; set; }

    public bool? CustomPriceLess { get; set; }

    public bool? CustomPriceMore { get; set; }

    public int? RoundingDecimals { get; set; }

    public decimal? MinOrder { get; set; }

    public decimal? MaxOrder { get; set; }

    public string? ImageLocation { get; set; }

    public string? MarkupType { get; set; }

    public decimal? MarkupValue { get; set; }

    public string? ContribType { get; set; }

    public decimal? ContribValue { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? MarkupInclVat { get; set; }

    public bool? Deleted { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual ICollection<ClientProductApproval> ClientProductApprovals { get; set; } = new List<ClientProductApproval>();

    public virtual BaseProduct? FkBaseProduct { get; set; }

    public virtual Category? FkCategory { get; set; }

    public virtual Unit? FkUnit { get; set; }

    public virtual ICollection<ProductExcluded> ProductExcludeds { get; set; } = new List<ProductExcluded>();

    public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();

    public virtual ICollection<TransactionCodeMap> TransactionCodeMaps { get; set; } = new List<TransactionCodeMap>();
}
