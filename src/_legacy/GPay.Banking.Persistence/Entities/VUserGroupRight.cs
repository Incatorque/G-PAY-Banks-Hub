using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUserGroupRight
{
    public Guid PkUserId { get; set; }

    public string? User { get; set; }

    public string? Email { get; set; }

    public Guid PkGroupId { get; set; }

    public string Group { get; set; } = null!;

    public bool? Active { get; set; }
}
