using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ItemClassCatagory
{
    public Guid PkClassCatagoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? ClassCatDescription { get; set; }

    public bool? Active { get; set; }

    public Guid? FkClassId { get; set; }

    public Guid? FkOwnerEntityId { get; set; }

    public bool? Enabled { get; set; }
}
