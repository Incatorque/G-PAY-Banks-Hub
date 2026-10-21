using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityOrderSweep")]
public partial class EntityOrderSweep : Entity<int>
{
    [Key]
    [Column("PkEntityOrderSweepID")]
    public override int Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkAccountID")]
    public Guid FkAccountId { get; set; }

    [Column("FkOrderSweepFrequencyID")]
    public int FkOrderSweepFrequencyId { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ExpiryDate { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }

    [InverseProperty("FkEntityOrderSweep")]
    public virtual ICollection<EntityOrderSweepLog> EntityOrderSweepLogs { get; set; } = new List<EntityOrderSweepLog>();

    [ForeignKey("FkAccountId")]
    [InverseProperty("EntityOrderSweeps")]
    public virtual Account FkAccount { get; set; } = null!;

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityOrderSweeps")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkOrderStatusId")]
    [InverseProperty("EntityOrderSweeps")]
    public virtual OrderStatus FkOrderStatus { get; set; } = null!;

    [ForeignKey("FkOrderSweepFrequencyId")]
    [InverseProperty("EntityOrderSweeps")]
    public virtual OrderSweepFrequency FkOrderSweepFrequency { get; set; } = null!;
}


