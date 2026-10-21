using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderAction")]
public partial class OrderAction : Entity<Guid>
{
    [Key]
    [Column("PkOrderActionID")]
    public override Guid Id { get; protected set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? Name { get; set; }
}


