using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("PaymentRequest")]
public partial class PaymentRequest : Entity<Guid>
{
    [Key]
    [Column("ID")]
    public override Guid Id { get; protected set; }

    [Column("MSISDN")]
    [StringLength(20)]
    [Unicode(false)]
    public string Msisdn { get; set; } = null!;

    [Column(TypeName = "decimal(15, 2)")]
    public decimal Amount { get; set; }

    [StringLength(29)]
    [Unicode(false)]
    public string Reference { get; set; } = null!;

    [Column("IntegratedSystemID")]
    public int IntegratedSystemId { get; set; }

    [Column("OriginUserID")]
    public Guid OriginUserId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateOfRequest { get; set; }

    public bool? Completed { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Reason { get; set; } = null!;

    [ForeignKey("IntegratedSystemId")]
    [InverseProperty("PaymentRequests")]
    public virtual IntegratedSystem IntegratedSystem { get; set; } = null!;

    [ForeignKey("OriginUserId")]
    [InverseProperty("PaymentRequests")]
    public virtual User OriginUser { get; set; } = null!;
}


