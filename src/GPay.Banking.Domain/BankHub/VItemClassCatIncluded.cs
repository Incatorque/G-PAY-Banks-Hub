using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VItemClassCatIncluded
{
    [Column("PkCategoryIncludedID")]
    public Guid? PkCategoryIncludedId { get; set; }

    [Column("FkClassCategoryID")]
    public Guid? FkClassCategoryId { get; set; }

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

    public bool? Included { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [StringLength(653)]
    [Unicode(false)]
    public string? DisplayValue { get; set; }

    public bool? Enabled { get; set; }
}


