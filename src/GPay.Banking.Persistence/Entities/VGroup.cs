using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VGroup
{
    public Guid PkGroupId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public Guid? FkOwnerEntityId { get; set; }

    public string? Taname { get; set; }

    public string? Description { get; set; }

    public bool? Deleted { get; set; }
}
