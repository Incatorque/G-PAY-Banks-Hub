using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderCommissionListBase
{
    [Column("PkOrderID")]
    public Guid PkOrderId { get; set; }

    [Column("FkAgentID")]
    public Guid FkAgentId { get; set; }

    [Column(TypeName = "decimal(38, 4)")]
    public decimal? DeliveredQuantity { get; set; }

    [Column("VAT Incl", TypeName = "decimal(38, 7)")]
    public decimal? VatIncl { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal TotalPercComm { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal TotalFixedComm { get; set; }

    [Column("TotalPerQComm", TypeName = "decimal(19, 4)")]
    public decimal TotalPerQcomm { get; set; }
}


