using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ChargeNotificationLog
{
    public int PkNotificationLogId { get; set; }

    public Guid FkAccountId { get; set; }

    public Guid FkEntityId { get; set; }

    public DateTime Date { get; set; }

    public decimal ChargeAmount { get; set; }

    public string Destination { get; set; } = null!;

    public string MailSent { get; set; } = null!;
}
