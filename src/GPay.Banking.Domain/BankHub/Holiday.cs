using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Holiday")]
[Index("Date", Name = "ncIdx_Holiday_Date")]
public partial class Holiday : Entity<int>
{
    [Key]
    [Column("ID")]
    public override int Id { get; protected set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? Note { get; set; }
}


