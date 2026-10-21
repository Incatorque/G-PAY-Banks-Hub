using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Plan")]
public partial class Plan : Entity<Guid>
{
    [Key]
    [Column("PkPlanId")]
    public override Guid Id { get; protected set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Description { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkAccountId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ExpirationDate { get; set; }

    public Guid FkPlanStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreationDate { get; set; }

    public Guid FkOrderId { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastRequisitionDate { get; set; }
}


