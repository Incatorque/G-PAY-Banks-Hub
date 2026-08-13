using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class CountryCurrency
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public int CurrencyId { get; set; }
}
