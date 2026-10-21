using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Category")]
public partial class Category : Entity<Guid>
{
    [Key]
    [Column("PkCategoryID")]
    public override Guid Id { get; protected set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [InverseProperty("FkCategory")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}


