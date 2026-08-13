using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VItemClass
{
    public Guid PkClassId { get; set; }

    public string Name { get; set; } = null!;

    public string? ClassDescription { get; set; }

    public bool? Active { get; set; }

    public string? Taname { get; set; }

    public Guid? FkOwnerEntityId { get; set; }
}
