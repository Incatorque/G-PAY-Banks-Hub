using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VTxCodeMap
{
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(200)]
    public string Term { get; set; } = null!;

    [StringLength(30)]
    [Unicode(false)]
    public string TransactionCode { get; set; } = null!;

    public int TransactionCodeId { get; set; }

    public bool? Active { get; set; }
}


