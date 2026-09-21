using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserNedbankWallet
{
    public Guid PkWalletId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid? CallbackId { get; set; }

    public int? WalletId { get; set; }

    public Guid? FkEntityBankStatement { get; set; }

    public string? WalletAccountNumber { get; set; }

    public virtual User FkUser { get; set; } = null!;
}
