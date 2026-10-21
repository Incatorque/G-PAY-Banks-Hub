using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VShare
{
    public Guid FkOrderId { get; set; }

    public int PkShareId { get; set; }

    public bool? Ceded { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CededDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    public int OrderNumber { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Quantity { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }
}


