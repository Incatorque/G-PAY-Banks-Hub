using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class CountryBank
{
    public int CountryBankId { get; set; }

    public Guid? BankId { get; set; }

    public int? CountryId { get; set; }

    public virtual Bank? Bank { get; set; }

    public virtual Country? Country { get; set; }
}
