using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("NedSMTCode")]
public partial class NedSmtcode : Entity<int>
{
    [Key]
    [Column("PkNedSMSCodeId")]
    public override int Id { get; protected set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(250)]
    public string Description { get; set; } = null!;
}


