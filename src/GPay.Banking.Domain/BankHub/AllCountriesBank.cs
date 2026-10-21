using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
[Index("Bic", Name = "IX_AllCountriesBanks_BIC")]
[Index("CountryIsocode2", Name = "IX_AllCountriesBanks_Country")]
[Index("BankName", Name = "IX_AllCountriesBansk_BankName")]
public partial class AllCountriesBank
{
    [Column("CountryISOCode2")]
    [StringLength(2)]
    [Unicode(false)]
    public string CountryIsocode2 { get; set; } = null!;

    [Column("BIC")]
    [StringLength(11)]
    [Unicode(false)]
    public string Bic { get; set; } = null!;

    [StringLength(201)]
    public string BankName { get; set; } = null!;

    [StringLength(35)]
    public string? City { get; set; }
}


