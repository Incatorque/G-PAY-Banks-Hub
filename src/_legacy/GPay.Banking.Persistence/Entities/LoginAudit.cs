using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class LoginAudit
{
    public Guid PkLoginAuditId { get; set; }

    public Guid? FkUserId { get; set; }

    public string Username { get; set; } = null!;

    public string Psssword { get; set; } = null!;

    public string? Pin { get; set; }

    public DateTime LoginDate { get; set; }

    public string Ipaddress { get; set; } = null!;

    public bool? LoginSuccess { get; set; }

    public virtual User? FkUser { get; set; }
}
