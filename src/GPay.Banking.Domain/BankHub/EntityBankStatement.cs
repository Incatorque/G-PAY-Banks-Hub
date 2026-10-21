using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityBankStatement")]
[Index("FkAccountId", "TransactionCodeId", "Active", "TransactionDate", Name = "EntityBankStatement_GETGRAPHDATA")]
[Index("Reconciled", Name = "IX_EntityBankStatement")]
public partial class EntityBankStatement : Entity<Guid>
{
    [Key]
    [Column("PkEntityBankStatementID")]
    public override Guid Id { get; protected set; }

    [Column("FkBankInfoID")]
    public Guid? FkBankInfoId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TransactionDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SyncDate { get; set; }

    public bool? Reconciled { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    public bool? IsIntra { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? EventNumber { get; set; }

    [Column("FkClassCategoryID")]
    public Guid? FkClassCategoryId { get; set; }

    public bool? IsProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateMapped { get; set; }

    public int TransactionCodeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierBankReference { get; set; }

    public bool? Active { get; set; }

    public bool? Restored { get; set; }

    [InverseProperty("FkentityBankStatementNavigation")]
    public virtual ICollection<ChargeLog> ChargeLogs { get; set; } = new List<ChargeLog>();

    [InverseProperty("FkentityBankStatement")]
    public virtual ICollection<EntityBankStatementLog> EntityBankStatementLogs { get; set; } = new List<EntityBankStatementLog>();

    [ForeignKey("FkAccountId")]
    [InverseProperty("EntityBankStatements")]
    public virtual Account? FkAccount { get; set; }

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityBankStatements")]
    public virtual CustomEntity? FkEntity { get; set; }

    [InverseProperty("FkReverseEntityBankStatement")]
    public virtual ICollection<MobileMartResponse> MobileMartResponses { get; set; } = new List<MobileMartResponse>();

    [InverseProperty("FkEntityBankStatement")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("FkEntityBankStatement")]
    public virtual ICollection<StatementRelation> StatementRelationFkEntityBankStatements { get; set; } = new List<StatementRelation>();

    [InverseProperty("FkParentEntityBankStatement")]
    public virtual ICollection<StatementRelation> StatementRelationFkParentEntityBankStatements { get; set; } = new List<StatementRelation>();

    [InverseProperty("EntityBankStatement")]
    public virtual ICollection<SweepLog> SweepLogs { get; set; } = new List<SweepLog>();

    [ForeignKey("TransactionCodeId")]
    [InverseProperty("EntityBankStatements")]
    public virtual TransactionCode TransactionCode { get; set; } = null!;

    [InverseProperty("FkEntityBankStatement")]
    public virtual ICollection<TransactionMappingRuleLink> TransactionMappingRuleLinks { get; set; } = new List<TransactionMappingRuleLink>();
}


