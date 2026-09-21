using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VItemClassCatIncluded
{
    public Guid? PkCategoryIncludedId { get; set; }

    public Guid? FkClassCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? ClassCatDescription { get; set; }

    public bool? Active { get; set; }

    public Guid? FkClassId { get; set; }

    public Guid? FkOwnerEntityId { get; set; }

    public bool? Included { get; set; }

    public Guid? FkEntityId { get; set; }

    public string? DisplayValue { get; set; }

    public bool? Enabled { get; set; }
}
