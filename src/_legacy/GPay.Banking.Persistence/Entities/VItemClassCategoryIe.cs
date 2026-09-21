using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VItemClassCategoryIe
{
    public Guid PkClassCatagoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? ClassCatDescription { get; set; }

    public bool? Active { get; set; }

    public Guid? FkClassId { get; set; }

    public bool? Included { get; set; }
}
