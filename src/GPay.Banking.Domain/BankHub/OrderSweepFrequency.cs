using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderSweepFrequency")]
public partial class OrderSweepFrequency : Entity<int>
{
    [Key]
    [Column("PkOrderSweepFrequencyID")]
    public override int Id { get; protected set; }

    public int Days { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }

    [InverseProperty("FkOrderSweepFrequency")]
    public virtual ICollection<EntityOrderSweep> EntityOrderSweeps { get; set; } = new List<EntityOrderSweep>();
}


