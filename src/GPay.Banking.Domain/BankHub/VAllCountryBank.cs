using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VAllCountryBank
{
    public int? CountryId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string BankName { get; set; } = null!;

    [Column("PkBankID")]
    public Guid PkBankId { get; set; }

    public int? CountryBankId { get; set; }
}


