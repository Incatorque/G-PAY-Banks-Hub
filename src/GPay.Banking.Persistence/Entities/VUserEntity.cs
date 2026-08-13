using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUserEntity
{
    public Guid PkEntityId { get; set; }

    public string? Name { get; set; }

    public bool? Active { get; set; }

    public Guid FkUserId { get; set; }

    public Guid FkStatusId { get; set; }
}
