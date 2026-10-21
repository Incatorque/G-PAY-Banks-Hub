using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("CommunicationType")]
public partial class CommunicationType : Entity<Guid>
{
    [Key]
    [Column("PkCommTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public Guid? FkMailTemplateId { get; set; }
}


