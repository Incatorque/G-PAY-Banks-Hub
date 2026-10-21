using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VCountryCurrency
{
    public int Id { get; set; }

    [StringLength(100)]
    public string CountryName { get; set; } = null!;

    [Column("CountryID")]
    public int CountryId { get; set; }

    public int CurrencyId { get; set; }

    [Column("cc_iso")]
    [StringLength(2)]
    public string CcIso { get; set; } = null!;

    [Column("code")]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(106)]
    public string DisplayValue { get; set; } = null!;

    public bool? Active { get; set; }

    [StringLength(50)]
    public string Symbol { get; set; } = null!;
}


