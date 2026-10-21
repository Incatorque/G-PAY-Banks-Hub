using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
[Table("DataSheet")]
public partial class DataSheet
{
    [StringLength(255)]
    public string? Image { get; set; }

    [StringLength(255)]
    public string? Code { get; set; }

    [Column("Short Name")]
    [StringLength(255)]
    public string? ShortName { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [StringLength(255)]
    public string? Specification { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Size { get; set; }

    [Column("UNIT")]
    [StringLength(255)]
    public string? Unit { get; set; }

    [Column("NO IN PACKAGE", TypeName = "decimal(19, 4)")]
    public decimal? NoInPackage { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column("Product Category", TypeName = "decimal(19, 4)")]
    public decimal? ProductCategory { get; set; }
}


