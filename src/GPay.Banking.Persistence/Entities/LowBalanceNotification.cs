using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class LowBalanceNotification
{
    public Guid PkLowBalancaNotificationId { get; set; }

    public Guid? FkAccountId { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkSupplierClientId { get; set; }

    public decimal? LowBalanceValue { get; set; }

    public int? LowBalancePercentage { get; set; }

    public string? LowBalanceRuleSetting { get; set; }

    public string? LowBalanceRecipients { get; set; }

    public Guid? FkEntityAccountId { get; set; }

    public bool? ProcessedForTheDay { get; set; }
}
