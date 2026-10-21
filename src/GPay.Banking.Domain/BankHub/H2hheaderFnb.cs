using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("H2HHeaderFNB")]
public partial class H2hheaderFnb : Entity<int>
{
    [Key]
    [Column("PkSequenceID")]
    public override int Id { get; protected set; }

    public int? FileSequence { get; set; }

    public int? HeaderSequence { get; set; }

    public int? TransactionSequence { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SendDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ErrorMessage { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserCode { get; set; }

    [StringLength(18)]
    [Unicode(false)]
    public string? AmountHash { get; set; }

    [StringLength(18)]
    [Unicode(false)]
    public string? AccountHash { get; set; }
}


