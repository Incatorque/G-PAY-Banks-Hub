using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Table("MailTemplate")]
[Keyless]
public partial class MailTemplate
{
    [Column("pkMailTemplate")]
    public Guid PkMailTemplate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Subject { get; set; } = null!;

    [Column(TypeName = "text")]
    public string BodyText { get; set; } = null!;

    [Column("IsSMS")]
    public bool? IsSms { get; set; }

    public bool? Active { get; set; }
}


