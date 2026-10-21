using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("IpAddress")]
public partial class IpAddress : Entity<Guid>
{
    [Key]
    [Column("PkIpAddressID")]
    public override Guid Id { get; protected set; }

    [Column("IpAddress")]
    [StringLength(50)]
    [Unicode(false)]
    public string IpAddress1 { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateAdded { get; set; }

    public bool? Active { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Description { get; set; }

    public bool? InUse { get; set; }
}


