using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ChargeLog
{
    public Guid PkchargeLog { get; set; }

    public Guid? FkaccountCharge { get; set; }

    public Guid? FkentityAccountCharge { get; set; }

    public Guid? FkentityBankStatement { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual EntityBankStatement? FkentityBankStatementNavigation { get; set; }
}
