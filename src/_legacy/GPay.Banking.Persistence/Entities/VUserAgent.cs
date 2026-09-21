using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUserAgent
{
    public Guid PkAgentId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public Guid FkStatusId { get; set; }

    public string? Taname { get; set; }

    public Guid FkUserId { get; set; }
}
