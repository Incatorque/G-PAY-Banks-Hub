using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("StatementRelationType")]
public partial class StatementRelationType : Entity<int>
{
    [Key]
    [Column("PkStatementRelationTypeID")]
    public override int Id { get; protected set; }

    [StringLength(25)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [InverseProperty("FkStatementRelationType")]
    public virtual ICollection<StatementRelation> StatementRelations { get; set; } = new List<StatementRelation>();
}


