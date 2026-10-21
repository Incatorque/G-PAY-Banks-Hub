using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OutwardBOPCategory")]
public partial class OutwardBopcategory : Entity<Guid>
{
    [Key]
    [Column("PKOutwardBOPCategoryId")]
    public override Guid Id { get; protected set; }

    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public bool Active { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("OutwardBopcategoryCreatedByNavigations")]
    public virtual User CreatedByNavigation { get; set; } = null!;

    [InverseProperty("FkOutwardBopcategory")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("UpdatedBy")]
    [InverseProperty("OutwardBopcategoryUpdatedByNavigations")]
    public virtual User UpdatedByNavigation { get; set; } = null!;
}


