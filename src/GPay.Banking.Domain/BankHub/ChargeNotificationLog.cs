using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
[Table("ChargeNotificationLog")]
public partial class ChargeNotificationLog
{
    public int PkNotificationLogId { get; set; }

    public Guid FkAccountId { get; set; }

    public Guid FkEntityId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column(TypeName = "money")]
    public decimal ChargeAmount { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Destination { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string MailSent { get; set; } = null!;
}


