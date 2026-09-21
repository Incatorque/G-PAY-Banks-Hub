using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ItemClassCatIncluded
{
    public Guid PkCategoryIncludedId { get; set; }

    public Guid FkClassCategoryId { get; set; }

    public bool? Included { get; set; }

    public Guid? FkEntityId { get; set; }
}
