using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("H2HHeaderNedbank")]
public partial class H2hheaderNedbank : Entity<int>
{
    [Key]
    [Column("PkSequenceID")]
    public override int Id { get; protected set; }

    public int FileSequence { get; set; }

    public int HeaderSequence { get; set; }

    public long TransactionSequence { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SendDate { get; set; }

    [StringLength(50)]
    public string? ErrorMessage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ClientCode { get; set; } = null!;
}


