using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountChargeMapping")]
public partial class AccountChargeMapping : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeMapping")]
    public override Guid Id { get; protected set; }

    [Column("FkAccountChargeID")]
    public Guid FkAccountChargeId { get; set; }

    [StringLength(50)]
    public string? Mask { get; set; }

    public int MaskCondition { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public bool? SendNotification { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    public bool? ApplyCharge { get; set; }

    [Column("TransactionCodeID")]
    public int? TransactionCodeId { get; set; }

    [ForeignKey("FkAccountChargeId")]
    [InverseProperty("AccountChargeMappings")]
    public virtual AccountCharge FkAccountCharge { get; set; } = null!;
}


