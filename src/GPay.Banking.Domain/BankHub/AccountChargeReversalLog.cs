using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountChargeReversalLog")]
public partial class AccountChargeReversalLog : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeReversalLogId")]
    public override Guid Id { get; protected set; }

    public Guid FkAccountChargeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [Unicode(false)]
    public string LogDescription { get; set; } = null!;

    [ForeignKey("FkAccountChargeId")]
    [InverseProperty("AccountChargeReversalLogs")]
    public virtual AccountCharge FkAccountCharge { get; set; } = null!;

    [ForeignKey("FkuserId")]
    [InverseProperty("AccountChargeReversalLogs")]
    public virtual User Fkuser { get; set; } = null!;
}


