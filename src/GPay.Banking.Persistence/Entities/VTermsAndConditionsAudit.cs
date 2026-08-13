using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VTermsAndConditionsAudit
{
    public Guid PkTermsId { get; set; }

    public string? UserName { get; set; }

    public string UserEmail { get; set; } = null!;

    public bool? UserTermsAccepted { get; set; }

    public bool? GpayTermsAccepted { get; set; }

    public DateTime DateAccepted { get; set; }

    public DateTime DateReset { get; set; }

    public Guid FkUserId { get; set; }

    public bool? Active { get; set; }
}
