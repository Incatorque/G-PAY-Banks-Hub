using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserNedbankWallet")]
public partial class UserNedbankWallet : Entity<Guid>
{
    [Key]
    [Column("PkWalletID")]
    public override Guid Id { get; protected set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("CallbackID")]
    public Guid? CallbackId { get; set; }

    public int? WalletId { get; set; }

    public Guid? FkEntityBankStatement { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? WalletAccountNumber { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("UserNedbankWallets")]
    public virtual User FkUser { get; set; } = null!;
}


