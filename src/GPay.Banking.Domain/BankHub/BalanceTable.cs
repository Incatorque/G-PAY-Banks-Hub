using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[PrimaryKey("AccountId", "BalanceDate")]
[Table("BalanceTable")]
public partial class BalanceTable : Entity<Guid>
{
    [Key]
    [Column("Account_ID")]
    public override Guid Id { get; protected set; }

    [Key]
    [Column("Balance_Date")]
    public DateOnly BalanceDate { get; set; }

    [Column("Opening_Balance", TypeName = "decimal(18, 2)")]
    public decimal OpeningBalance { get; set; }

    [Column("Closing_Balance", TypeName = "decimal(18, 2)")]
    public decimal ClosingBalance { get; set; }

    [Column("Cumulative_Debits", TypeName = "decimal(18, 2)")]
    public decimal? CumulativeDebits { get; set; }

    [Column("Cumulative_Credits", TypeName = "decimal(18, 2)")]
    public decimal? CumulativeCredits { get; set; }

    [Column("Last_Updated")]
    public DateTime? LastUpdated { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Source { get; set; }
}


