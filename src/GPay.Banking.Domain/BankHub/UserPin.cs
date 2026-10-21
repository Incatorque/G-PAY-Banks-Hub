using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserPin")]
public partial class UserPin : Entity<Guid>
{
    [Key]
    [Column("PkUserPin")]
    public override Guid Id { get; protected set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkSystemID")]
    public int FkSystemId { get; set; }

    [Column("PIN")]
    [StringLength(512)]
    public string Pin { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime IssuedAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpiresAt { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RedeemedAt { get; set; }

    [ForeignKey("FkSystemId")]
    [InverseProperty("UserPins")]
    public virtual IntegratedSystem FkSystem { get; set; } = null!;
}


