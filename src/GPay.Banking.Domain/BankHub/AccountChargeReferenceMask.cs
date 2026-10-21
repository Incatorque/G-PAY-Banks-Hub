using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountChargeReferenceMask")]
public partial class AccountChargeReferenceMask : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeReferenceMask")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityAccountChargeID")]
    public Guid FkEntityAccountChargeId { get; set; }

    [StringLength(50)]
    public string Mask { get; set; } = null!;

    public int MaskCondition { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public bool? SendNotification { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Email { get; set; }

    public bool? ApplyCharge { get; set; }

    [ForeignKey("FkEntityAccountChargeId")]
    [InverseProperty("AccountChargeReferenceMasks")]
    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;
}


