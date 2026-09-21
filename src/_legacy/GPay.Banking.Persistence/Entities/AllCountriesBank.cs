using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AllCountriesBank
{
    public string CountryIsocode2 { get; set; } = null!;

    public string Bic { get; set; } = null!;

    public string BankName { get; set; } = null!;

    public string? City { get; set; }
}
