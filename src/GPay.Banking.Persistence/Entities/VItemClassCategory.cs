using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VItemClassCategory
{
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public string? Class { get; set; }

    public Guid PkClassCatagoryId { get; set; }

    public string? ClassCatDescription { get; set; }

    public Guid? PkClassId { get; set; }

    public string? DisplayValue { get; set; }

    public string? ClassDescription { get; set; }

    public Guid? FkOwnerEntityId { get; set; }

    public string? Taname { get; set; }

    public string Status { get; set; } = null!;
}
