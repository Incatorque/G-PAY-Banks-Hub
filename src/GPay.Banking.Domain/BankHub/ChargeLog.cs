using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ChargeLog")]
public partial class ChargeLog : Entity<Guid>
{
    [Key]
    [Column("PKChargeLog")]
    public override Guid Id { get; protected set; }

    [Column("FKAccountCharge")]
    public Guid? FkaccountCharge { get; set; }

    [Column("FKEntityAccountCharge")]
    public Guid? FkentityAccountCharge { get; set; }

    [Column("FKEntityBankStatement")]
    public Guid? FkentityBankStatement { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [ForeignKey("FkentityBankStatement")]
    [InverseProperty("ChargeLogs")]
    public virtual EntityBankStatement? FkentityBankStatementNavigation { get; set; }
}


