using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Currency")]
public partial class Currency : Entity<int>
{
    [Key]
    [Column("ID")]
    public override int Id { get; protected set; }

    [Column("code")]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column("number")]
    [StringLength(3)]
    [Unicode(false)]
    public string Number { get; set; } = null!;

    [Column("country")]
    [StringLength(2)]
    [Unicode(false)]
    public string? Country { get; set; }

    [Column("cc_iso")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CcIso { get; set; }
}


