using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Table("GenericLookUpTable")]
[Keyless]
public partial class GenericLookUpTable
{
    [Column("PkGenericLookUpTableID")]
    public Guid PkGenericLookUpTableId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PkName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TableName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? PageName { get; set; }

    [Column("FkModuleID")]
    public Guid? FkModuleId { get; set; }

    [ForeignKey("FkModuleId")]
    [InverseProperty("GenericLookUpTables")]
    public virtual Module? FkModule { get; set; }
}


