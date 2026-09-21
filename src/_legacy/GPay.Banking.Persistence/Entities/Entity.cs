using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Entity
{
    public Guid PkEntityId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Taname { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Vatnumber { get; set; }

    public DateTime? LastSyncDate { get; set; }

    public string? Comments { get; set; }

    public string? Description { get; set; }

    public Guid FkStatusId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public bool? Featured { get; set; }

    public Guid? FkEntitySubTypeId { get; set; }

    public string? Idnumber { get; set; }

    public bool? Vatexemption { get; set; }

    public string? RejectReason { get; set; }

    public string? WbulkPaymentType { get; set; }

    public string? WbulkLimitSgn { get; set; }

    public int Id { get; set; }

    public DateTime? LastUpdated { get; set; }

    public bool? Vatregistered { get; set; }

    public Guid? FkIdentificationTypeId { get; set; }

    public string? EntityReferenceAlias { get; set; }

    public string? ImageLocation { get; set; }

    public bool? IsNew { get; set; }

    public DateTime? ProfileExpiryDate { get; set; }

    public bool? IsCmconsumer { get; set; }

    public bool? Deleted { get; set; }

    public string? Hash { get; set; }

    public virtual ICollection<AccountChargeExludedEntity> AccountChargeExludedEntities { get; set; } = new List<AccountChargeExludedEntity>();

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    public virtual ICollection<AgentSupplier> AgentSupplierFkAgents { get; set; } = new List<AgentSupplier>();

    public virtual ICollection<AgentSupplier> AgentSupplierFkSuppliers { get; set; } = new List<AgentSupplier>();

    public virtual ICollection<BatchBeneficiaryFile> BatchBeneficiaryFiles { get; set; } = new List<BatchBeneficiaryFile>();

    public virtual ICollection<BeneficiaryFile> BeneficiaryFiles { get; set; } = new List<BeneficiaryFile>();

    public virtual ICollection<ChargeUploadFile> ChargeUploadFiles { get; set; } = new List<ChargeUploadFile>();

    public virtual ICollection<ClientApproval> ClientApprovals { get; set; } = new List<ClientApproval>();

    public virtual ICollection<ClientProductApproval> ClientProductApprovals { get; set; } = new List<ClientProductApproval>();

    public virtual ICollection<DashboardConfig> DashboardConfigs { get; set; } = new List<DashboardConfig>();

    public virtual ICollection<DashboardLedgerLink> DashboardLedgerLinks { get; set; } = new List<DashboardLedgerLink>();

    public virtual ICollection<EntityAccount> EntityAccounts { get; set; } = new List<EntityAccount>();

    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();

    public virtual ICollection<EntityBankInfo> EntityBankInfos { get; set; } = new List<EntityBankInfo>();

    public virtual ICollection<EntityBankStatement> EntityBankStatements { get; set; } = new List<EntityBankStatement>();

    public virtual ICollection<EntityConfig> EntityConfigFkEntities { get; set; } = new List<EntityConfig>();

    public virtual ICollection<EntityConfig> EntityConfigFkEntityIdFeesNavigations { get; set; } = new List<EntityConfig>();

    public virtual ICollection<EntityContact> EntityContacts { get; set; } = new List<EntityContact>();

    public virtual ICollection<EntityDataApproval> EntityDataApprovals { get; set; } = new List<EntityDataApproval>();

    public virtual ICollection<EntityEntityType> EntityEntityTypes { get; set; } = new List<EntityEntityType>();

    public virtual ICollection<EntityGroup> EntityGroups { get; set; } = new List<EntityGroup>();

    public virtual ICollection<EntityModule> EntityModules { get; set; } = new List<EntityModule>();

    public virtual ICollection<EntityOrderSweep> EntityOrderSweeps { get; set; } = new List<EntityOrderSweep>();

    public virtual ICollection<EntityRecordApproval> EntityRecordApprovals { get; set; } = new List<EntityRecordApproval>();

    public virtual ICollection<EntityStatusLog> EntityStatusLogs { get; set; } = new List<EntityStatusLog>();

    public virtual ICollection<OrderCommission> OrderCommissionFkAgents { get; set; } = new List<OrderCommission>();

    public virtual ICollection<OrderCommission> OrderCommissionFkClients { get; set; } = new List<OrderCommission>();

    public virtual ICollection<Order> OrderFkAllocatedVendors { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderFkClients { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderFkWholesalers { get; set; } = new List<Order>();

    public virtual ICollection<OrderItemCommission> OrderItemCommissions { get; set; } = new List<OrderItemCommission>();

    public virtual ICollection<ProductCommission> ProductCommissions { get; set; } = new List<ProductCommission>();

    public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();

    public virtual ICollection<ProductZoneCommission> ProductZoneCommissions { get; set; } = new List<ProductZoneCommission>();

    public virtual ICollection<RewardsUploadFile> RewardsUploadFiles { get; set; } = new List<RewardsUploadFile>();

    public virtual ICollection<SupplierClient> SupplierClientFkClients { get; set; } = new List<SupplierClient>();

    public virtual ICollection<SupplierClient> SupplierClientFkSuppliers { get; set; } = new List<SupplierClient>();

    public virtual ICollection<SupplierCommission> SupplierCommissionFkAgents { get; set; } = new List<SupplierCommission>();

    public virtual ICollection<SupplierCommission> SupplierCommissionFkClients { get; set; } = new List<SupplierCommission>();

    public virtual ICollection<SupplierCommission> SupplierCommissionFkSuppliers { get; set; } = new List<SupplierCommission>();

    public virtual ICollection<SupplierZone> SupplierZones { get; set; } = new List<SupplierZone>();

    public virtual ICollection<UserEntity> UserEntities { get; set; } = new List<UserEntity>();

    public virtual ICollection<UserSelection> UserSelectionFkEntities { get; set; } = new List<UserSelection>();

    public virtual ICollection<UserSelection> UserSelectionFkVendors { get; set; } = new List<UserSelection>();

    public virtual ICollection<UserSupplierCustomPrice> UserSupplierCustomPrices { get; set; } = new List<UserSupplierCustomPrice>();
}
