using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VSupplierClientAccount
{
    [Column("PkSupplierClientAccountID")]
    public Guid PkSupplierClientAccountId { get; set; }

    [StringLength(183)]
    public string? SupplierAccountNumber { get; set; }

    [StringLength(183)]
    public string? ClientAccountNumber { get; set; }

    [Column("FkSupplierClientID")]
    public Guid FkSupplierClientId { get; set; }

    [Column("FkClientAccountID")]
    public Guid FkClientAccountId { get; set; }

    [Column("FkSupplierAccountID")]
    public Guid FkSupplierAccountId { get; set; }
}


