using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("HistoryCategory")]
public partial class HistoryCategory : Entity<Guid>
{
    [Key]
    [Column("PkHistoryCategoryID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkHistoryCategory")]
    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
}


