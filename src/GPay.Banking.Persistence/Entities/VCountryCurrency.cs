using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VCountryCurrency
{
    public int Id { get; set; }

    public string CountryName { get; set; } = null!;

    public int CountryId { get; set; }

    public int CurrencyId { get; set; }

    public string CcIso { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string DisplayValue { get; set; } = null!;

    public bool? Active { get; set; }

    public string Symbol { get; set; } = null!;
}
