using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ItemClassCatagory")]
public partial class ItemClassCatagory : Entity<Guid>
{
    [Key]
    [Column("PkClassCatagoryID")]
    public override Guid Id { get; protected set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? ClassCatDescription { get; set; }

    public bool? Active { get; set; }

    [Column("FkClassID")]
    public Guid? FkClassId { get; set; }

    [Column("FkOwnerEntityID")]
    public Guid? FkOwnerEntityId { get; set; }

    public bool? Enabled { get; set; }
}


