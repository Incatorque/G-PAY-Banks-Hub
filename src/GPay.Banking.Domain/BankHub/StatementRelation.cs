using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("StatementRelation")]
public partial class StatementRelation : Entity<int>
{
    [Key]
    [Column("PkStatementRelationID")]
    public override int Id { get; protected set; }

    [Column("FkParentEntityBankStatementID")]
    public Guid FkParentEntityBankStatementId { get; set; }

    [Column("FkEntityBankStatementID")]
    public Guid FkEntityBankStatementId { get; set; }

    [Column("FkStatementRelationTypeID")]
    public int FkStatementRelationTypeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateLinked { get; set; }

    [ForeignKey("FkEntityBankStatementId")]
    [InverseProperty("StatementRelationFkEntityBankStatements")]
    public virtual EntityBankStatement FkEntityBankStatement { get; set; } = null!;

    [ForeignKey("FkParentEntityBankStatementId")]
    [InverseProperty("StatementRelationFkParentEntityBankStatements")]
    public virtual EntityBankStatement FkParentEntityBankStatement { get; set; } = null!;

    [ForeignKey("FkStatementRelationTypeId")]
    [InverseProperty("StatementRelations")]
    public virtual StatementRelationType FkStatementRelationType { get; set; } = null!;
}


