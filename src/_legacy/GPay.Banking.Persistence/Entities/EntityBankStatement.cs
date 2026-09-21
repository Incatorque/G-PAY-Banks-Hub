using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityBankStatement
{
    public Guid PkEntityBankStatementId { get; set; }

    public Guid? FkBankInfoId { get; set; }

    public string? Description { get; set; }

    public string? ReferenceNumber { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal Amount { get; set; }

    public DateTime SyncDate { get; set; }

    public bool? Reconciled { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkAccountId { get; set; }

    public bool? IsIntra { get; set; }

    public string? EventNumber { get; set; }

    public Guid? FkClassCategoryId { get; set; }

    public bool? IsProcessed { get; set; }

    public DateTime? DateMapped { get; set; }

    public int TransactionCodeId { get; set; }

    public string? SupplierBankReference { get; set; }

    public bool? Active { get; set; }

    public bool? Restored { get; set; }

    public virtual ICollection<ChargeLog> ChargeLogs { get; set; } = new List<ChargeLog>();

    public virtual ICollection<EntityBankStatementLog> EntityBankStatementLogs { get; set; } = new List<EntityBankStatementLog>();

    public virtual Account? FkAccount { get; set; }

    public virtual Entity? FkEntity { get; set; }

    public virtual ICollection<MobileMartResponse> MobileMartResponses { get; set; } = new List<MobileMartResponse>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<StatementRelation> StatementRelationFkEntityBankStatements { get; set; } = new List<StatementRelation>();

    public virtual ICollection<StatementRelation> StatementRelationFkParentEntityBankStatements { get; set; } = new List<StatementRelation>();

    public virtual ICollection<SweepLog> SweepLogs { get; set; } = new List<SweepLog>();

    public virtual TransactionCode TransactionCode { get; set; } = null!;

    public virtual ICollection<TransactionMappingRuleLink> TransactionMappingRuleLinks { get; set; } = new List<TransactionMappingRuleLink>();
}
