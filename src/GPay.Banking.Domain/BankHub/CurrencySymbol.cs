using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class CurrencySymbol
{
    [StringLength(50)]
    public string? Country { get; set; }

    [StringLength(50)]
    public string? Currency { get; set; }

    [Column("Currency_Code")]
    [StringLength(50)]
    public string? CurrencyCode { get; set; }

    [StringLength(50)]
    public string? Symbol { get; set; }

    [Column("Windows_Shortcut")]
    [StringLength(50)]
    public string? WindowsShortcut { get; set; }

    [Column("Mac_Shortcut")]
    [StringLength(50)]
    public string? MacShortcut { get; set; }

    [Column("cc_iso")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CcIso { get; set; }
}


