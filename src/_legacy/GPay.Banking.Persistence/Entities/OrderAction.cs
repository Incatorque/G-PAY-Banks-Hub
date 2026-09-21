using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderAction
{
    public Guid PkOrderActionId { get; set; }

    public string? Name { get; set; }
}
