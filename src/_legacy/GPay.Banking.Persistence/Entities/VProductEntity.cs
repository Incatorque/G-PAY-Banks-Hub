using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VProductEntity
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

    public Guid? FkUnitId { get; set; }

    public string? ImageLocation { get; set; }

    public bool? Active { get; set; }

    public string? Supplier { get; set; }

    public Guid SupplierStatus { get; set; }

    public Guid FkProductStatusId { get; set; }

    public string Name { get; set; } = null!;

    public string? MarkupType { get; set; }

    public decimal? MarkupValue { get; set; }

    public string? ContribType { get; set; }

    public decimal? ContribValue { get; set; }

    public DateTime? DateCreated { get; set; }
}
