using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountChargeMappingLog")]
[Index("FkuserId", Name = "IX_AccountChargeMappingLog_FKUserId")]
[Index("FkAccountChargeId", Name = "IX_AccountChargeMappingLog_FkAccountChargeId")]
[Index("FkAccountChargeMappingId", Name = "IX_AccountChargeMappingLog_FkAccountChargeMappingId")]
public partial class AccountChargeMappingLog : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeMappingLogId")]
    public override Guid Id { get; protected set; }

    public Guid FkAccountChargeMappingId { get; set; }

    public Guid FkAccountChargeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [Unicode(false)]
    public string LogDescription { get; set; } = null!;
}


