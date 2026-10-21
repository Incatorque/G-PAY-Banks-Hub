using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ISOPainMessagesFNB")]
public partial class IsopainMessagesFnb : Entity<int>
{
    [Key]
    [Column("PKIsoPainMessageId")]
    public override int Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string MessageId { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime MessageDate { get; set; }

    public bool? Pending { get; set; }

    public bool? Accepted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AcceptedDate { get; set; }

    public bool? Rejected { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RejectedDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? ErrorMessage { get; set; }
}


