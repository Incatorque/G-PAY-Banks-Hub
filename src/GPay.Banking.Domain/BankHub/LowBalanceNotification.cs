using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("LowBalanceNotification")]
public partial class LowBalanceNotification : Entity<Guid>
{
    [Key]
    [Column("PkLowBalancaNotificationID")]
    public override Guid Id { get; protected set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column("FkSupplierClientID")]
    public Guid? FkSupplierClientId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? LowBalanceValue { get; set; }

    public int? LowBalancePercentage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LowBalanceRuleSetting { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? LowBalanceRecipients { get; set; }

    [Column("FkEntityAccountID")]
    public Guid? FkEntityAccountId { get; set; }

    public bool? ProcessedForTheDay { get; set; }
}


