using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ProductZoneCommission")]
public partial class ProductZoneCommission : Entity<Guid>
{
    [Key]
    [Column("PkProductZoneCommissionID")]
    public override Guid Id { get; protected set; }

    [Column("FkProductZoneID")]
    public Guid FkProductZoneId { get; set; }

    [Column("FkAgentID")]
    public Guid FkAgentId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Value { get; set; }

    [Column("FkCommissionTypeID")]
    public Guid FkCommissionTypeId { get; set; }

    public int Order { get; set; }

    [ForeignKey("FkAgentId")]
    [InverseProperty("ProductZoneCommissions")]
    public virtual Agent FkAgent { get; set; } = null!;

    [ForeignKey("FkAgentId")]
    [InverseProperty("ProductZoneCommissions")]
    public virtual CustomEntity FkAgentNavigation { get; set; } = null!;

    [ForeignKey("FkCommissionTypeId")]
    [InverseProperty("ProductZoneCommissions")]
    public virtual CommissionType FkCommissionType { get; set; } = null!;

    [ForeignKey("FkProductZoneId")]
    [InverseProperty("ProductZoneCommissions")]
    public virtual ProductZone FkProductZone { get; set; } = null!;
}


