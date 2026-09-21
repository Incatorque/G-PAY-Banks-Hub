using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MailTemplate
{
    public Guid PkMailTemplate { get; set; }

    public string Subject { get; set; } = null!;

    public string BodyText { get; set; } = null!;

    public bool? IsSms { get; set; }

    public bool? Active { get; set; }
}
