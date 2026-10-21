using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("SweepChangeLog")]
public partial class SweepChangeLog : Entity<Guid>
{
    [Key]
    [Column("PkSweepChangeLogID")]
    public override Guid Id { get; protected set; }

    [Column("FkSweepID")]
    public Guid? FkSweepId { get; set; }

    [Column("FkUserID")]
    public Guid? FkUserId { get; set; }

    [Unicode(false)]
    public string? LogDescription { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LogDate { get; set; }
}


