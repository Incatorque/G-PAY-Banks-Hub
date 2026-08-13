using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AgentSupplier
{
    public Guid PkAgentSupplierId { get; set; }

    public Guid FkAgentId { get; set; }

    public Guid FkSupplierId { get; set; }

    public virtual Entity FkAgent { get; set; } = null!;

    public virtual Entity FkSupplier { get; set; } = null!;
}
