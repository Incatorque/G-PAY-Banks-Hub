using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class StatementRelation
{
    public int PkStatementRelationId { get; set; }

    public Guid FkParentEntityBankStatementId { get; set; }

    public Guid FkEntityBankStatementId { get; set; }

    public int FkStatementRelationTypeId { get; set; }

    public DateTime DateLinked { get; set; }

    public virtual EntityBankStatement FkEntityBankStatement { get; set; } = null!;

    public virtual EntityBankStatement FkParentEntityBankStatement { get; set; } = null!;

    public virtual StatementRelationType FkStatementRelationType { get; set; } = null!;
}
