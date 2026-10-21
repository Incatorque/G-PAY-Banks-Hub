using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VPlanning
{
    public Guid PkPlanId { get; set; }

    public Guid FkAccountId { get; set; }

    public Guid FkEntityId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime ExpirationDate { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PlanStatus { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? LastRequisitionDate { get; set; }

    [Column(TypeName = "numeric(38, 6)")]
    public decimal? PlanValue { get; set; }

    public Guid FkPlanStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreationDate { get; set; }

    public bool? AddedToBasketFromPlan { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? PlanNumber { get; set; }

    public bool? Active { get; set; }

    [Column("PkOrderID")]
    public Guid PkOrderId { get; set; }
}


