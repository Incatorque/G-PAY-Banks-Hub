using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VItemClassCategory
{
    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Class { get; set; }

    [Column("PkClassCatagoryID")]
    public Guid PkClassCatagoryId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ClassCatDescription { get; set; }

    [Column("PkClassID")]
    public Guid? PkClassId { get; set; }

    [StringLength(173)]
    [Unicode(false)]
    public string? DisplayValue { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ClassDescription { get; set; }

    [Column("FkOwnerEntityID")]
    public Guid? FkOwnerEntityId { get; set; }

    [Column("TAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taname { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string Status { get; set; } = null!;
}


