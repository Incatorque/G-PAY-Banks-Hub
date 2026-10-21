using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VLineChartDatum
{
    [StringLength(50)]
    [Unicode(false)]
    public string? Class { get; set; }

    [Column("PkClassID")]
    public Guid? PkClassId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Amount { get; set; }

    [Column("PkEntityBankStatementID")]
    public Guid PkEntityBankStatementId { get; set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column("FkClassCategoryID")]
    public Guid? FkClassCategoryId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TransactionDate { get; set; }
}


