using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityBankStatementLog")]
public partial class EntityBankStatementLog : Entity<Guid>
{
    [Key]
    [Column("PkEntityBankStatementLog")]
    public override Guid Id { get; protected set; }

    [Column("FKEntityBankStatementId")]
    public Guid FkentityBankStatementId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [Unicode(false)]
    public string UpdateDescription { get; set; } = null!;

    public bool? IsCopied { get; set; }

    [ForeignKey("FkentityBankStatementId")]
    [InverseProperty("EntityBankStatementLogs")]
    public virtual EntityBankStatement FkentityBankStatement { get; set; } = null!;

    [ForeignKey("FkuserId")]
    [InverseProperty("EntityBankStatementLogs")]
    public virtual User Fkuser { get; set; } = null!;
}


