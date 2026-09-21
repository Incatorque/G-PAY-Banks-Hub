using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Country
{
    public int CountryId { get; set; }

    public string CcFips { get; set; } = null!;

    public string CcIso { get; set; } = null!;

    public string Tld { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public virtual ICollection<CountryBank> CountryBanks { get; set; } = new List<CountryBank>();
}
