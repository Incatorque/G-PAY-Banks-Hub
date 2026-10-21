using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("IntegrationError")]
public partial class IntegrationError : Entity<Guid>
{
    [Key]
    [Column("PkIntegrationErrorID")]
    public override Guid Id { get; protected set; }

    [StringLength(5000)]
    [Unicode(false)]
    public string ErrorMessage { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime EntryDateTime { get; set; }
}


