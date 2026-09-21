using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityEntityTypeList
{
    public Guid PkEntityId { get; set; }

    public Guid PkEntityTypeId { get; set; }

    public string Name { get; set; } = null!;

    public string? DefaultLink { get; set; }

    public string? EntityName { get; set; }

    public bool? Active { get; set; }

    public Guid FkStatusId { get; set; }
}
