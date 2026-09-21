using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ApprovalType
{
    public Guid PkApprocalTypeId { get; set; }

    public string Name { get; set; } = null!;
}
