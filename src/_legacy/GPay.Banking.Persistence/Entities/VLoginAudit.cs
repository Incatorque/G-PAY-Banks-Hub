using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VLoginAudit
{
    public Guid PkLoginAuditId { get; set; }

    public string Username { get; set; } = null!;

    public string? Passsword { get; set; }

    public string? Pin { get; set; }

    public DateTime LoginDate { get; set; }

    public string Ipaddress { get; set; } = null!;

    public string LoginSuccess { get; set; } = null!;

    public bool Active { get; set; }
}
