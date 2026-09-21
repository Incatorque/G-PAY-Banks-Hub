using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityAccountLink
{
    public Guid FkAccountId { get; set; }

    public Guid FkEntityId { get; set; }

    public string? Entity { get; set; }

    public string? ResourceType { get; set; }

    public string? AccountLabel { get; set; }
}
