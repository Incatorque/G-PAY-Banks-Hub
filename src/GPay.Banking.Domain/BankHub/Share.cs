using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class Share : Entity<int>
{
    [Key]
    [Column("PkShareId")]
    public override int Id { get; protected set; }

    public Guid FkOrderId { get; set; }

    public bool? Ceded { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CededDate { get; set; }
}


