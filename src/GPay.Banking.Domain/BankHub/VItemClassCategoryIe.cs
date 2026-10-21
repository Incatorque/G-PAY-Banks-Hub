using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VItemClassCategoryIe
{
    [Column("PkClassCatagoryID")]
    public Guid PkClassCatagoryId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? ClassCatDescription { get; set; }

    public bool? Active { get; set; }

    [Column("FkClassID")]
    public Guid? FkClassId { get; set; }

    public bool? Included { get; set; }
}


