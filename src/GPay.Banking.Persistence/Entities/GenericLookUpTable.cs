using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class GenericLookUpTable
{
    public Guid PkGenericLookUpTableId { get; set; }

    public string? PkName { get; set; }

    public string? TableName { get; set; }

    public string? PageName { get; set; }

    public Guid? FkModuleId { get; set; }

    public virtual Module? FkModule { get; set; }
}
