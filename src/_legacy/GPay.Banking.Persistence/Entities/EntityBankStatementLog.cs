using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityBankStatementLog
{
    public Guid PkEntityBankStatementLog { get; set; }

    public Guid FkentityBankStatementId { get; set; }

    public DateTime UpdatedDate { get; set; }

    public Guid FkuserId { get; set; }

    public string UpdateDescription { get; set; } = null!;

    public bool? IsCopied { get; set; }

    public virtual EntityBankStatement FkentityBankStatement { get; set; } = null!;

    public virtual User Fkuser { get; set; } = null!;
}
