using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class User
{
    public Guid PkUserId { get; set; }

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public bool? Active { get; set; }

    public string? Uipersistance { get; set; }

    public string? Comment { get; set; }

    public bool? Enabled { get; set; }

    public Guid? FkGroupId { get; set; }

    public string? Name { get; set; }

    public bool? TermsAccepted { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public int? LoginAttempt { get; set; }

    public bool? Locked { get; set; }

    public string? Cellphone { get; set; }

    public Guid? FkDefaultAccountId { get; set; }

    public bool? EmailNotification { get; set; }

    public bool? SmsNotification { get; set; }

    public bool? IgnoreDuplicateLoginChecks { get; set; }

    public bool? IdentityReset { get; set; }

    public bool? Reviewed { get; set; }

    public string? ImageLocation { get; set; }

    public bool? GpayTermsAccepted { get; set; }

    public DateTime? PasswordExpiryDate { get; set; }

    public int? PasswordResetTerms { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<AccountChargeExludedEntityLog> AccountChargeExludedEntityLogs { get; set; } = new List<AccountChargeExludedEntityLog>();

    public virtual ICollection<AccountChargeLog> AccountChargeLogs { get; set; } = new List<AccountChargeLog>();

    public virtual ICollection<AccountChargeReversalLog> AccountChargeReversalLogs { get; set; } = new List<AccountChargeReversalLog>();

    public virtual ICollection<BatchBeneficiaryFile> BatchBeneficiaryFiles { get; set; } = new List<BatchBeneficiaryFile>();

    public virtual ICollection<BeneficiaryFile> BeneficiaryFiles { get; set; } = new List<BeneficiaryFile>();

    public virtual ICollection<ChargeUploadFile> ChargeUploadFiles { get; set; } = new List<ChargeUploadFile>();

    public virtual ICollection<ClientApproval> ClientApprovalFkOrderUsers { get; set; } = new List<ClientApproval>();

    public virtual ICollection<ClientApproval> ClientApprovalFkUsers { get; set; } = new List<ClientApproval>();

    public virtual ICollection<ClientProductApproval> ClientProductApprovals { get; set; } = new List<ClientProductApproval>();

    public virtual ICollection<EntityAccountChargeExlusionLog> EntityAccountChargeExlusionLogs { get; set; } = new List<EntityAccountChargeExlusionLog>();

    public virtual ICollection<EntityAccountChargeLog> EntityAccountChargeLogs { get; set; } = new List<EntityAccountChargeLog>();

    public virtual ICollection<EntityAccountChargeReversalLog> EntityAccountChargeReversalLogs { get; set; } = new List<EntityAccountChargeReversalLog>();

    public virtual ICollection<EntityBankStatementLog> EntityBankStatementLogs { get; set; } = new List<EntityBankStatementLog>();

    public virtual ICollection<EntityConfig> EntityConfigs { get; set; } = new List<EntityConfig>();

    public virtual ICollection<EntityContactPending> EntityContactPendings { get; set; } = new List<EntityContactPending>();

    public virtual ICollection<EntityContact> EntityContacts { get; set; } = new List<EntityContact>();

    public virtual ICollection<EntityDataApproval> EntityDataApprovalFkFromUsers { get; set; } = new List<EntityDataApproval>();

    public virtual ICollection<EntityDataApproval> EntityDataApprovalFkUsers { get; set; } = new List<EntityDataApproval>();

    public virtual ICollection<EntityPending> EntityPendings { get; set; } = new List<EntityPending>();

    public virtual ICollection<EntityRecordApproval> EntityRecordApprovals { get; set; } = new List<EntityRecordApproval>();

    public virtual ICollection<EntityStatusLog> EntityStatusLogs { get; set; } = new List<EntityStatusLog>();

    public virtual ICollection<EtguuserType> EtguuserTypes { get; set; } = new List<EtguuserType>();

    public virtual Group? FkGroup { get; set; }

    public virtual ICollection<LoginAudit> LoginAudits { get; set; } = new List<LoginAudit>();

    public virtual ICollection<MobileMartResponse> MobileMartResponses { get; set; } = new List<MobileMartResponse>();

    public virtual ICollection<OrderApproval> OrderApprovals { get; set; } = new List<OrderApproval>();

    public virtual ICollection<OrderFile> OrderFileFkLastUpdatedUsers { get; set; } = new List<OrderFile>();

    public virtual ICollection<OrderFile> OrderFileFkUploadUsers { get; set; } = new List<OrderFile>();

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual ICollection<OrderItemApproval> OrderItemApprovals { get; set; } = new List<OrderItemApproval>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<OutwardBopcategory> OutwardBopcategoryCreatedByNavigations { get; set; } = new List<OutwardBopcategory>();

    public virtual ICollection<OutwardBopcategory> OutwardBopcategoryUpdatedByNavigations { get; set; } = new List<OutwardBopcategory>();

    public virtual ICollection<PaymentRequest> PaymentRequests { get; set; } = new List<PaymentRequest>();

    public virtual ICollection<RewardsUploadFile> RewardsUploadFiles { get; set; } = new List<RewardsUploadFile>();

    public virtual ICollection<UserEntity> UserEntities { get; set; } = new List<UserEntity>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    public virtual ICollection<UserNedbankWallet> UserNedbankWallets { get; set; } = new List<UserNedbankWallet>();

    public virtual ICollection<UserPasswordChangeLog> UserPasswordChangeLogs { get; set; } = new List<UserPasswordChangeLog>();

    public virtual ICollection<UserSecurityAnswer> UserSecurityAnswers { get; set; } = new List<UserSecurityAnswer>();

    public virtual ICollection<UserSelection> UserSelections { get; set; } = new List<UserSelection>();

    public virtual ICollection<UserSupplierCustomPrice> UserSupplierCustomPrices { get; set; } = new List<UserSupplierCustomPrice>();

    public virtual ICollection<UserUserType> UserUserTypes { get; set; } = new List<UserUserType>();
}
