using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityAccountLedger_Test")]
public partial class EntityAccountLedgerTest : Entity<long>
{
    [Key]
    [Column("LedgerID")]
    public override long Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkAccountID")]
    public Guid FkAccountId { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string SourceType { get; set; } = null!;

    [Column("SourceID")]
    public Guid? SourceId { get; set; }

    public DateTime CreatedOn { get; set; }
}


