using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class StatementRelationType
{
    public int PkStatementRelationTypeId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<StatementRelation> StatementRelations { get; set; } = new List<StatementRelation>();
}
