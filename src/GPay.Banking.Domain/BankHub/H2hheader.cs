using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("H2HHeader")]
public partial class H2hheader : Entity<int>
{
    [Key]
    [Column("PkSequenceID")]
    public override int Id { get; protected set; }

    public int? Sequence { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SendDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ErrorMessage { get; set; }

    public bool? Error { get; set; }

    public bool? IncreaseSequence { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? IntAudDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? FinAudDate { get; set; }
}


