using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VAllCountryBank
{
    public int? CountryId { get; set; }

    public string BankName { get; set; } = null!;

    public Guid PkBankId { get; set; }

    public int? CountryBankId { get; set; }
}
