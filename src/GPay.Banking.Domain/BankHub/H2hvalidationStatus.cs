using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("H2HValidationStatus")]
public partial class H2hvalidationStatus : Entity<Guid>
{
    [Key]
    [Column("PkH2HValidationStatusID")]
    public override Guid Id { get; protected set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [InverseProperty("FkH2hvalidationStatus")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}


