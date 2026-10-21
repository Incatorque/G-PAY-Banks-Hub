using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class AccountChargeReversal : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeReversalId")]
    public override Guid Id { get; protected set; }

    [Column("FkAccountChargeID")]
    public Guid FkAccountChargeId { get; set; }

    [StringLength(50)]
    public string? Mask { get; set; }

    [Column(TypeName = "money")]
    public decimal? Value { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [ForeignKey("FkAccountChargeId")]
    [InverseProperty("AccountChargeReversals")]
    public virtual AccountCharge FkAccountCharge { get; set; } = null!;
}


