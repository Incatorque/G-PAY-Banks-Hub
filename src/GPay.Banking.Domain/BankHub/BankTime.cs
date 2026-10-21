using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BankTime")]
public partial class BankTime : Entity<Guid>
{
    [Key]
    [Column("PkBankTimeID")]
    public override Guid Id { get; protected set; }

    [Column("FkBankID")]
    public Guid FkBankId { get; set; }

    public bool? IsInternal { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "decimal(16, 4)")]
    public decimal? Amount { get; set; }

    [ForeignKey("FkBankId")]
    [InverseProperty("BankTimes")]
    public virtual Bank FkBank { get; set; } = null!;
}


