using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Entity")]
[Index("Id", Name = "AK_Entity_ID", IsUnique = true)]
[Index("Active", "FkStatusId", Name = "NCI_Active_FKStatusID_PKEntityID_Name")]
public partial class CustomEntity : Entity<Guid>
{
    [Key]
    [Column("PkEntityID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("TAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taname { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? RegistrationNumber { get; set; }

    [Column("VATNumber")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Vatnumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastSyncDate { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comments { get; set; }

    [StringLength(2500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("FkStatusID")]
    public Guid FkStatusId { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    public bool? Featured { get; set; }

    [Column("FkEntitySubTypeID")]
    public Guid? FkEntitySubTypeId { get; set; }

    [Column("IDNumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Idnumber { get; set; }

    [Column("VATExemption")]
    public bool? Vatexemption { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? RejectReason { get; set; }

    [Column("WBulkPaymentType")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WbulkPaymentType { get; set; }

    [Column("WBulkLimitSgn")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WbulkLimitSgn { get; set; }

    [Column("ID")]
    public int OtherId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdated { get; set; }

    [Column("VATRegistered")]
    public bool? Vatregistered { get; set; }

    [Column("FkIdentificationTypeID")]
    public Guid? FkIdentificationTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? EntityReferenceAlias { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    public bool? IsNew { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ProfileExpiryDate { get; set; }

    [Column("IsCMConsumer")]
    public bool? IsCmconsumer { get; set; }

    public bool? Deleted { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Hash { get; set; }

    [InverseProperty("Fkentity")]
    public virtual ICollection<AccountChargeExludedEntity> AccountChargeExludedEntities { get; set; } = new List<AccountChargeExludedEntity>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();

    [InverseProperty("FkAgent")]
    public virtual ICollection<AgentSupplier> AgentSupplierFkAgents { get; set; } = new List<AgentSupplier>();

    [InverseProperty("FkSupplier")]
    public virtual ICollection<AgentSupplier> AgentSupplierFkSuppliers { get; set; } = new List<AgentSupplier>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<BatchBeneficiaryFile> BatchBeneficiaryFiles { get; set; } = new List<BatchBeneficiaryFile>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<BeneficiaryFile> BeneficiaryFiles { get; set; } = new List<BeneficiaryFile>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<ChargeUploadFile> ChargeUploadFiles { get; set; } = new List<ChargeUploadFile>();

    [InverseProperty("FkClient")]
    public virtual ICollection<ClientApproval> ClientApprovals { get; set; } = new List<ClientApproval>();

    [InverseProperty("FkClient")]
    public virtual ICollection<ClientProductApproval> ClientProductApprovals { get; set; } = new List<ClientProductApproval>();

    [InverseProperty("Fkentity")]
    public virtual ICollection<DashboardConfig> DashboardConfigs { get; set; } = new List<DashboardConfig>();

    [InverseProperty("FkledgerEntity")]
    public virtual ICollection<DashboardLedgerLink> DashboardLedgerLinks { get; set; } = new List<DashboardLedgerLink>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityAccount> EntityAccounts { get; set; } = new List<EntityAccount>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityBankInfo> EntityBankInfos { get; set; } = new List<EntityBankInfo>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityBankStatement> EntityBankStatements { get; set; } = new List<EntityBankStatement>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityConfig> EntityConfigFkEntities { get; set; } = new List<EntityConfig>();

    [InverseProperty("FkEntityIdFeesNavigation")]
    public virtual ICollection<EntityConfig> EntityConfigFkEntityIdFeesNavigations { get; set; } = new List<EntityConfig>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityContact> EntityContacts { get; set; } = new List<EntityContact>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityDataApproval> EntityDataApprovals { get; set; } = new List<EntityDataApproval>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityEntityType> EntityEntityTypes { get; set; } = new List<EntityEntityType>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityGroup> EntityGroups { get; set; } = new List<EntityGroup>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityModule> EntityModules { get; set; } = new List<EntityModule>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityOrderSweep> EntityOrderSweeps { get; set; } = new List<EntityOrderSweep>();

    [InverseProperty("Fkentity")]
    public virtual ICollection<EntityRecordApproval> EntityRecordApprovals { get; set; } = new List<EntityRecordApproval>();

    [InverseProperty("Fkentity")]
    public virtual ICollection<EntityStatusLog> EntityStatusLogs { get; set; } = new List<EntityStatusLog>();

    [InverseProperty("FkAgent")]
    public virtual ICollection<OrderCommission> OrderCommissionFkAgents { get; set; } = new List<OrderCommission>();

    [InverseProperty("FkClient")]
    public virtual ICollection<OrderCommission> OrderCommissionFkClients { get; set; } = new List<OrderCommission>();

    [InverseProperty("FkAllocatedVendor")]
    public virtual ICollection<Order> OrderFkAllocatedVendors { get; set; } = new List<Order>();

    [InverseProperty("FkClient")]
    public virtual ICollection<Order> OrderFkClients { get; set; } = new List<Order>();

    [InverseProperty("FkWholesaler")]
    public virtual ICollection<Order> OrderFkWholesalers { get; set; } = new List<Order>();

    [InverseProperty("FkAgent")]
    public virtual ICollection<OrderItemCommission> OrderItemCommissions { get; set; } = new List<OrderItemCommission>();

    [InverseProperty("FkAgent")]
    public virtual ICollection<ProductCommission> ProductCommissions { get; set; } = new List<ProductCommission>();

    [InverseProperty("FkSupplier")]
    public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();

    [InverseProperty("FkAgentNavigation")]
    public virtual ICollection<ProductZoneCommission> ProductZoneCommissions { get; set; } = new List<ProductZoneCommission>();

    [InverseProperty("Fkentity")]
    public virtual ICollection<RewardsUploadFile> RewardsUploadFiles { get; set; } = new List<RewardsUploadFile>();

    [InverseProperty("FkClient")]
    public virtual ICollection<SupplierClient> SupplierClientFkClients { get; set; } = new List<SupplierClient>();

    [InverseProperty("FkSupplier")]
    public virtual ICollection<SupplierClient> SupplierClientFkSuppliers { get; set; } = new List<SupplierClient>();

    [InverseProperty("FkAgent")]
    public virtual ICollection<SupplierCommission> SupplierCommissionFkAgents { get; set; } = new List<SupplierCommission>();

    [InverseProperty("FkClient")]
    public virtual ICollection<SupplierCommission> SupplierCommissionFkClients { get; set; } = new List<SupplierCommission>();

    [InverseProperty("FkSupplier")]
    public virtual ICollection<SupplierCommission> SupplierCommissionFkSuppliers { get; set; } = new List<SupplierCommission>();

    [InverseProperty("FkSupplier")]
    public virtual ICollection<SupplierZone> SupplierZones { get; set; } = new List<SupplierZone>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<UserEntity> UserEntities { get; set; } = new List<UserEntity>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<UserSelection> UserSelectionFkEntities { get; set; } = new List<UserSelection>();

    [InverseProperty("FkVendor")]
    public virtual ICollection<UserSelection> UserSelectionFkVendors { get; set; } = new List<UserSelection>();

    [InverseProperty("FkSupplier")]
    public virtual ICollection<UserSupplierCustomPrice> UserSupplierCustomPrices { get; set; } = new List<UserSupplierCustomPrice>();
}


