using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AgentSupplier")]
public partial class AgentSupplier : Entity<Guid>
{
    [Key]
    [Column("PkAgentSupplierID")]
    public override Guid Id { get; protected set; }

    [Column("FkAgentID")]
    public Guid FkAgentId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [ForeignKey("FkAgentId")]
    [InverseProperty("AgentSupplierFkAgents")]
    public virtual CustomEntity FkAgent { get; set; } = null!;

    [ForeignKey("FkSupplierId")]
    [InverseProperty("AgentSupplierFkSuppliers")]
    public virtual CustomEntity FkSupplier { get; set; } = null!;
}


