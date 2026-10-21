using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("HelpContent")]
public partial class HelpContent : Entity<Guid>
{
    [Key]
    [Column("PkHelpContentID")]
    public override Guid Id { get; protected set; }

    [Column("LegacyID")]
    public int LegacyId { get; set; }

    [StringLength(64)]
    [Unicode(false)]
    public string Title { get; set; } = null!;

    public string BodyText { get; set; } = null!;

    public bool? Active { get; set; }
}


