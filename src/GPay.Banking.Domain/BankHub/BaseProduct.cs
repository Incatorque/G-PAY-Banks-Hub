using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BaseProduct")]
public partial class BaseProduct : Entity<Guid>
{
    [Key]
    [Column("PkBaseProductID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("FkGridID")]
    public Guid? FkGridId { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkBaseProduct")]
    public virtual ICollection<BaseProductPrice> BaseProductPrices { get; set; } = new List<BaseProductPrice>();

    [ForeignKey("FkGridId")]
    [InverseProperty("BaseProducts")]
    public virtual Grid? FkGrid { get; set; }

    [InverseProperty("FkBaseProduct")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}


