using System;
using System.Collections.Generic;
using GPay.Banking.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.Persistence.Data;

public partial class BankingDbContext : DbContext
{
    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbpAuditLog> AbpAuditLogs { get; set; }

    public virtual DbSet<AbpAuditLogAction> AbpAuditLogActions { get; set; }

    public virtual DbSet<AbpAuditLogExcelFile> AbpAuditLogExcelFiles { get; set; }

    public virtual DbSet<AbpBackgroundJob> AbpBackgroundJobs { get; set; }

    public virtual DbSet<AbpBlob> AbpBlobs { get; set; }

    public virtual DbSet<AbpBlobContainer> AbpBlobContainers { get; set; }

    public virtual DbSet<AbpClaimType> AbpClaimTypes { get; set; }

    public virtual DbSet<AbpEntityChange> AbpEntityChanges { get; set; }

    public virtual DbSet<AbpEntityPropertyChange> AbpEntityPropertyChanges { get; set; }

    public virtual DbSet<AbpFeature> AbpFeatures { get; set; }

    public virtual DbSet<AbpFeatureGroup> AbpFeatureGroups { get; set; }

    public virtual DbSet<AbpFeatureValue> AbpFeatureValues { get; set; }

    public virtual DbSet<AbpLinkUser> AbpLinkUsers { get; set; }

    public virtual DbSet<AbpOrganizationUnit> AbpOrganizationUnits { get; set; }

    public virtual DbSet<AbpOrganizationUnitRole> AbpOrganizationUnitRoles { get; set; }

    public virtual DbSet<AbpPermission> AbpPermissions { get; set; }

    public virtual DbSet<AbpPermissionGrant> AbpPermissionGrants { get; set; }

    public virtual DbSet<AbpPermissionGroup> AbpPermissionGroups { get; set; }

    public virtual DbSet<AbpRole> AbpRoles { get; set; }

    public virtual DbSet<AbpRoleClaim> AbpRoleClaims { get; set; }

    public virtual DbSet<AbpSecurityLog> AbpSecurityLogs { get; set; }

    public virtual DbSet<AbpSession> AbpSessions { get; set; }

    public virtual DbSet<AbpSetting> AbpSettings { get; set; }

    public virtual DbSet<AbpSettingDefinition> AbpSettingDefinitions { get; set; }

    public virtual DbSet<AbpTenant> AbpTenants { get; set; }

    public virtual DbSet<AbpTenantConnectionString> AbpTenantConnectionStrings { get; set; }

    public virtual DbSet<AbpUser> AbpUsers { get; set; }

    public virtual DbSet<AbpUserClaim> AbpUserClaims { get; set; }

    public virtual DbSet<AbpUserDelegation> AbpUserDelegations { get; set; }

    public virtual DbSet<AbpUserLogin> AbpUserLogins { get; set; }

    public virtual DbSet<AbpUserOrganizationUnit> AbpUserOrganizationUnits { get; set; }

    public virtual DbSet<AbpUserRole> AbpUserRoles { get; set; }

    public virtual DbSet<AbpUserToken> AbpUserTokens { get; set; }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountCharge> AccountCharges { get; set; }

    public virtual DbSet<AccountChargeAccumulatedAmountLog> AccountChargeAccumulatedAmountLogs { get; set; }

    public virtual DbSet<AccountChargeExludedEntity> AccountChargeExludedEntities { get; set; }

    public virtual DbSet<AccountChargeExludedEntityLog> AccountChargeExludedEntityLogs { get; set; }

    public virtual DbSet<AccountChargeLog> AccountChargeLogs { get; set; }

    public virtual DbSet<AccountChargeMapping> AccountChargeMappings { get; set; }

    public virtual DbSet<AccountChargeMappingLog> AccountChargeMappingLogs { get; set; }

    public virtual DbSet<AccountChargeReferenceMask> AccountChargeReferenceMasks { get; set; }

    public virtual DbSet<AccountChargeReversal> AccountChargeReversals { get; set; }

    public virtual DbSet<AccountChargeReversalLog> AccountChargeReversalLogs { get; set; }

    public virtual DbSet<AccountPending> AccountPendings { get; set; }

    public virtual DbSet<AccountStatus> AccountStatuses { get; set; }

    public virtual DbSet<AccountType> AccountTypes { get; set; }

    public virtual DbSet<AddressType> AddressTypes { get; set; }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<AgentSupplier> AgentSuppliers { get; set; }

    public virtual DbSet<AllCountriesBank> AllCountriesBanks { get; set; }

    public virtual DbSet<ApprovalStatusType> ApprovalStatusTypes { get; set; }

    public virtual DbSet<ApprovalType> ApprovalTypes { get; set; }

    public virtual DbSet<BalanceTable> BalanceTables { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankTime> BankTimes { get; set; }

    public virtual DbSet<BaseProduct> BaseProducts { get; set; }

    public virtual DbSet<BaseProductPrice> BaseProductPrices { get; set; }

    public virtual DbSet<BaseProductZone> BaseProductZones { get; set; }

    public virtual DbSet<BatchBeneficiaryFile> BatchBeneficiaryFiles { get; set; }

    public virtual DbSet<BeneficiaryFile> BeneficiaryFiles { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChargeLog> ChargeLogs { get; set; }

    public virtual DbSet<ChargeNotificationLog> ChargeNotificationLogs { get; set; }

    public virtual DbSet<ChargeUploadFile> ChargeUploadFiles { get; set; }

    public virtual DbSet<ClientApproval> ClientApprovals { get; set; }

    public virtual DbSet<ClientProductApproval> ClientProductApprovals { get; set; }

    public virtual DbSet<ClusterNotification> ClusterNotifications { get; set; }

    public virtual DbSet<CommissionType> CommissionTypes { get; set; }

    public virtual DbSet<CommunicationType> CommunicationTypes { get; set; }

    public virtual DbSet<ConfigAccountType> ConfigAccountTypes { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CountryBank> CountryBanks { get; set; }

    public virtual DbSet<CountryCurrency> CountryCurrencies { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CurrencySymbol> CurrencySymbols { get; set; }

    public virtual DbSet<CvLEb> CvLEbs { get; set; }

    public virtual DbSet<DashboardChartVisibilityLink> DashboardChartVisibilityLinks { get; set; }

    public virtual DbSet<DashboardColumnVisibility> DashboardColumnVisibilities { get; set; }

    public virtual DbSet<DashboardConfig> DashboardConfigs { get; set; }

    public virtual DbSet<DashboardFrequency> DashboardFrequencies { get; set; }

    public virtual DbSet<DashboardLedgerLink> DashboardLedgerLinks { get; set; }

    public virtual DbSet<DashboardTransactionCodeLink> DashboardTransactionCodeLinks { get; set; }

    public virtual DbSet<DashboardTransactionType> DashboardTransactionTypes { get; set; }

    public virtual DbSet<DashboardType> DashboardTypes { get; set; }

    public virtual DbSet<DashboardUnitOfMeasure> DashboardUnitOfMeasures { get; set; }

    public virtual DbSet<DataSheet> DataSheets { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DifferentialType> DifferentialTypes { get; set; }

    public virtual DbSet<DiscountType> DiscountTypes { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Entity> Entities { get; set; }

    public virtual DbSet<EntityAccount> EntityAccounts { get; set; }

    public virtual DbSet<EntityAccountCharge> EntityAccountCharges { get; set; }

    public virtual DbSet<EntityAccountChargeAccumulatedAmountLog> EntityAccountChargeAccumulatedAmountLogs { get; set; }

    public virtual DbSet<EntityAccountChargeExclusion> EntityAccountChargeExclusions { get; set; }

    public virtual DbSet<EntityAccountChargeExlusionLog> EntityAccountChargeExlusionLogs { get; set; }

    public virtual DbSet<EntityAccountChargeLog> EntityAccountChargeLogs { get; set; }

    public virtual DbSet<EntityAccountChargeMapping> EntityAccountChargeMappings { get; set; }

    public virtual DbSet<EntityAccountChargeMappingLog> EntityAccountChargeMappingLogs { get; set; }

    public virtual DbSet<EntityAccountChargeReversal> EntityAccountChargeReversals { get; set; }

    public virtual DbSet<EntityAccountChargeReversalLog> EntityAccountChargeReversalLogs { get; set; }

    public virtual DbSet<EntityAccountLedgerTest> EntityAccountLedgerTests { get; set; }

    public virtual DbSet<EntityAddress> EntityAddresses { get; set; }

    public virtual DbSet<EntityAddressPending> EntityAddressPendings { get; set; }

    public virtual DbSet<EntityBankInfo> EntityBankInfos { get; set; }

    public virtual DbSet<EntityBankInfoPending> EntityBankInfoPendings { get; set; }

    public virtual DbSet<EntityBankStatement> EntityBankStatements { get; set; }

    public virtual DbSet<EntityBankStatementLog> EntityBankStatementLogs { get; set; }

    public virtual DbSet<EntityBankStatementTemp> EntityBankStatementTemps { get; set; }

    public virtual DbSet<EntityConfig> EntityConfigs { get; set; }

    public virtual DbSet<EntityConfigAccount> EntityConfigAccounts { get; set; }

    public virtual DbSet<EntityContact> EntityContacts { get; set; }

    public virtual DbSet<EntityContactPending> EntityContactPendings { get; set; }

    public virtual DbSet<EntityDataApproval> EntityDataApprovals { get; set; }

    public virtual DbSet<EntityEntityType> EntityEntityTypes { get; set; }

    public virtual DbSet<EntityGroup> EntityGroups { get; set; }

    public virtual DbSet<EntityModule> EntityModules { get; set; }

    public virtual DbSet<EntityOrderSweep> EntityOrderSweeps { get; set; }

    public virtual DbSet<EntityOrderSweepLog> EntityOrderSweepLogs { get; set; }

    public virtual DbSet<EntityPending> EntityPendings { get; set; }

    public virtual DbSet<EntityRecordApproval> EntityRecordApprovals { get; set; }

    public virtual DbSet<EntityStatus> EntityStatuses { get; set; }

    public virtual DbSet<EntityStatusLog> EntityStatusLogs { get; set; }

    public virtual DbSet<EntitySubType> EntitySubTypes { get; set; }

    public virtual DbSet<EntityType> EntityTypes { get; set; }

    public virtual DbSet<EtguuserType> EtguuserTypes { get; set; }

    public virtual DbSet<Fnb3PimApiCallLog> Fnb3PimApiCallLogs { get; set; }

    public virtual DbSet<Fnb3PimImportWatermark> Fnb3PimImportWatermarks { get; set; }

    public virtual DbSet<Fnb3PimOrderQueue> Fnb3PimOrderQueues { get; set; }

    public virtual DbSet<Frequency> Frequencies { get; set; }

    public virtual DbSet<GenericLookUpTable> GenericLookUpTables { get; set; }

    public virtual DbSet<GetProductTransactionCodeId> GetProductTransactionCodeIds { get; set; }

    public virtual DbSet<GlobalPublicRecipient> GlobalPublicRecipients { get; set; }

    public virtual DbSet<Grid> Grids { get; set; }

    public virtual DbSet<GridZone> GridZones { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<H2hheader> H2hheaders { get; set; }

    public virtual DbSet<H2hheaderAbsa> H2hheaderAbsas { get; set; }

    public virtual DbSet<H2hheaderFnb> H2hheaderFnbs { get; set; }

    public virtual DbSet<H2hheaderNedbank> H2hheaderNedbanks { get; set; }

    public virtual DbSet<H2hvalidationStatus> H2hvalidationStatuses { get; set; }

    public virtual DbSet<HelpContent> HelpContents { get; set; }

    public virtual DbSet<HistoryCategory> HistoryCategories { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<IdentificationType> IdentificationTypes { get; set; }

    public virtual DbSet<IntegratedSystem> IntegratedSystems { get; set; }

    public virtual DbSet<IntegrationError> IntegrationErrors { get; set; }

    public virtual DbSet<IpAddress> IpAddresses { get; set; }

    public virtual DbSet<IsopainMessagesAbsa> IsopainMessagesAbsas { get; set; }

    public virtual DbSet<IsopainMessagesFnb> IsopainMessagesFnbs { get; set; }

    public virtual DbSet<ItemClass> ItemClasses { get; set; }

    public virtual DbSet<ItemClassCatIncluded> ItemClassCatIncludeds { get; set; }

    public virtual DbSet<ItemClassCatagory> ItemClassCatagories { get; set; }

    public virtual DbSet<LoginAudit> LoginAudits { get; set; }

    public virtual DbSet<LowBalanceNotification> LowBalanceNotifications { get; set; }

    public virtual DbSet<MagisterialDistrict> MagisterialDistricts { get; set; }

    public virtual DbSet<MagisterialDistrictGridZone> MagisterialDistrictGridZones { get; set; }

    public virtual DbSet<MailTemplate> MailTemplates { get; set; }

    public virtual DbSet<MainView> MainViews { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Menu1> Menus1 { get; set; }

    public virtual DbSet<MobileMartAccountStatus> MobileMartAccountStatuses { get; set; }

    public virtual DbSet<MobileMartProductsCategory> MobileMartProductsCategories { get; set; }

    public virtual DbSet<MobileMartResponse> MobileMartResponses { get; set; }

    public virtual DbSet<MobileMartUtilityConsumerDetail> MobileMartUtilityConsumerDetails { get; set; }

    public virtual DbSet<MobileMartUtilityMunicipalityDetail> MobileMartUtilityMunicipalityDetails { get; set; }

    public virtual DbSet<MobileMartUtilityToken> MobileMartUtilityTokens { get; set; }

    public virtual DbSet<MobileMartUtilityUnit> MobileMartUtilityUnits { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<NedSmtcode> NedSmtcodes { get; set; }

    public virtual DbSet<OpenIddictApplication> OpenIddictApplications { get; set; }

    public virtual DbSet<OpenIddictAuthorization> OpenIddictAuthorizations { get; set; }

    public virtual DbSet<OpenIddictScope> OpenIddictScopes { get; set; }

    public virtual DbSet<OpenIddictToken> OpenIddictTokens { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderAction> OrderActions { get; set; }

    public virtual DbSet<OrderApproval> OrderApprovals { get; set; }

    public virtual DbSet<OrderCommission> OrderCommissions { get; set; }

    public virtual DbSet<OrderCommissionStatementRelation> OrderCommissionStatementRelations { get; set; }

    public virtual DbSet<OrderFile> OrderFiles { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderItemApproval> OrderItemApprovals { get; set; }

    public virtual DbSet<OrderItemCommission> OrderItemCommissions { get; set; }

    public virtual DbSet<OrderItemReference> OrderItemReferences { get; set; }

    public virtual DbSet<OrderItemStatus> OrderItemStatuses { get; set; }

    public virtual DbSet<OrderStatementRelation> OrderStatementRelations { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrderSweepFrequency> OrderSweepFrequencies { get; set; }

    public virtual DbSet<OrderType> OrderTypes { get; set; }

    public virtual DbSet<OutwardBopcategory> OutwardBopcategories { get; set; }

    public virtual DbSet<PaymentRequest> PaymentRequests { get; set; }

    public virtual DbSet<Plan> Plans { get; set; }

    public virtual DbSet<PlanStatus> PlanStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductAllocationOverridge> ProductAllocationOverridges { get; set; }

    public virtual DbSet<ProductCommission> ProductCommissions { get; set; }

    public virtual DbSet<ProductExcluded> ProductExcludeds { get; set; }

    public virtual DbSet<ProductStatus> ProductStatuses { get; set; }

    public virtual DbSet<ProductSupplier> ProductSuppliers { get; set; }

    public virtual DbSet<ProductZone> ProductZones { get; set; }

    public virtual DbSet<ProductZoneCommission> ProductZoneCommissions { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<RewardsUploadFile> RewardsUploadFiles { get; set; }

    public virtual DbSet<RuleType> RuleTypes { get; set; }

    public virtual DbSet<Share> Shares { get; set; }

    public virtual DbSet<Signal> Signals { get; set; }

    public virtual DbSet<StatementRelation> StatementRelations { get; set; }

    public virtual DbSet<StatementRelationType> StatementRelationTypes { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<SupplierClient> SupplierClients { get; set; }

    public virtual DbSet<SupplierClientAccount> SupplierClientAccounts { get; set; }

    public virtual DbSet<SupplierCommission> SupplierCommissions { get; set; }

    public virtual DbSet<SupplierZone> SupplierZones { get; set; }

    public virtual DbSet<Sweep> Sweeps { get; set; }

    public virtual DbSet<SweepChangeLog> SweepChangeLogs { get; set; }

    public virtual DbSet<SweepLog> SweepLogs { get; set; }

    public virtual DbSet<TermsAndConditionsAudit> TermsAndConditionsAudits { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<Town> Towns { get; set; }

    public virtual DbSet<TownSupplierZone> TownSupplierZones { get; set; }

    public virtual DbSet<TransactionCode> TransactionCodes { get; set; }

    public virtual DbSet<TransactionCodeMap> TransactionCodeMaps { get; set; }

    public virtual DbSet<TransactionMappingRule> TransactionMappingRules { get; set; }

    public virtual DbSet<TransactionMappingRuleLink> TransactionMappingRuleLinks { get; set; }

    public virtual DbSet<TransactionStore> TransactionStores { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UploadHistory> UploadHistories { get; set; }

    public virtual DbSet<UploadReference> UploadReferences { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserEntity> UserEntities { get; set; }

    public virtual DbSet<UserGroup> UserGroups { get; set; }

    public virtual DbSet<UserNedbankWallet> UserNedbankWallets { get; set; }

    public virtual DbSet<UserPasswordChangeLog> UserPasswordChangeLogs { get; set; }

    public virtual DbSet<UserPin> UserPins { get; set; }

    public virtual DbSet<UserSecurityAnswer> UserSecurityAnswers { get; set; }

    public virtual DbSet<UserSecurityQuestion> UserSecurityQuestions { get; set; }

    public virtual DbSet<UserSelection> UserSelections { get; set; }

    public virtual DbSet<UserSupplierCustomPrice> UserSupplierCustomPrices { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<UserTypeModule> UserTypeModules { get; set; }

    public virtual DbSet<UserTypeUserType> UserTypeUserTypes { get; set; }

    public virtual DbSet<UserUserType> UserUserTypes { get; set; }

    public virtual DbSet<VAccount> VAccounts { get; set; }

    public virtual DbSet<VAccountPending> VAccountPendings { get; set; }

    public virtual DbSet<VAgentSupplier> VAgentSuppliers { get; set; }

    public virtual DbSet<VAllCountryBank> VAllCountryBanks { get; set; }

    public virtual DbSet<VBankStatementGbbilist> VBankStatementGbbilists { get; set; }

    public virtual DbSet<VBankTime> VBankTimes { get; set; }

    public virtual DbSet<VBaseProductPrice> VBaseProductPrices { get; set; }

    public virtual DbSet<VBasket> VBaskets { get; set; }

    public virtual DbSet<VClientApproval> VClientApprovals { get; set; }

    public virtual DbSet<VClientBankStatement> VClientBankStatements { get; set; }

    public virtual DbSet<VClientDataApproval> VClientDataApprovals { get; set; }

    public virtual DbSet<VClientSupplier> VClientSuppliers { get; set; }

    public virtual DbSet<VClientTotal> VClientTotals { get; set; }

    public virtual DbSet<VClusterNotification> VClusterNotifications { get; set; }

    public virtual DbSet<VCommissionList> VCommissionLists { get; set; }

    public virtual DbSet<VCountryBank> VCountryBanks { get; set; }

    public virtual DbSet<VCountryCurrency> VCountryCurrencies { get; set; }

    public virtual DbSet<VDevice> VDevices { get; set; }

    public virtual DbSet<VDeviceLink> VDeviceLinks { get; set; }

    public virtual DbSet<VEntityAccount> VEntityAccounts { get; set; }

    public virtual DbSet<VEntityAccountLink> VEntityAccountLinks { get; set; }

    public virtual DbSet<VEntityAccountOptimized> VEntityAccountOptimizeds { get; set; }

    public virtual DbSet<VEntityAgentList> VEntityAgentLists { get; set; }

    public virtual DbSet<VEntityBankStatement> VEntityBankStatements { get; set; }

    public virtual DbSet<VEntityBankStatementDeleted> VEntityBankStatementDeleteds { get; set; }

    public virtual DbSet<VEntityBankStatementIaccount> VEntityBankStatementIaccounts { get; set; }

    public virtual DbSet<VEntityBankStatementMappedToClass> VEntityBankStatementMappedToClasses { get; set; }

    public virtual DbSet<VEntityBankStatementMapping> VEntityBankStatementMappings { get; set; }

    public virtual DbSet<VEntityBankStatementMvU> VEntityBankStatementMvUs { get; set; }

    public virtual DbSet<VEntityBeneficiaryList> VEntityBeneficiaryLists { get; set; }

    public virtual DbSet<VEntityBeneficiaryListDistinct> VEntityBeneficiaryListDistincts { get; set; }

    public virtual DbSet<VEntityClientList> VEntityClientLists { get; set; }

    public virtual DbSet<VEntityCompactList> VEntityCompactLists { get; set; }

    public virtual DbSet<VEntityEntitiesList> VEntityEntitiesLists { get; set; }

    public virtual DbSet<VEntityEntityTypeList> VEntityEntityTypeLists { get; set; }

    public virtual DbSet<VEntityFeesConfig> VEntityFeesConfigs { get; set; }

    public virtual DbSet<VEntityList> VEntityLists { get; set; }

    public virtual DbSet<VEntityOrderSweep> VEntityOrderSweeps { get; set; }

    public virtual DbSet<VEntityRight> VEntityRights { get; set; }

    public virtual DbSet<VEntitySupplierClient> VEntitySupplierClients { get; set; }

    public virtual DbSet<VEntitySupplierList> VEntitySupplierLists { get; set; }

    public virtual DbSet<VEntityWithdrawalList> VEntityWithdrawalLists { get; set; }

    public virtual DbSet<VGoodsFeesPerEntity> VGoodsFeesPerEntities { get; set; }

    public virtual DbSet<VGridZone> VGridZones { get; set; }

    public virtual DbSet<VGroup> VGroups { get; set; }

    public virtual DbSet<VItemClass> VItemClasses { get; set; }

    public virtual DbSet<VItemClassCatIncluded> VItemClassCatIncludeds { get; set; }

    public virtual DbSet<VItemClassCatLinkedToEntity> VItemClassCatLinkedToEntities { get; set; }

    public virtual DbSet<VItemClassCategory> VItemClassCategories { get; set; }

    public virtual DbSet<VItemClassCategoryIe> VItemClassCategoryIes { get; set; }

    public virtual DbSet<VLineChartDatum> VLineChartData { get; set; }

    public virtual DbSet<VLoginAudit> VLoginAudits { get; set; }

    public virtual DbSet<VMagisterialDistrictProvince> VMagisterialDistrictProvinces { get; set; }

    public virtual DbSet<VMyPlan> VMyPlans { get; set; }

    public virtual DbSet<VOrderApprovalList> VOrderApprovalLists { get; set; }

    public virtual DbSet<VOrderCommissionList> VOrderCommissionLists { get; set; }

    public virtual DbSet<VOrderCommissionListBase> VOrderCommissionListBases { get; set; }

    public virtual DbSet<VOrderHistory> VOrderHistories { get; set; }

    public virtual DbSet<VOrderItemCommissionList> VOrderItemCommissionLists { get; set; }

    public virtual DbSet<VOrderItemCommissionListBase> VOrderItemCommissionListBases { get; set; }

    public virtual DbSet<VOrderItemGboclist> VOrderItemGboclists { get; set; }

    public virtual DbSet<VOrderItemGbolist> VOrderItemGbolists { get; set; }

    public virtual DbSet<VOrderItemList> VOrderItemLists { get; set; }

    public virtual DbSet<VOrderItemListBase> VOrderItemListBases { get; set; }

    public virtual DbSet<VOrderItemListSupplier> VOrderItemListSuppliers { get; set; }

    public virtual DbSet<VOrderItemOrder> VOrderItemOrders { get; set; }

    public virtual DbSet<VOrderList> VOrderLists { get; set; }

    public virtual DbSet<VOrderListChart> VOrderListCharts { get; set; }

    public virtual DbSet<VOrderOfPaymentGrandTotalsPerMonth> VOrderOfPaymentGrandTotalsPerMonths { get; set; }

    public virtual DbSet<VOrderOfPaymentView> VOrderOfPaymentViews { get; set; }

    public virtual DbSet<VOrderSupplier> VOrderSuppliers { get; set; }

    public virtual DbSet<VOrderSweepFrequency> VOrderSweepFrequencies { get; set; }

    public virtual DbSet<VPlanning> VPlannings { get; set; }

    public virtual DbSet<VProductApproval> VProductApprovals { get; set; }

    public virtual DbSet<VProductEntity> VProductEntities { get; set; }

    public virtual DbSet<VProductEntityAllAvailableProduct> VProductEntityAllAvailableProducts { get; set; }

    public virtual DbSet<VProductEntityWithExtra> VProductEntityWithExtras { get; set; }

    public virtual DbSet<VProductExcluded> VProductExcludeds { get; set; }

    public virtual DbSet<VProductSupplierZone> VProductSupplierZones { get; set; }

    public virtual DbSet<VRequisitionApproval> VRequisitionApprovals { get; set; }

    public virtual DbSet<VRewardsUploadFile> VRewardsUploadFiles { get; set; }

    public virtual DbSet<VShare> VShares { get; set; }

    public virtual DbSet<VSubscription> VSubscriptions { get; set; }

    public virtual DbSet<VSupplierClient> VSupplierClients { get; set; }

    public virtual DbSet<VSupplierClientAccount> VSupplierClientAccounts { get; set; }

    public virtual DbSet<VSupplierClientCu> VSupplierClientCus { get; set; }

    public virtual DbSet<VSupplierClientFilteredOnExcludedProduct> VSupplierClientFilteredOnExcludedProducts { get; set; }

    public virtual DbSet<VSupplierClientForPaymentPriority> VSupplierClientForPaymentPriorities { get; set; }

    public virtual DbSet<VSupplierClientForPaymentPriorityOop> VSupplierClientForPaymentPriorityOops { get; set; }

    public virtual DbSet<VSupplierClientList> VSupplierClientLists { get; set; }

    public virtual DbSet<VSupplierClientRefrenceNo> VSupplierClientRefrenceNos { get; set; }

    public virtual DbSet<VSupplierClientSu> VSupplierClientSus { get; set; }

    public virtual DbSet<VSweepRecord> VSweepRecords { get; set; }

    public virtual DbSet<VTermsAndConditionsAudit> VTermsAndConditionsAudits { get; set; }

    public virtual DbSet<VTownProvince> VTownProvinces { get; set; }

    public virtual DbSet<VTransactionMappingRule> VTransactionMappingRules { get; set; }

    public virtual DbSet<VTxCodeMap> VTxCodeMaps { get; set; }

    public virtual DbSet<VUnallocatedFund> VUnallocatedFunds { get; set; }

    public virtual DbSet<VUploadHistory> VUploadHistories { get; set; }

    public virtual DbSet<VUploadReference> VUploadReferences { get; set; }

    public virtual DbSet<VUserAgent> VUserAgents { get; set; }

    public virtual DbSet<VUserClient> VUserClients { get; set; }

    public virtual DbSet<VUserClientAddress> VUserClientAddresses { get; set; }

    public virtual DbSet<VUserEntity> VUserEntities { get; set; }

    public virtual DbSet<VUserGroup> VUserGroups { get; set; }

    public virtual DbSet<VUserGroupRight> VUserGroupRights { get; set; }

    public virtual DbSet<VUserSupplier> VUserSuppliers { get; set; }

    public virtual DbSet<VUserSuppliersClient> VUserSuppliersClients { get; set; }

    public virtual DbSet<VUserTypesAllowedPerUser> VUserTypesAllowedPerUsers { get; set; }

    public virtual DbSet<VallProduct> VallProducts { get; set; }

    public virtual DbSet<Vatamount> Vatamounts { get; set; }

    public virtual DbSet<VbaseProductAll> VbaseProductAlls { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VentityApprovalList> VentityApprovalLists { get; set; }

    public virtual DbSet<VetguuserType> VetguuserTypes { get; set; }

    public virtual DbSet<VproductAllocationOver> VproductAllocationOvers { get; set; }

    public virtual DbSet<VuserSecurity> VuserSecurities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbpAuditLog>(entity =>
        {
            entity.HasIndex(e => new { e.TenantId, e.ExecutionTime }, "IX_AbpAuditLogs_TenantId_ExecutionTime");

            entity.HasIndex(e => new { e.TenantId, e.UserId, e.ExecutionTime }, "IX_AbpAuditLogs_TenantId_UserId_ExecutionTime");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApplicationName).HasMaxLength(96);
            entity.Property(e => e.BrowserInfo).HasMaxLength(512);
            entity.Property(e => e.ClientId).HasMaxLength(64);
            entity.Property(e => e.ClientIpAddress).HasMaxLength(64);
            entity.Property(e => e.ClientName).HasMaxLength(128);
            entity.Property(e => e.Comments).HasMaxLength(256);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.CorrelationId).HasMaxLength(64);
            entity.Property(e => e.HttpMethod).HasMaxLength(16);
            entity.Property(e => e.ImpersonatorTenantName).HasMaxLength(64);
            entity.Property(e => e.ImpersonatorUserName).HasMaxLength(256);
            entity.Property(e => e.TenantName).HasMaxLength(64);
            entity.Property(e => e.Url).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<AbpAuditLogAction>(entity =>
        {
            entity.HasIndex(e => e.AuditLogId, "IX_AbpAuditLogActions_AuditLogId");

            entity.HasIndex(e => new { e.TenantId, e.ServiceName, e.MethodName, e.ExecutionTime }, "IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_ExecutionTime");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.MethodName).HasMaxLength(128);
            entity.Property(e => e.Parameters).HasMaxLength(2000);
            entity.Property(e => e.ServiceName).HasMaxLength(256);

            entity.HasOne(d => d.AuditLog).WithMany(p => p.AbpAuditLogActions).HasForeignKey(d => d.AuditLogId);
        });

        modelBuilder.Entity<AbpAuditLogExcelFile>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FileName).HasMaxLength(256);
        });

        modelBuilder.Entity<AbpBackgroundJob>(entity =>
        {
            entity.HasIndex(e => new { e.IsAbandoned, e.NextTryTime }, "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApplicationName).HasMaxLength(96);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.IsAbandoned)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.JobName).HasMaxLength(128);
            entity.Property(e => e.Priority).HasDefaultValueSql("(CONVERT([tinyint],(15),0))");
            entity.Property(e => e.TryCount).HasDefaultValueSql("(CONVERT([smallint],(0),0))");
        });

        modelBuilder.Entity<AbpBlob>(entity =>
        {
            entity.HasIndex(e => e.ContainerId, "IX_AbpBlobs_ContainerId");

            entity.HasIndex(e => new { e.TenantId, e.ContainerId, e.Name }, "IX_AbpBlobs_TenantId_ContainerId_Name");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.Name).HasMaxLength(256);

            entity.HasOne(d => d.Container).WithMany(p => p.AbpBlobs).HasForeignKey(d => d.ContainerId);
        });

        modelBuilder.Entity<AbpBlobContainer>(entity =>
        {
            entity.HasIndex(e => new { e.TenantId, e.Name }, "IX_AbpBlobContainers_TenantId_Name");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<AbpClaimType>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Regex).HasMaxLength(512);
            entity.Property(e => e.RegexDescription).HasMaxLength(128);
        });

        modelBuilder.Entity<AbpEntityChange>(entity =>
        {
            entity.HasIndex(e => e.AuditLogId, "IX_AbpEntityChanges_AuditLogId");

            entity.HasIndex(e => new { e.TenantId, e.EntityTypeFullName, e.EntityId }, "IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EntityId).HasMaxLength(128);
            entity.Property(e => e.EntityTypeFullName).HasMaxLength(128);

            entity.HasOne(d => d.AuditLog).WithMany(p => p.AbpEntityChanges).HasForeignKey(d => d.AuditLogId);
        });

        modelBuilder.Entity<AbpEntityPropertyChange>(entity =>
        {
            entity.HasIndex(e => e.EntityChangeId, "IX_AbpEntityPropertyChanges_EntityChangeId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.NewValue).HasMaxLength(512);
            entity.Property(e => e.OriginalValue).HasMaxLength(512);
            entity.Property(e => e.PropertyName).HasMaxLength(128);
            entity.Property(e => e.PropertyTypeFullName).HasMaxLength(64);

            entity.HasOne(d => d.EntityChange).WithMany(p => p.AbpEntityPropertyChanges).HasForeignKey(d => d.EntityChangeId);
        });

        modelBuilder.Entity<AbpFeature>(entity =>
        {
            entity.HasIndex(e => e.GroupName, "IX_AbpFeatures_GroupName");

            entity.HasIndex(e => e.Name, "IX_AbpFeatures_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AllowedProviders).HasMaxLength(256);
            entity.Property(e => e.DefaultValue).HasMaxLength(256);
            entity.Property(e => e.Description).HasMaxLength(256);
            entity.Property(e => e.DisplayName).HasMaxLength(256);
            entity.Property(e => e.GroupName).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.ParentName).HasMaxLength(128);
            entity.Property(e => e.ValueType).HasMaxLength(2048);
        });

        modelBuilder.Entity<AbpFeatureGroup>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_AbpFeatureGroups_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DisplayName).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<AbpFeatureValue>(entity =>
        {
            entity.HasIndex(e => new { e.Name, e.ProviderName, e.ProviderKey }, "IX_AbpFeatureValues_Name_ProviderName_ProviderKey")
                .IsUnique()
                .HasFilter("([ProviderName] IS NOT NULL AND [ProviderKey] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(64);
            entity.Property(e => e.ProviderName).HasMaxLength(64);
            entity.Property(e => e.Value).HasMaxLength(128);
        });

        modelBuilder.Entity<AbpLinkUser>(entity =>
        {
            entity.HasIndex(e => new { e.SourceUserId, e.SourceTenantId, e.TargetUserId, e.TargetTenantId }, "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_TargetTenantId")
                .IsUnique()
                .HasFilter("([SourceTenantId] IS NOT NULL AND [TargetTenantId] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<AbpOrganizationUnit>(entity =>
        {
            entity.HasIndex(e => e.Code, "IX_AbpOrganizationUnits_Code");

            entity.HasIndex(e => e.ParentId, "IX_AbpOrganizationUnits_ParentId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(95);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.DisplayName).HasMaxLength(128);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent).HasForeignKey(d => d.ParentId);
        });

        modelBuilder.Entity<AbpOrganizationUnitRole>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationUnitId, e.RoleId });

            entity.HasIndex(e => new { e.RoleId, e.OrganizationUnitId }, "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId");

            entity.HasOne(d => d.OrganizationUnit).WithMany(p => p.AbpOrganizationUnitRoles).HasForeignKey(d => d.OrganizationUnitId);

            entity.HasOne(d => d.Role).WithMany(p => p.AbpOrganizationUnitRoles).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AbpPermission>(entity =>
        {
            entity.HasIndex(e => e.GroupName, "IX_AbpPermissions_GroupName");

            entity.HasIndex(e => e.Name, "IX_AbpPermissions_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DisplayName).HasMaxLength(256);
            entity.Property(e => e.GroupName).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.ParentName).HasMaxLength(128);
            entity.Property(e => e.Providers).HasMaxLength(128);
            entity.Property(e => e.StateCheckers).HasMaxLength(256);
        });

        modelBuilder.Entity<AbpPermissionGrant>(entity =>
        {
            entity.HasIndex(e => new { e.TenantId, e.Name, e.ProviderName, e.ProviderKey }, "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey")
                .IsUnique()
                .HasFilter("([TenantId] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(64);
            entity.Property(e => e.ProviderName).HasMaxLength(64);
        });

        modelBuilder.Entity<AbpPermissionGroup>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_AbpPermissionGroups_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DisplayName).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(128);
        });

        modelBuilder.Entity<AbpRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "IX_AbpRoles_NormalizedName");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AbpRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AbpRoleClaims_RoleId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClaimType).HasMaxLength(256);
            entity.Property(e => e.ClaimValue).HasMaxLength(1024);

            entity.HasOne(d => d.Role).WithMany(p => p.AbpRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AbpSecurityLog>(entity =>
        {
            entity.HasIndex(e => new { e.TenantId, e.Action }, "IX_AbpSecurityLogs_TenantId_Action");

            entity.HasIndex(e => new { e.TenantId, e.ApplicationName }, "IX_AbpSecurityLogs_TenantId_ApplicationName");

            entity.HasIndex(e => new { e.TenantId, e.Identity }, "IX_AbpSecurityLogs_TenantId_Identity");

            entity.HasIndex(e => new { e.TenantId, e.UserId }, "IX_AbpSecurityLogs_TenantId_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Action).HasMaxLength(96);
            entity.Property(e => e.ApplicationName).HasMaxLength(96);
            entity.Property(e => e.BrowserInfo).HasMaxLength(512);
            entity.Property(e => e.ClientId).HasMaxLength(64);
            entity.Property(e => e.ClientIpAddress).HasMaxLength(64);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.CorrelationId).HasMaxLength(64);
            entity.Property(e => e.Identity).HasMaxLength(96);
            entity.Property(e => e.TenantName).HasMaxLength(64);
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<AbpSession>(entity =>
        {
            entity.HasIndex(e => e.Device, "IX_AbpSessions_Device");

            entity.HasIndex(e => e.SessionId, "IX_AbpSessions_SessionId");

            entity.HasIndex(e => new { e.TenantId, e.UserId }, "IX_AbpSessions_TenantId_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClientId).HasMaxLength(64);
            entity.Property(e => e.Device).HasMaxLength(64);
            entity.Property(e => e.DeviceInfo).HasMaxLength(64);
            entity.Property(e => e.IpAddresses).HasMaxLength(2048);
            entity.Property(e => e.SessionId).HasMaxLength(128);
        });

        modelBuilder.Entity<AbpSetting>(entity =>
        {
            entity.HasIndex(e => new { e.Name, e.ProviderName, e.ProviderKey }, "IX_AbpSettings_Name_ProviderName_ProviderKey")
                .IsUnique()
                .HasFilter("([ProviderName] IS NOT NULL AND [ProviderKey] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(64);
            entity.Property(e => e.ProviderName).HasMaxLength(64);
            entity.Property(e => e.Value).HasMaxLength(2048);
        });

        modelBuilder.Entity<AbpSettingDefinition>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_AbpSettingDefinitions_Name").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DefaultValue).HasMaxLength(2048);
            entity.Property(e => e.Description).HasMaxLength(512);
            entity.Property(e => e.DisplayName).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.Providers).HasMaxLength(1024);
        });

        modelBuilder.Entity<AbpTenant>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_AbpTenants_Name");

            entity.HasIndex(e => e.NormalizedName, "IX_AbpTenants_NormalizedName");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.NormalizedName).HasMaxLength(64);
        });

        modelBuilder.Entity<AbpTenantConnectionString>(entity =>
        {
            entity.HasKey(e => new { e.TenantId, e.Name });

            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.Value).HasMaxLength(1024);

            entity.HasOne(d => d.Tenant).WithMany(p => p.AbpTenantConnectionStrings).HasForeignKey(d => d.TenantId);
        });

        modelBuilder.Entity<AbpUser>(entity =>
        {
            entity.HasIndex(e => e.Email, "IX_AbpUsers_Email");

            entity.HasIndex(e => e.NormalizedEmail, "IX_AbpUsers_NormalizedEmail");

            entity.HasIndex(e => e.NormalizedUserName, "IX_AbpUsers_NormalizedUserName");

            entity.HasIndex(e => e.UserName, "IX_AbpUsers_UserName");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.EmailConfirmed)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.IsExternal)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.LockoutEnabled)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.Name).HasMaxLength(64);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.PhoneNumber).HasMaxLength(16);
            entity.Property(e => e.PhoneNumberConfirmed)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.SecurityStamp).HasMaxLength(256);
            entity.Property(e => e.Surname).HasMaxLength(64);
            entity.Property(e => e.TwoFactorEnabled)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<AbpUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AbpUserClaims_UserId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ClaimType).HasMaxLength(256);
            entity.Property(e => e.ClaimValue).HasMaxLength(1024);

            entity.HasOne(d => d.User).WithMany(p => p.AbpUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AbpUserDelegation>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<AbpUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider });

            entity.HasIndex(e => new { e.LoginProvider, e.ProviderKey }, "IX_AbpUserLogins_LoginProvider_ProviderKey");

            entity.Property(e => e.LoginProvider).HasMaxLength(64);
            entity.Property(e => e.ProviderDisplayName).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(196);

            entity.HasOne(d => d.User).WithMany(p => p.AbpUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AbpUserOrganizationUnit>(entity =>
        {
            entity.HasKey(e => new { e.OrganizationUnitId, e.UserId });

            entity.HasIndex(e => new { e.UserId, e.OrganizationUnitId }, "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId");

            entity.HasOne(d => d.OrganizationUnit).WithMany(p => p.AbpUserOrganizationUnits).HasForeignKey(d => d.OrganizationUnitId);

            entity.HasOne(d => d.User).WithMany(p => p.AbpUserOrganizationUnits).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AbpUserRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasIndex(e => new { e.RoleId, e.UserId }, "IX_AbpUserRoles_RoleId_UserId");

            entity.HasOne(d => d.Role).WithMany(p => p.AbpUserRoles).HasForeignKey(d => d.RoleId);

            entity.HasOne(d => d.User).WithMany(p => p.AbpUserRoles).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AbpUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(64);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AbpUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.PkAccountId).HasFillFactor(90);

            entity.ToTable("Account");

            entity.HasIndex(e => new { e.AccountNumber, e.Active, e.ResourceType }, "AAR").HasFillFactor(90);

            entity.HasIndex(e => e.AccountNumber, "IX_Account").HasFillFactor(90);

            entity.Property(e => e.PkAccountId)
                .ValueGeneratedNever()
                .HasColumnName("PkAccountID");
            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.BankUserCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode).HasMaxLength(20);
            entity.Property(e => e.BranchName).HasMaxLength(50);
            entity.Property(e => e.CountryCurrencyId).HasColumnName("CountryCurrencyID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FacilityValue).HasColumnType("money");
            entity.Property(e => e.FkAccountStatusId).HasColumnName("FkAccountStatusID");
            entity.Property(e => e.FkAccountTypeId).HasColumnName("FkAccountTypeID");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.FkControllingEntityId).HasColumnName("FkControllingEntityID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.HasEntityAccountFacility).HasDefaultValue(false);
            entity.Property(e => e.HasFacility).HasDefaultValue(false);
            entity.Property(e => e.Is3Pim).HasColumnName("Is3PIM");
            entity.Property(e => e.MapAccount).HasDefaultValue(false);
            entity.Property(e => e.Primarary).HasDefaultValue(true);
            entity.Property(e => e.ResourceType).HasMaxLength(20);
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkAccountStatus).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.FkAccountStatusId)
                .HasConstraintName("FK_Account_AccountStatus");

            entity.HasOne(d => d.FkAccountType).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.FkAccountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__FkAccou__5D21AF45");

            entity.HasOne(d => d.FkBank).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.FkBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__FkBankI__5C2D8B0C");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.FkEntityId)
                .HasConstraintName("FK__Account__FkEntit__79D2FC8C");
        });

        modelBuilder.Entity<AccountCharge>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeId)
                .HasName("PK__AccountC__5E489CF4DE7EDDB9")
                .HasFillFactor(90);

            entity.ToTable("AccountCharge");

            entity.Property(e => e.PkAccountChargeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PkAccountChargeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ApplyOnDebit).HasDefaultValue(false);
            entity.Property(e => e.ChargeAccumulatedAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ChargeExpiryAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ChargeExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.ChargeStartDate).HasColumnType("datetime");
            entity.Property(e => e.ChargeType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkaccountId).HasColumnName("FKAccountID");
            entity.Property(e => e.FkentityAccountId).HasColumnName("FKEntityAccountID");
            entity.Property(e => e.Reference).HasMaxLength(50);
            entity.Property(e => e.TransactionCodeId)
                .HasDefaultValue(1)
                .HasColumnName("TransactionCodeID");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.Fkaccount).WithMany(p => p.AccountCharges)
                .HasForeignKey(d => d.FkaccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCh__FKAcc__63456846");

            entity.HasOne(d => d.TransactionCode).WithMany(p => p.AccountCharges)
                .HasForeignKey(d => d.TransactionCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCh__Trans__64398C7F");
        });

        modelBuilder.Entity<AccountChargeAccumulatedAmountLog>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeAccumulatedAmountLogId);

            entity.ToTable("AccountChargeAccumulatedAmountLog");

            entity.Property(e => e.PkAccountChargeAccumulatedAmountLogId).ValueGeneratedNever();
            entity.Property(e => e.AdjustmentAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ChargeDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkAccountCharge).WithMany(p => p.AccountChargeAccumulatedAmountLogs).HasForeignKey(d => d.FkAccountChargeId);
        });

        modelBuilder.Entity<AccountChargeExludedEntity>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeExludedEntities)
                .HasName("PK__AccountC__4429B4B3D3B1AE73")
                .HasFillFactor(90);

            entity.Property(e => e.PkAccountChargeExludedEntities).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkAccountChargeId).HasColumnName("FkAccountChargeID");
            entity.Property(e => e.FkentityId).HasColumnName("FKEntityID");

            entity.HasOne(d => d.FkAccountCharge).WithMany(p => p.AccountChargeExludedEntities)
                .HasForeignKey(d => d.FkAccountChargeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCh__FkAcc__6DC2F6B9");

            entity.HasOne(d => d.Fkentity).WithMany(p => p.AccountChargeExludedEntities)
                .HasForeignKey(d => d.FkentityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCh__FKEnt__6EB71AF2");
        });

        modelBuilder.Entity<AccountChargeExludedEntityLog>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeExludedEntityLogId);

            entity.ToTable("AccountChargeExludedEntityLog");

            entity.HasIndex(e => e.FkuserId, "IX_AccountChargeExludedEntityLog_FKUserId");

            entity.HasIndex(e => e.FkAccountChargeId, "IX_AccountChargeExludedEntityLog_FkAccountChargeId");

            entity.Property(e => e.PkAccountChargeExludedEntityLogId).ValueGeneratedNever();
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);

            entity.HasOne(d => d.FkAccountCharge).WithMany(p => p.AccountChargeExludedEntityLogs).HasForeignKey(d => d.FkAccountChargeId);

            entity.HasOne(d => d.Fkuser).WithMany(p => p.AccountChargeExludedEntityLogs).HasForeignKey(d => d.FkuserId);
        });

        modelBuilder.Entity<AccountChargeLog>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeLogId);

            entity.ToTable("AccountChargeLog");

            entity.HasIndex(e => e.FkuserId, "IX_AccountChargeLog_FKUserId");

            entity.HasIndex(e => e.FkAccountChargeId, "IX_AccountChargeLog_FkAccountChargeId");

            entity.Property(e => e.PkAccountChargeLogId).ValueGeneratedNever();
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);

            entity.HasOne(d => d.FkAccountCharge).WithMany(p => p.AccountChargeLogs).HasForeignKey(d => d.FkAccountChargeId);

            entity.HasOne(d => d.Fkuser).WithMany(p => p.AccountChargeLogs).HasForeignKey(d => d.FkuserId);
        });

        modelBuilder.Entity<AccountChargeMapping>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeMapping)
                .HasName("PK__AccountC__55AF4B6EAAB9E292")
                .HasFillFactor(90);

            entity.ToTable("AccountChargeMapping");

            entity.Property(e => e.PkAccountChargeMapping).HasDefaultValueSql("(newid())", "DF__AccountCh__PkAcc__6715F92A");
            entity.Property(e => e.ApplyCharge).HasDefaultValue(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())", "DF__AccountCh__Creat__680A1D63")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountChargeId).HasColumnName("FkAccountChargeID");
            entity.Property(e => e.Mask).HasMaxLength(50);
            entity.Property(e => e.SendNotification).HasDefaultValue(false);
            entity.Property(e => e.TransactionCodeId).HasColumnName("TransactionCodeID");

            entity.HasOne(d => d.FkAccountCharge).WithMany(p => p.AccountChargeMappings)
                .HasForeignKey(d => d.FkAccountChargeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCh__FkAcc__68FE419C");
        });

        modelBuilder.Entity<AccountChargeMappingLog>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeMappingLogId);

            entity.ToTable("AccountChargeMappingLog");

            entity.HasIndex(e => e.FkuserId, "IX_AccountChargeMappingLog_FKUserId");

            entity.HasIndex(e => e.FkAccountChargeId, "IX_AccountChargeMappingLog_FkAccountChargeId");

            entity.HasIndex(e => e.FkAccountChargeMappingId, "IX_AccountChargeMappingLog_FkAccountChargeMappingId");

            entity.Property(e => e.PkAccountChargeMappingLogId).ValueGeneratedNever();
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);
        });

        modelBuilder.Entity<AccountChargeReferenceMask>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeReferenceMask)
                .HasName("PK__AccountC__2227A72C1B333780")
                .HasFillFactor(90);

            entity.ToTable("AccountChargeReferenceMask");

            entity.Property(e => e.PkAccountChargeReferenceMask).HasDefaultValueSql("(newid())");
            entity.Property(e => e.ApplyCharge).HasDefaultValue(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityAccountChargeId).HasColumnName("FkEntityAccountChargeID");
            entity.Property(e => e.Mask).HasMaxLength(50);
            entity.Property(e => e.SendNotification).HasDefaultValue(false);

            entity.HasOne(d => d.FkEntityAccountCharge).WithMany(p => p.AccountChargeReferenceMasks)
                .HasForeignKey(d => d.FkEntityAccountChargeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AccountCh__FkEnt__6FE04955");
        });

        modelBuilder.Entity<AccountChargeReversal>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeReversalId)
                .HasName("PK__Ac__Reversals")
                .HasFillFactor(90);

            entity.Property(e => e.PkAccountChargeReversalId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkAccountChargeId).HasColumnName("FkAccountChargeID");
            entity.Property(e => e.Mask).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("money");

            entity.HasOne(d => d.FkAccountCharge).WithMany(p => p.AccountChargeReversals)
                .HasForeignKey(d => d.FkAccountChargeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("AccountReversalsToCharge");
        });

        modelBuilder.Entity<AccountChargeReversalLog>(entity =>
        {
            entity.HasKey(e => e.PkAccountChargeReversalLogId);

            entity.ToTable("AccountChargeReversalLog");

            entity.Property(e => e.PkAccountChargeReversalLogId).ValueGeneratedNever();
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);

            entity.HasOne(d => d.FkAccountCharge).WithMany(p => p.AccountChargeReversalLogs)
                .HasForeignKey(d => d.FkAccountChargeId)
                .HasConstraintName("FK_AccountChargeReversalLogAccountCharge_FkAccountChargeId");

            entity.HasOne(d => d.Fkuser).WithMany(p => p.AccountChargeReversalLogs).HasForeignKey(d => d.FkuserId);
        });

        modelBuilder.Entity<AccountPending>(entity =>
        {
            entity.HasKey(e => e.PkAccountPendingId).HasFillFactor(90);

            entity.ToTable("AccountPending");

            entity.Property(e => e.PkAccountPendingId)
                .ValueGeneratedNever()
                .HasColumnName("PkAccountPendingID");
            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.BankUserCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode).HasMaxLength(20);
            entity.Property(e => e.BranchName).HasMaxLength(50);
            entity.Property(e => e.CountryCurrencyId).HasColumnName("CountryCurrencyID");
            entity.Property(e => e.FkAccountStatusId).HasColumnName("FkAccountStatusID");
            entity.Property(e => e.FkAccountTypeId).HasColumnName("FkAccountTypeID");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.FkControllingEntityId).HasColumnName("FkControllingEntityID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.MapAccount).HasDefaultValue(false);
            entity.Property(e => e.Primarary).HasDefaultValue(true);
            entity.Property(e => e.ResourceType).HasMaxLength(20);
            entity.Property(e => e.ToDelete).HasDefaultValue(false);

            entity.HasOne(d => d.FkAccountType).WithMany(p => p.AccountPendings)
                .HasForeignKey(d => d.FkAccountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountPending_AccountType1");

            entity.HasOne(d => d.FkBank).WithMany(p => p.AccountPendings)
                .HasForeignKey(d => d.FkBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountPending_Bank");
        });

        modelBuilder.Entity<AccountStatus>(entity =>
        {
            entity.HasKey(e => e.PkAccountStatusId).HasFillFactor(90);

            entity.ToTable("AccountStatus");

            entity.Property(e => e.PkAccountStatusId)
                .ValueGeneratedNever()
                .HasColumnName("PkAccountStatusID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AccountType>(entity =>
        {
            entity.HasKey(e => e.PkAccountTypeId).HasFillFactor(90);

            entity.ToTable("AccountType");

            entity.Property(e => e.PkAccountTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkAccountTypeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Fnbh2href).HasColumnName("FNBH2HRef");
            entity.Property(e => e.H2href).HasColumnName("H2HRef");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AddressType>(entity =>
        {
            entity.HasKey(e => e.PkAddressTypeId).HasFillFactor(90);

            entity.ToTable("AddressType");

            entity.Property(e => e.PkAddressTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkAddressTypeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.PkAgentId).HasFillFactor(90);

            entity.ToTable("Agent");

            entity.Property(e => e.PkAgentId)
                .ValueGeneratedNever()
                .HasColumnName("PkAgentID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AgentSupplier>(entity =>
        {
            entity.HasKey(e => e.PkAgentSupplierId).HasFillFactor(90);

            entity.ToTable("AgentSupplier");

            entity.Property(e => e.PkAgentSupplierId)
                .ValueGeneratedNever()
                .HasColumnName("PkAgentSupplierID");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");

            entity.HasOne(d => d.FkAgent).WithMany(p => p.AgentSupplierFkAgents)
                .HasForeignKey(d => d.FkAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AgentSupplier_Agent");

            entity.HasOne(d => d.FkSupplier).WithMany(p => p.AgentSupplierFkSuppliers)
                .HasForeignKey(d => d.FkSupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AgentSupplier_Supplier");
        });

        modelBuilder.Entity<AllCountriesBank>(entity =>
        {
            entity.HasNoKey();

            entity.HasIndex(e => e.Bic, "IX_AllCountriesBanks_BIC");

            entity.HasIndex(e => e.CountryIsocode2, "IX_AllCountriesBanks_Country");

            entity.HasIndex(e => e.BankName, "IX_AllCountriesBansk_BankName");

            entity.Property(e => e.BankName).HasMaxLength(201);
            entity.Property(e => e.Bic)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("BIC");
            entity.Property(e => e.City).HasMaxLength(35);
            entity.Property(e => e.CountryIsocode2)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CountryISOCode2");
        });

        modelBuilder.Entity<ApprovalStatusType>(entity =>
        {
            entity.HasKey(e => e.PkApprovalStatusTypeId).HasFillFactor(90);

            entity.ToTable("ApprovalStatusType");

            entity.Property(e => e.PkApprovalStatusTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkApprovalStatusTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ApprovalType>(entity =>
        {
            entity.HasKey(e => e.PkApprocalTypeId).HasFillFactor(90);

            entity.ToTable("ApprovalType");

            entity.Property(e => e.PkApprocalTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkApprocalTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<BalanceTable>(entity =>
        {
            entity.HasKey(e => new { e.AccountId, e.BalanceDate }).HasName("PK__BalanceT__1FC66A262F1E0A22");

            entity.ToTable("BalanceTable");

            entity.Property(e => e.AccountId).HasColumnName("Account_ID");
            entity.Property(e => e.BalanceDate).HasColumnName("Balance_Date");
            entity.Property(e => e.ClosingBalance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Closing_Balance");
            entity.Property(e => e.CumulativeCredits)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Cumulative_Credits");
            entity.Property(e => e.CumulativeDebits)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Cumulative_Debits");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("Last_Updated");
            entity.Property(e => e.OpeningBalance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Opening_Balance");
            entity.Property(e => e.Source)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Batch");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.PkBankId).HasFillFactor(90);

            entity.ToTable("Bank");

            entity.Property(e => e.PkBankId)
                .ValueGeneratedNever()
                .HasColumnName("PkBankID");
            entity.Property(e => e.Active).HasDefaultValue(true, "DF__Bank__Active__5D6271BE");
            entity.Property(e => e.AddressLine1)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Currency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PostalCode)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UniversalCode).HasMaxLength(20);
        });

        modelBuilder.Entity<BankTime>(entity =>
        {
            entity.HasKey(e => e.PkBankTimeId).HasName("PK__BankTime__CFCD73EBEF95CC7A");

            entity.ToTable("BankTime");

            entity.Property(e => e.PkBankTimeId)
                .ValueGeneratedNever()
                .HasColumnName("PkBankTimeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Amount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(16, 4)");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.IsInternal).HasDefaultValue(false);

            entity.HasOne(d => d.FkBank).WithMany(p => p.BankTimes)
                .HasForeignKey(d => d.FkBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BankTime__FkBank__344A823B");
        });

        modelBuilder.Entity<BaseProduct>(entity =>
        {
            entity.HasKey(e => e.PkBaseProductId).HasFillFactor(90);

            entity.ToTable("BaseProduct");

            entity.Property(e => e.PkBaseProductId)
                .ValueGeneratedNever()
                .HasColumnName("PkBaseProductID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FkGridId).HasColumnName("FkGridID");

            entity.HasOne(d => d.FkGrid).WithMany(p => p.BaseProducts)
                .HasForeignKey(d => d.FkGridId)
                .HasConstraintName("FK_BaseProduct_Grid1");
        });

        modelBuilder.Entity<BaseProductPrice>(entity =>
        {
            entity.HasKey(e => e.PkBaseProductPriceId).HasFillFactor(90);

            entity.ToTable("BaseProductPrice");

            entity.Property(e => e.PkBaseProductPriceId)
                .ValueGeneratedNever()
                .HasColumnName("PkBaseProductPriceID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.FkBaseProductId).HasColumnName("FkBaseProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.FkBaseProduct).WithMany(p => p.BaseProductPrices)
                .HasForeignKey(d => d.FkBaseProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BaseProductPrice_BaseProduct");
        });

        modelBuilder.Entity<BaseProductZone>(entity =>
        {
            entity.HasKey(e => e.PkBaseProductZoneId).HasFillFactor(90);

            entity.ToTable("BaseProductZone");

            entity.Property(e => e.PkBaseProductZoneId)
                .ValueGeneratedNever()
                .HasColumnName("PkBaseProductZoneID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Differential).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.FkBaseProductPriceId).HasColumnName("FkBaseProductPriceID");
            entity.Property(e => e.FkGridZoneId).HasColumnName("FkGridZoneID");

            entity.HasOne(d => d.FkBaseProductPrice).WithMany(p => p.BaseProductZones)
                .HasForeignKey(d => d.FkBaseProductPriceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BaseProductZone_BaseProductPrice1");

            entity.HasOne(d => d.FkGridZone).WithMany(p => p.BaseProductZones)
                .HasForeignKey(d => d.FkGridZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BaseProductZone_GridZone");
        });

        modelBuilder.Entity<BatchBeneficiaryFile>(entity =>
        {
            entity.HasKey(e => e.PkBatchFileId);

            entity.ToTable("BatchBeneficiaryFile");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FileName).IsUnicode(false);
            entity.Property(e => e.FilePath).IsUnicode(false);
            entity.Property(e => e.OriginalFileName).IsUnicode(false);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.BatchBeneficiaryFiles)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BatchBeneficiaryFile_Entity");

            entity.HasOne(d => d.FkUploadUser).WithMany(p => p.BatchBeneficiaryFiles)
                .HasForeignKey(d => d.FkUploadUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BatchBeneficiaryFile_User");
        });

        modelBuilder.Entity<BeneficiaryFile>(entity =>
        {
            entity.HasKey(e => e.PkBeneficiaryFileId);

            entity.ToTable("BeneficiaryFile");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FileName).IsUnicode(false);
            entity.Property(e => e.FilePath).IsUnicode(false);
            entity.Property(e => e.OriginalFileName).IsUnicode(false);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.BeneficiaryFiles)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BeneficiaryFile_Entity");

            entity.HasOne(d => d.FkUploadUser).WithMany(p => p.BeneficiaryFiles)
                .HasForeignKey(d => d.FkUploadUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BeneficiaryFile_User");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.PkCategoryId)
                .HasName("PK_Category_1")
                .HasFillFactor(90);

            entity.ToTable("Category");

            entity.Property(e => e.PkCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("PkCategoryID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ChargeLog>(entity =>
        {
            entity.HasKey(e => e.PkchargeLog)
                .HasName("PK__ChargeLo__CF6ED4EFA6A94ED3")
                .HasFillFactor(90);

            entity.ToTable("ChargeLog");

            entity.Property(e => e.PkchargeLog)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PKChargeLog");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkaccountCharge).HasColumnName("FKAccountCharge");
            entity.Property(e => e.FkentityAccountCharge).HasColumnName("FKEntityAccountCharge");
            entity.Property(e => e.FkentityBankStatement).HasColumnName("FKEntityBankStatement");

            entity.HasOne(d => d.FkentityBankStatementNavigation).WithMany(p => p.ChargeLogs)
                .HasForeignKey(d => d.FkentityBankStatement)
                .HasConstraintName("FK__ChargeLog__FKEnt__6E8210C8");
        });

        modelBuilder.Entity<ChargeNotificationLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ChargeNotificationLog");

            entity.Property(e => e.ChargeAmount).HasColumnType("money");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Destination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MailSent)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkNotificationLogId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<ChargeUploadFile>(entity =>
        {
            entity.HasKey(e => e.PkFileId);

            entity.ToTable("ChargeUploadFile");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FileName).IsUnicode(false);
            entity.Property(e => e.FilePath).IsUnicode(false);
            entity.Property(e => e.OriginalFileName).IsUnicode(false);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.ChargeUploadFiles)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChargeUploadFile_Entity");

            entity.HasOne(d => d.FkUploadUser).WithMany(p => p.ChargeUploadFiles)
                .HasForeignKey(d => d.FkUploadUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChargeUploadFile_User");
        });

        modelBuilder.Entity<ClientApproval>(entity =>
        {
            entity.HasKey(e => e.PkClientApprovalId).HasFillFactor(90);

            entity.ToTable("ClientApproval");

            entity.Property(e => e.PkClientApprovalId)
                .ValueGeneratedNever()
                .HasColumnName("PkClientApprovalID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOrderUserId).HasColumnName("FkOrderUserID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.IOrder).HasColumnName("iOrder");

            entity.HasOne(d => d.FkClient).WithMany(p => p.ClientApprovals)
                .HasForeignKey(d => d.FkClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientApproval_Entity");

            entity.HasOne(d => d.FkOrderStatus).WithMany(p => p.ClientApprovals)
                .HasForeignKey(d => d.FkOrderStatusId)
                .HasConstraintName("FK_ClientApproval_OrderStatus");

            entity.HasOne(d => d.FkOrderUser).WithMany(p => p.ClientApprovalFkOrderUsers)
                .HasForeignKey(d => d.FkOrderUserId)
                .HasConstraintName("FK_ClientApproval_UserOrder");

            entity.HasOne(d => d.FkUser).WithMany(p => p.ClientApprovalFkUsers)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientApproval_User");
        });

        modelBuilder.Entity<ClientProductApproval>(entity =>
        {
            entity.HasKey(e => e.PkClientProductApprovalId).HasFillFactor(90);

            entity.ToTable("ClientProductApproval");

            entity.Property(e => e.PkClientProductApprovalId)
                .ValueGeneratedNever()
                .HasColumnName("PkClientProductApprovalID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkProductId).HasColumnName("FkProductID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.IOrder).HasColumnName("iOrder");

            entity.HasOne(d => d.FkClient).WithMany(p => p.ClientProductApprovals)
                .HasForeignKey(d => d.FkClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientProductApproval_Entity");

            entity.HasOne(d => d.FkOrderStatus).WithMany(p => p.ClientProductApprovals)
                .HasForeignKey(d => d.FkOrderStatusId)
                .HasConstraintName("FK_ClientProductApproval_OrderStatus");

            entity.HasOne(d => d.FkProduct).WithMany(p => p.ClientProductApprovals)
                .HasForeignKey(d => d.FkProductId)
                .HasConstraintName("FK_ClientProductApproval_Product");

            entity.HasOne(d => d.FkUser).WithMany(p => p.ClientProductApprovals)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClientProductApproval_User");
        });

        modelBuilder.Entity<ClusterNotification>(entity =>
        {
            entity.HasKey(e => e.PkClusterId).HasFillFactor(90);

            entity.ToTable("ClusterNotification");

            entity.Property(e => e.PkClusterId).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Destination)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CommissionType>(entity =>
        {
            entity.HasKey(e => e.PkCommissionTypeId).HasFillFactor(90);

            entity.ToTable("CommissionType");

            entity.Property(e => e.PkCommissionTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkCommissionTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CommunicationType>(entity =>
        {
            entity.HasKey(e => e.PkCommTypeId).HasFillFactor(90);

            entity.ToTable("CommunicationType");

            entity.Property(e => e.PkCommTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkCommTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ConfigAccountType>(entity =>
        {
            entity.HasKey(e => e.PkConfigAccountTypeId).HasName("PK__ConfigAc__22E312BD930B5FDD");

            entity.ToTable("ConfigAccountType");

            entity.Property(e => e.PkConfigAccountTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkConfigAccountTypeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__10D160BFA44818A5");

            entity.ToTable("Country");

            entity.HasIndex(e => e.CcFips, "idx_cc_fips");

            entity.HasIndex(e => e.CcIso, "idx_cc_iso");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CcFips)
                .HasMaxLength(2)
                .HasColumnName("cc_fips");
            entity.Property(e => e.CcIso)
                .HasMaxLength(2)
                .HasColumnName("cc_iso");
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.Tld)
                .HasMaxLength(3)
                .HasColumnName("tld");
        });

        modelBuilder.Entity<CountryBank>(entity =>
        {
            entity.HasKey(e => e.CountryBankId).HasName("PK__CountryB__B2BF0C420EBD3890");

            entity.ToTable("CountryBank");

            entity.HasOne(d => d.Bank).WithMany(p => p.CountryBanks)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK__CountryBa__BankI__5FD3FEBE");

            entity.HasOne(d => d.Country).WithMany(p => p.CountryBanks)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__CountryBa__Count__60C822F7");
        });

        modelBuilder.Entity<CountryCurrency>(entity =>
        {
            entity.ToTable("CountryCurrency");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("Currency");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("code");
            entity.Property(e => e.Country)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)")
                .IsFixedLength()
                .HasColumnName("country");
            entity.Property(e => e.Number)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("number");
        });

        modelBuilder.Entity<CurrencySymbol>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(50)
                .HasColumnName("Currency_Code");
            entity.Property(e => e.MacShortcut)
                .HasMaxLength(50)
                .HasColumnName("Mac_Shortcut");
            entity.Property(e => e.Symbol).HasMaxLength(50);
            entity.Property(e => e.WindowsShortcut)
                .HasMaxLength(50)
                .HasColumnName("Windows_Shortcut");
        });

        modelBuilder.Entity<CvLEb>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CvL_EBS");

            entity.HasIndex(e => e.ExtractedOrderNum, "IX_EBS_ExtractedOrderNum");

            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DateMapped).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExtractedOrderNum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasComputedColumnSql("(case when [Description] like '%GPAY%' then substring([Description],charindex('GPAY',[Description])+(4),(20))  end)", true);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkBankInfoId).HasColumnName("FkBankInfoID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DashboardChartVisibilityLink>(entity =>
        {
            entity.HasKey(e => e.PkDashboardChartVisibilityLinkId).HasName("PK__Dashboar__9072F2AA9CAAD12A");

            entity.ToTable("DashboardChartVisibilityLink");

            entity.Property(e => e.PkDashboardChartVisibilityLinkId).HasColumnName("PkDashboardChartVisibilityLinkID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.DashboardTypeId).HasColumnName("DashboardTypeID");
            entity.Property(e => e.EntityId).HasColumnName("EntityID");
            entity.Property(e => e.Visible).HasDefaultValue(true);
        });

        modelBuilder.Entity<DashboardColumnVisibility>(entity =>
        {
            entity.HasKey(e => e.PkDashboardColumnVisibilityId).HasName("PK__DashBoar__5B2F30BF61E3E88C");

            entity.ToTable("DashboardColumnVisibility");

            entity.Property(e => e.PkDashboardColumnVisibilityId)
                .ValueGeneratedNever()
                .HasColumnName("PkDashboardColumnVisibilityID");
            entity.Property(e => e.AccountNumber).HasDefaultValue(true);
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Amount).HasDefaultValue(true);
            entity.Property(e => e.Bank).HasDefaultValue(true);
            entity.Property(e => e.Description).HasDefaultValue(true);
            entity.Property(e => e.Entity).HasDefaultValue(true);
            entity.Property(e => e.EventNumber).HasDefaultValue(true);
            entity.Property(e => e.ReferenceNumber).HasDefaultValue(true);
            entity.Property(e => e.SyncDate).HasDefaultValue(true);
            entity.Property(e => e.TransactionCode).HasDefaultValue(true);
            entity.Property(e => e.TransactionDate).HasDefaultValue(true);
            entity.Property(e => e.Type).HasDefaultValue(true);
        });

        modelBuilder.Entity<DashboardConfig>(entity =>
        {
            entity.HasKey(e => e.PkdashboardConfigId).HasName("PK__Dashboar__1378D7A9A8E35633");

            entity.ToTable("DashboardConfig");

            entity.Property(e => e.PkdashboardConfigId)
                .ValueGeneratedNever()
                .HasColumnName("PKDashboardConfigID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreditTransaction).HasDefaultValue(true);
            entity.Property(e => e.DebitTransaction).HasDefaultValue(true);
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkDashboardColumnVisibilityId).HasColumnName("FkDashboardColumnVisibilityID");
            entity.Property(e => e.FkDashboardTypeId).HasColumnName("FkDashboardTypeID");
            entity.Property(e => e.FkFrequencyId).HasColumnName("FkFrequencyID");
            entity.Property(e => e.FkUnitOfMeasureId).HasColumnName("FkUnitOfMeasureID");
            entity.Property(e => e.FkentityId).HasColumnName("FKEntityID");
            entity.Property(e => e.Heading)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Interval)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MaxVal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MinVal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Reference).IsUnicode(false);
            entity.Property(e => e.ShowSummaryData).HasDefaultValue(true);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.XAxis)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("X-Axis");
            entity.Property(e => e.YAxis)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Y-Axis");

            entity.HasOne(d => d.FkAccount).WithMany(p => p.DashboardConfigs)
                .HasForeignKey(d => d.FkAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DashboardConfig_Account");

            entity.HasOne(d => d.FkDashboardColumnVisibility).WithMany(p => p.DashboardConfigs)
                .HasForeignKey(d => d.FkDashboardColumnVisibilityId)
                .HasConstraintName("FK_DashboardConfig_DashboardColumnVisibility");

            entity.HasOne(d => d.FkDashboardType).WithMany(p => p.DashboardConfigs)
                .HasForeignKey(d => d.FkDashboardTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dashboard__FkDas__0758AB8A");

            entity.HasOne(d => d.FkFrequency).WithMany(p => p.DashboardConfigs)
                .HasForeignKey(d => d.FkFrequencyId)
                .HasConstraintName("FK__Dashboard__FkFre__0C1D60A7");

            entity.HasOne(d => d.FkUnitOfMeasure).WithMany(p => p.DashboardConfigs)
                .HasForeignKey(d => d.FkUnitOfMeasureId)
                .HasConstraintName("FK__Dashboard__FkUni__0B293C6E");

            entity.HasOne(d => d.Fkentity).WithMany(p => p.DashboardConfigs)
                .HasForeignKey(d => d.FkentityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dashboard__FKEnt__084CCFC3");
        });

        modelBuilder.Entity<DashboardFrequency>(entity =>
        {
            entity.HasKey(e => e.PkdashboardFrequencyId);

            entity.ToTable("DashboardFrequency");

            entity.Property(e => e.PkdashboardFrequencyId).HasColumnName("PKDashboardFrequencyId");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DashboardLedgerLink>(entity =>
        {
            entity.HasKey(e => e.PkDashboardLedgerLinkId).HasName("PK__Dashboar__395F8FFE11EBFDB9");

            entity.ToTable("DashboardLedgerLink");

            entity.Property(e => e.PkDashboardLedgerLinkId)
                .ValueGeneratedNever()
                .HasColumnName("PkDashboardLedgerLinkID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkDashboardConfigId).HasColumnName("FkDashboardConfigID");
            entity.Property(e => e.FkledgerEntityId).HasColumnName("FKLedgerEntityId");

            entity.HasOne(d => d.FkDashboardConfig).WithMany(p => p.DashboardLedgerLinks)
                .HasForeignKey(d => d.FkDashboardConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dashboard__FkDas__0EF9CD52");

            entity.HasOne(d => d.FkledgerEntity).WithMany(p => p.DashboardLedgerLinks)
                .HasForeignKey(d => d.FkledgerEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dashboard__FKLedgerEntityId__0EF9CD52");
        });

        modelBuilder.Entity<DashboardTransactionCodeLink>(entity =>
        {
            entity.HasKey(e => e.PkDashboardTransactionCodeLinkId).HasName("PK__Dashboar__EFAB67E84F7F2DA1");

            entity.ToTable("DashboardTransactionCodeLink");

            entity.Property(e => e.PkDashboardTransactionCodeLinkId)
                .ValueGeneratedNever()
                .HasColumnName("PkDashboardTransactionCodeLinkID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkDashboardConfigId).HasColumnName("FkDashboardConfigID");
            entity.Property(e => e.FktransactionCodeId).HasColumnName("FKTransactionCodeId");

            entity.HasOne(d => d.FkDashboardConfig).WithMany(p => p.DashboardTransactionCodeLinks)
                .HasForeignKey(d => d.FkDashboardConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dashboard__FkDas__12CA5E36");

            entity.HasOne(d => d.FktransactionCode).WithMany(p => p.DashboardTransactionCodeLinks)
                .HasForeignKey(d => d.FktransactionCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dashboard__FKTra__13BE826F");
        });

        modelBuilder.Entity<DashboardTransactionType>(entity =>
        {
            entity.HasKey(e => e.PkdashboardTransactionTypeId);

            entity.ToTable("DashboardTransactionType");

            entity.Property(e => e.PkdashboardTransactionTypeId).HasColumnName("PKDashboardTransactionTypeId");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DashboardType>(entity =>
        {
            entity.HasKey(e => e.PkdashboardTypeId);

            entity.ToTable("DashboardType");

            entity.Property(e => e.PkdashboardTypeId).HasColumnName("PKDashboardTypeId");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DashboardUnitOfMeasure>(entity =>
        {
            entity.HasKey(e => e.PkdashboardUnitOfMeasureId);

            entity.ToTable("DashboardUnitOfMeasure");

            entity.Property(e => e.PkdashboardUnitOfMeasureId).HasColumnName("PKDashboardUnitOfMeasureId");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DataSheet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("DataSheet");

            entity.Property(e => e.Code).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Image).HasMaxLength(255);
            entity.Property(e => e.NoInPackage)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("NO IN PACKAGE");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ProductCategory)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("Product Category");
            entity.Property(e => e.ShortName)
                .HasMaxLength(255)
                .HasColumnName("Short Name");
            entity.Property(e => e.Size).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Specification).HasMaxLength(255);
            entity.Property(e => e.Unit)
                .HasMaxLength(255)
                .HasColumnName("UNIT");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Devices__3213E83F43DE61E3");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DeviceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeviceStatus).HasDefaultValue(false);
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.StatusDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DifferentialType>(entity =>
        {
            entity.HasKey(e => e.PkDifferentialTypeId).HasFillFactor(90);

            entity.ToTable("DifferentialType");

            entity.Property(e => e.PkDifferentialTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkDifferentialTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DiscountType>(entity =>
        {
            entity.HasKey(e => e.PkDiscountTypeId).HasFillFactor(90);

            entity.ToTable("DiscountType");

            entity.Property(e => e.PkDiscountTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkDiscountTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drivers__3213E83FD83503A6");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IdNumber)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Entity>(entity =>
        {
            entity.HasKey(e => e.PkEntityId)
                .HasName("PK_Supplier")
                .HasFillFactor(90);

            entity.ToTable("Entity", tb => tb.HasTrigger("trg_Entity_SetHash"));

            entity.HasIndex(e => e.Id, "AK_Entity_ID").IsUnique();

            entity.HasIndex(e => new { e.Active, e.Taname }, "IX_Product_Search_Entity_VendorSearch").HasFilter("([Active]=(1))");

            entity.HasIndex(e => new { e.Active, e.FkStatusId }, "NCI_Active_FKStatusID_PKEntityID_Name");

            entity.Property(e => e.PkEntityId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())", "DF_Entity_DateCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntityReferenceAlias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Featured).HasDefaultValue(false);
            entity.Property(e => e.FkEntitySubTypeId).HasColumnName("FkEntitySubTypeID");
            entity.Property(e => e.FkIdentificationTypeId).HasColumnName("FkIdentificationTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.Hash)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsCmconsumer)
                .HasDefaultValue(false)
                .HasColumnName("IsCMConsumer");
            entity.Property(e => e.IsNew).HasDefaultValue(false, "DF_Entity_IsNew");
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())", "DF_Entity_LastUpdated")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProfileExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RejectReason)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatexemption)
                .HasDefaultValue(false)
                .HasColumnName("VATExemption");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
            entity.Property(e => e.Vatregistered)
                .HasDefaultValue(false)
                .HasColumnName("VATRegistered");
            entity.Property(e => e.WbulkLimitSgn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WBulkLimitSgn");
            entity.Property(e => e.WbulkPaymentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WBulkPaymentType");
        });

        modelBuilder.Entity<EntityAccount>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountId).HasFillFactor(90);

            entity.ToTable("EntityAccount");

            entity.Property(e => e.PkEntityAccountId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityAccountID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.DefaultToThisEntity).HasDefaultValue(false);
            entity.Property(e => e.Facility).HasColumnType("money");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValue("01", "Processing");
            entity.Property(e => e.TransactionCost).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.FkAccount).WithMany(p => p.EntityAccounts)
                .HasForeignKey(d => d.FkAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityAccount_EntityAccount");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityAccounts)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityAccount_Entity");
        });

        modelBuilder.Entity<EntityAccountCharge>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeId)
                .HasName("PK__EntityAc__D39FB42D1805E4C9")
                .HasFillFactor(90);

            entity.ToTable("EntityAccountCharge");

            entity.Property(e => e.PkEntityAccountChargeId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PkEntityAccountChargeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ApplyOnDebit).HasDefaultValue(false);
            entity.Property(e => e.ChargeAccumulatedAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ChargeExpiryAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChargeExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.ChargeStartDate).HasColumnType("datetime");
            entity.Property(e => e.ChargeType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FilterOnRange).HasDefaultValue(false);
            entity.Property(e => e.FkEntityAccountId).HasColumnName("FkEntityAccountID");
            entity.Property(e => e.MainEntityAccountId).HasColumnName("MainEntityAccountID");
            entity.Property(e => e.MaximumAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MinimumAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Reference).HasMaxLength(50);
            entity.Property(e => e.TransactionCodeId)
                .HasDefaultValue(1)
                .HasColumnName("TransactionCodeID");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.FkEntityAccount).WithMany(p => p.EntityAccountCharges)
                .HasForeignKey(d => d.FkEntityAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityAcc__FkEnt__257C74A0");

            entity.HasOne(d => d.TransactionCode).WithMany(p => p.EntityAccountCharges)
                .HasForeignKey(d => d.TransactionCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityAcc__Trans__0015E5C7");
        });

        modelBuilder.Entity<EntityAccountChargeAccumulatedAmountLog>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeAccumulatedAmountLogId).HasName("PK_ChargeAccumulatedAmountLog");

            entity.ToTable("EntityAccountChargeAccumulatedAmountLog");

            entity.Property(e => e.PkEntityAccountChargeAccumulatedAmountLogId).ValueGeneratedNever();
            entity.Property(e => e.ChargeAdjustmentAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ChargeDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkEntityAccountCharge).WithMany(p => p.EntityAccountChargeAccumulatedAmountLogs).HasForeignKey(d => d.FkEntityAccountChargeId);
        });

        modelBuilder.Entity<EntityAccountChargeExclusion>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeExclusionId)
                .HasName("PK__EntityAc__Exclusion")
                .HasFillFactor(90);

            entity.Property(e => e.PkEntityAccountChargeExclusionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkEntityAccountChargeId).HasColumnName("FkEntityAccountChargeID");
            entity.Property(e => e.Mask).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("money");

            entity.HasOne(d => d.FkEntityAccountCharge).WithMany(p => p.EntityAccountChargeExclusions)
                .HasForeignKey(d => d.FkEntityAccountChargeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ExclusionToCharge");
        });

        modelBuilder.Entity<EntityAccountChargeExlusionLog>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeExclusionLogId);

            entity.ToTable("EntityAccountChargeExlusionLog");

            entity.HasIndex(e => e.FkuserId, "IX_EntityAccountChargeExlusionLog_FKUserId");

            entity.HasIndex(e => e.FkEntityAccountChargeId, "IX_EntityAccountChargeExlusionLog_FkEntityAccountChargeId");

            entity.Property(e => e.PkEntityAccountChargeExclusionLogId).ValueGeneratedNever();
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);

            entity.HasOne(d => d.FkEntityAccountCharge).WithMany(p => p.EntityAccountChargeExlusionLogs).HasForeignKey(d => d.FkEntityAccountChargeId);

            entity.HasOne(d => d.Fkuser).WithMany(p => p.EntityAccountChargeExlusionLogs).HasForeignKey(d => d.FkuserId);
        });

        modelBuilder.Entity<EntityAccountChargeLog>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeLogId);

            entity.ToTable("EntityAccountChargeLog");

            entity.HasIndex(e => e.FkuserId, "IX_EntityAccountChargeLog_FKUserId");

            entity.HasIndex(e => e.FkEntityAccountChargeId, "IX_EntityAccountChargeLog_FkEntityAccountChargeId");

            entity.Property(e => e.PkEntityAccountChargeLogId).ValueGeneratedNever();
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);

            entity.HasOne(d => d.FkEntityAccountCharge).WithMany(p => p.EntityAccountChargeLogs).HasForeignKey(d => d.FkEntityAccountChargeId);

            entity.HasOne(d => d.Fkuser).WithMany(p => p.EntityAccountChargeLogs).HasForeignKey(d => d.FkuserId);
        });

        modelBuilder.Entity<EntityAccountChargeMapping>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeMapping)
                .HasName("PK__EntityAc__5C7B016F4C042A7C")
                .HasFillFactor(90);

            entity.ToTable("EntityAccountChargeMapping");

            entity.Property(e => e.PkEntityAccountChargeMapping).HasDefaultValueSql("(newid())", "DF__EntityAcc__PkEnt__2CE95795");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ApplyCharge).HasDefaultValue(false);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())", "DF__EntityAcc__Creat__2DDD7BCE")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityAccountChargeId).HasColumnName("FkEntityAccountChargeID");
            entity.Property(e => e.Mask).HasMaxLength(50);
            entity.Property(e => e.SendNotification).HasDefaultValue(false);
            entity.Property(e => e.TransactionCodeId).HasColumnName("TransactionCodeID");

            entity.HasOne(d => d.FkEntityAccountCharge).WithMany(p => p.EntityAccountChargeMappings)
                .HasForeignKey(d => d.FkEntityAccountChargeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityAcc__FkEnt__2ED1A007");
        });

        modelBuilder.Entity<EntityAccountChargeMappingLog>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeMappingLogId);

            entity.ToTable("EntityAccountChargeMappingLog");

            entity.HasIndex(e => e.FkuserId, "IX_EntityAccountChargeMappingLog_FKUserId");

            entity.HasIndex(e => e.FkEntityAccountChargeId, "IX_EntityAccountChargeMappingLog_FkEntityAccountChargeId");

            entity.HasIndex(e => e.FkEntityAccountChargeMappingId, "IX_EntityAccountChargeMappingLog_FkEntityAccountChargeMappingId");

            entity.Property(e => e.PkEntityAccountChargeMappingLogId).ValueGeneratedNever();
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);
        });

        modelBuilder.Entity<EntityAccountChargeReversal>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeReversalId)
                .HasName("PK__EntityAc__Reversals")
                .HasFillFactor(90);

            entity.Property(e => e.PkEntityAccountChargeReversalId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkEntityAccountChargeId).HasColumnName("FkEntityAccountChargeID");
            entity.Property(e => e.Mask).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("money");

            entity.HasOne(d => d.FkEntityAccountCharge).WithMany(p => p.EntityAccountChargeReversals)
                .HasForeignKey(d => d.FkEntityAccountChargeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ReversalsToCharge");
        });

        modelBuilder.Entity<EntityAccountChargeReversalLog>(entity =>
        {
            entity.HasKey(e => e.PkEntityAccountChargeReversalLogId);

            entity.ToTable("EntityAccountChargeReversalLog");

            entity.Property(e => e.PkEntityAccountChargeReversalLogId).ValueGeneratedNever();
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);

            entity.HasOne(d => d.FkEntityAccountCharge).WithMany(p => p.EntityAccountChargeReversalLogs).HasForeignKey(d => d.FkEntityAccountChargeId);

            entity.HasOne(d => d.Fkuser).WithMany(p => p.EntityAccountChargeReversalLogs).HasForeignKey(d => d.FkuserId);
        });

        modelBuilder.Entity<EntityAccountLedgerTest>(entity =>
        {
            entity.HasKey(e => e.LedgerId).HasName("PK__EntityAc__AE70E0AF73C54D7E");

            entity.ToTable("EntityAccountLedger_Test");

            entity.Property(e => e.LedgerId).HasColumnName("LedgerID");
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.CreatedOn).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.SourceId).HasColumnName("SourceID");
            entity.Property(e => e.SourceType)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EntityAddress>(entity =>
        {
            entity.HasKey(e => e.PkEntityAddressId)
                .HasName("PK_SupplierAddress")
                .HasFillFactor(90);

            entity.ToTable("EntityAddress");

            entity.Property(e => e.PkEntityAddressId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityAddressID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Address3)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AdrReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cellphone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAddressTypeId).HasColumnName("FkAddressTypeID");
            entity.Property(e => e.FkCountryId).HasColumnName("FkCountryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkMagisterialDistrictId).HasColumnName("FkMagisterialDistrictID");
            entity.Property(e => e.FkProvinceId).HasColumnName("FkProvinceID");
            entity.Property(e => e.FkTownId).HasColumnName("FkTownID");
            entity.Property(e => e.Primaray).HasDefaultValue(true);
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Wcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WCode");
            entity.Property(e => e.WcostStructItem)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WCostStructItem");
            entity.Property(e => e.WcostStructPrefix)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WCostStructPrefix");
            entity.Property(e => e.WsiteName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WSiteName");

            entity.HasOne(d => d.FkAddressType).WithMany(p => p.EntityAddresses)
                .HasForeignKey(d => d.FkAddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierAddress_AddressType");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityAddresses)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierAddress_Supplier");

            entity.HasOne(d => d.FkMagisterialDistrict).WithMany(p => p.EntityAddresses)
                .HasForeignKey(d => d.FkMagisterialDistrictId)
                .HasConstraintName("FK_EntityAddress_MagisterialDistrict");

            entity.HasOne(d => d.FkProvince).WithMany(p => p.EntityAddresses)
                .HasForeignKey(d => d.FkProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierAddress_Province");

            entity.HasOne(d => d.FkTown).WithMany(p => p.EntityAddresses)
                .HasForeignKey(d => d.FkTownId)
                .HasConstraintName("FK_EntityAddress_Town");
        });

        modelBuilder.Entity<EntityAddressPending>(entity =>
        {
            entity.HasKey(e => e.PkEntityAddressPendingId).HasFillFactor(90);

            entity.ToTable("EntityAddressPending");

            entity.Property(e => e.PkEntityAddressPendingId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityAddressPendingID");
            entity.Property(e => e.Address1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Address3)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AdrReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cellphone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAddressTypeId).HasColumnName("FkAddressTypeID");
            entity.Property(e => e.FkCountryId).HasColumnName("FkCountryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkMagisterialDistrictId).HasColumnName("FkMagisterialDistrictID");
            entity.Property(e => e.FkProvinceId).HasColumnName("FkProvinceID");
            entity.Property(e => e.FkTownId).HasColumnName("FkTownID");
            entity.Property(e => e.Primaray).HasDefaultValue(false, "DF_EntityAddressPending_Primaray");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ToDelete).HasDefaultValue(false);
            entity.Property(e => e.Wcode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WCode");
            entity.Property(e => e.WcostStructItem)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WCostStructItem");
            entity.Property(e => e.WcostStructPrefix)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WCostStructPrefix");
            entity.Property(e => e.WsiteName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WSiteName");

            entity.HasOne(d => d.FkAddressType).WithMany(p => p.EntityAddressPendings)
                .HasForeignKey(d => d.FkAddressTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityAddressPending_AddressType");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityAddressPendings)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityAddressPending_Supplier");

            entity.HasOne(d => d.FkProvince).WithMany(p => p.EntityAddressPendings)
                .HasForeignKey(d => d.FkProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityAddressPending_Province");

            entity.HasOne(d => d.FkTown).WithMany(p => p.EntityAddressPendings)
                .HasForeignKey(d => d.FkTownId)
                .HasConstraintName("FK_EntityAddressPending_Town");
        });

        modelBuilder.Entity<EntityBankInfo>(entity =>
        {
            entity.HasKey(e => e.PkBankInfoId)
                .HasName("PK_SupplierBankInfo")
                .HasFillFactor(90);

            entity.ToTable("EntityBankInfo");

            entity.Property(e => e.PkBankInfoId)
                .ValueGeneratedNever()
                .HasColumnName("PkBankInfoID");
            entity.Property(e => e.AccountNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FacilityAccount).HasDefaultValue(false, "DF_EntityBankInfo_FacilityAccount_1");
            entity.Property(e => e.FkAccountTypeId).HasColumnName("FkAccountTypeID");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.Primarary).HasDefaultValue(true);
            entity.Property(e => e.ProcurementAccount).HasDefaultValue(false);

            entity.HasOne(d => d.FkAccountType).WithMany(p => p.EntityBankInfos)
                .HasForeignKey(d => d.FkAccountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierBankInfo_AccountType");

            entity.HasOne(d => d.FkBank).WithMany(p => p.EntityBankInfos)
                .HasForeignKey(d => d.FkBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierBankInfo_Bank");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityBankInfos)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierBankInfo_Supplier");
        });

        modelBuilder.Entity<EntityBankInfoPending>(entity =>
        {
            entity.HasKey(e => e.PkBankInfoId).HasFillFactor(90);

            entity.ToTable("EntityBankInfoPending");

            entity.Property(e => e.PkBankInfoId)
                .ValueGeneratedNever()
                .HasColumnName("PkBankInfoID");
            entity.Property(e => e.AccountNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountTypeId).HasColumnName("FkAccountTypeID");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.Primarary).HasDefaultValue(true);
            entity.Property(e => e.ProcurementAccount).HasDefaultValue(false);
            entity.Property(e => e.ToDelete).HasDefaultValue(false, "DF_EntityBankInfoPending_ToDelete");

            entity.HasOne(d => d.FkAccountType).WithMany(p => p.EntityBankInfoPendings)
                .HasForeignKey(d => d.FkAccountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityBankInfoPending_AccountType");

            entity.HasOne(d => d.FkBank).WithMany(p => p.EntityBankInfoPendings)
                .HasForeignKey(d => d.FkBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityBankInfoPending_Bank");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityBankInfoPendings)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityBankInfoPending_Supplier");
        });

        modelBuilder.Entity<EntityBankStatement>(entity =>
        {
            entity.HasKey(e => e.PkEntityBankStatementId).HasFillFactor(90);

            entity.ToTable("EntityBankStatement", tb => tb.HasTrigger("tr_dbo_EntityBankStatement_9787cc4f-8b2f-4b78-a59c-78d1f1d828fa_Sender"));

            entity.HasIndex(e => new { e.FkAccountId, e.TransactionCodeId, e.Active, e.TransactionDate }, "EntityBankStatement_GETGRAPHDATA");

            entity.HasIndex(e => e.Reconciled, "IX_EntityBankStatement");

            entity.HasIndex(e => new { e.FkAccountId, e.FkEntityId }, "IX_EntityBankStatement_Reconciled").HasFilter("([Reconciled]=(1) AND [Active]=(1))");

            entity.Property(e => e.PkEntityBankStatementId)
                .HasDefaultValueSql("(newid())", "DF_EntityBankStatement_PkEntityBankStatementID")
                .HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DateMapped).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkBankInfoId).HasColumnName("FkBankInfoID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.IsIntra).HasDefaultValue(false);
            entity.Property(e => e.IsProcessed).HasDefaultValue(false);
            entity.Property(e => e.Reconciled).HasDefaultValue(true);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionCodeId).HasDefaultValue(1);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkAccount).WithMany(p => p.EntityBankStatements)
                .HasForeignKey(d => d.FkAccountId)
                .HasConstraintName("FK_EntityBankStatement_Account");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityBankStatements)
                .HasForeignKey(d => d.FkEntityId)
                .HasConstraintName("FK_EntityBankStatement_Entity");

            entity.HasOne(d => d.TransactionCode).WithMany(p => p.EntityBankStatements)
                .HasForeignKey(d => d.TransactionCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityBan__Trans__01FE2E39");
        });

        modelBuilder.Entity<EntityBankStatementLog>(entity =>
        {
            entity.HasKey(e => e.PkEntityBankStatementLog).HasName("PK__EntityBa__913CCD58DE678AF5");

            entity.ToTable("EntityBankStatementLog");

            entity.Property(e => e.PkEntityBankStatementLog).ValueGeneratedNever();
            entity.Property(e => e.FkentityBankStatementId).HasColumnName("FKEntityBankStatementId");
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.IsCopied).HasDefaultValue(false, "DF__EntityBan__IsCop__21D7979C");
            entity.Property(e => e.UpdateDescription).IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkentityBankStatement).WithMany(p => p.EntityBankStatementLogs)
                .HasForeignKey(d => d.FkentityBankStatementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityBan__FKEnt__6AF17706");

            entity.HasOne(d => d.Fkuser).WithMany(p => p.EntityBankStatementLogs)
                .HasForeignKey(d => d.FkuserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityBan__FKUse__69FD52CD");
        });

        modelBuilder.Entity<EntityBankStatementTemp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("EntityBankStatementTemp");

            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DateMapped).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkBankInfoId).HasColumnName("FkBankInfoID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<EntityConfig>(entity =>
        {
            entity.HasKey(e => e.PkEntityConfigId).HasName("PK__EntityCo__17129861CC4B4081");

            entity.ToTable("EntityConfig");

            entity.HasIndex(e => e.FkEntityIdFees, "IX_EntityConfig_FkEntityIdFees");

            entity.Property(e => e.PkEntityConfigId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityConfigID");
            entity.Property(e => e.AddFees).HasDefaultValue(false);
            entity.Property(e => e.AllowUnallocated).HasDefaultValue(false);
            entity.Property(e => e.Apiroute)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("APIRoute");
            entity.Property(e => e.BlanketOver).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ConsumerDescr)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DisableDuplicateCheck).HasDefaultValue(false);
            entity.Property(e => e.DisableRegistrationNotification).HasDefaultValue(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmailPlaceholder)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.HomeUrl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HomeURL");
            entity.Property(e => e.OpenLoop).HasDefaultValue(false);
            entity.Property(e => e.RegistrationUrl)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RegistrationURL");
            entity.Property(e => e.ReportHeader)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RepresentativeDescr)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SmsauthToken)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SMSAuthToken");
            entity.Property(e => e.VendorDescr)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkAccount).WithMany(p => p.EntityConfigs)
                .HasForeignKey(d => d.FkAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityCon__FkAcc__1D9C270D");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityConfigFkEntities)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityCon__FkEnt__1CA802D4");

            entity.HasOne(d => d.FkEntityIdFeesNavigation).WithMany(p => p.EntityConfigFkEntityIdFeesNavigations).HasForeignKey(d => d.FkEntityIdFees);

            entity.HasOne(d => d.FkGroup).WithMany(p => p.EntityConfigs)
                .HasForeignKey(d => d.FkGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityCon__FkGro__1BB3DE9B");

            entity.HasOne(d => d.FkUser).WithMany(p => p.EntityConfigs)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityCon__FkUse__1E904B46");
        });

        modelBuilder.Entity<EntityConfigAccount>(entity =>
        {
            entity.HasKey(e => e.PkEntityConfigAccountId).HasName("PK__EntityCo__87CC8B6497A95A82");

            entity.ToTable("EntityConfigAccount");

            entity.Property(e => e.PkEntityConfigAccountId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityConfigAccountID");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkConfigAccountTypeId).HasColumnName("FkConfigAccountTypeID");
            entity.Property(e => e.FkEntityConfigId).HasColumnName("FkEntityConfigID");

            entity.HasOne(d => d.FkAccount).WithMany(p => p.EntityConfigAccounts)
                .HasForeignKey(d => d.FkAccountId)
                .HasConstraintName("FK__EntityCon__FkAcc__26316D0E");

            entity.HasOne(d => d.FkConfigAccountType).WithMany(p => p.EntityConfigAccounts)
                .HasForeignKey(d => d.FkConfigAccountTypeId)
                .HasConstraintName("FK__EntityCon__FkCon__27259147");

            entity.HasOne(d => d.FkEntityConfig).WithMany(p => p.EntityConfigAccounts)
                .HasForeignKey(d => d.FkEntityConfigId)
                .HasConstraintName("FK__EntityCon__FkEnt__253D48D5");
        });

        modelBuilder.Entity<EntityContact>(entity =>
        {
            entity.HasKey(e => e.PkEntityContactId)
                .HasName("PK_SupplierContact")
                .HasFillFactor(90);

            entity.ToTable("EntityContact");

            entity.HasIndex(e => e.FkEntityId, "NCI_EntityContact_FKEntityID");

            entity.Property(e => e.PkEntityContactId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityContactID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Cellphone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(550)
                .IsUnicode(false);
            entity.Property(e => e.EmailNotification).HasDefaultValue(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityAddressId).HasColumnName("FkEntityAddressID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkTitleId).HasColumnName("FkTitleID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.Initials)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastnName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Primaray).HasDefaultValue(false);
            entity.Property(e => e.Smsnotification)
                .HasDefaultValue(false)
                .HasColumnName("SMSNotification");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityContacts)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierContact_Supplier");

            entity.HasOne(d => d.FkTitle).WithMany(p => p.EntityContacts)
                .HasForeignKey(d => d.FkTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierContact_Title");

            entity.HasOne(d => d.FkUser).WithMany(p => p.EntityContacts)
                .HasForeignKey(d => d.FkUserId)
                .HasConstraintName("FK_EntityContact_User");
        });

        modelBuilder.Entity<EntityContactPending>(entity =>
        {
            entity.HasKey(e => e.PkEntityContactId).HasFillFactor(90);

            entity.ToTable("EntityContactPending");

            entity.Property(e => e.PkEntityContactId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityContactID");
            entity.Property(e => e.Cellphone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmailNotification).HasDefaultValue(false);
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityAddressId).HasColumnName("FkEntityAddressID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkTitleId).HasColumnName("FkTitleID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.Initials)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastnName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Primaray).HasDefaultValue(false);
            entity.Property(e => e.Smsnotification)
                .HasDefaultValue(false)
                .HasColumnName("SMSNotification");
            entity.Property(e => e.Telephone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ToDelete).HasDefaultValue(false);

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityContactPendings)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityContactPending_Supplier");

            entity.HasOne(d => d.FkTitle).WithMany(p => p.EntityContactPendings)
                .HasForeignKey(d => d.FkTitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityContactPending_Title");

            entity.HasOne(d => d.FkUser).WithMany(p => p.EntityContactPendings)
                .HasForeignKey(d => d.FkUserId)
                .HasConstraintName("FK_EntityContactPending_User");
        });

        modelBuilder.Entity<EntityDataApproval>(entity =>
        {
            entity.HasKey(e => e.PkEntityApprovalId).HasFillFactor(90);

            entity.ToTable("EntityDataApproval");

            entity.Property(e => e.PkEntityApprovalId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityApprovalID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Edit).HasDefaultValue(false, "DF_EntityDataApproval_Edit");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkFromUserId).HasColumnName("FkFromUserID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.IOrder).HasColumnName("iOrder");
            entity.Property(e => e.New).HasDefaultValue(false, "DF_EntityDataApproval_New");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityDataApprovals)
                .HasForeignKey(d => d.FkEntityId)
                .HasConstraintName("FK_EntityDataApproval_Entity");

            entity.HasOne(d => d.FkFromUser).WithMany(p => p.EntityDataApprovalFkFromUsers)
                .HasForeignKey(d => d.FkFromUserId)
                .HasConstraintName("FK_EntityDataApproval_FromUser");

            entity.HasOne(d => d.FkUser).WithMany(p => p.EntityDataApprovalFkUsers)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityDataApproval_User");
        });

        modelBuilder.Entity<EntityEntityType>(entity =>
        {
            entity.HasKey(e => e.PkEntityEntityTypeId).HasFillFactor(90);

            entity.ToTable("EntityEntityType");

            entity.HasIndex(e => e.FkEntityTypeId, "NCI_FKEntityTypeID_FKEntityID");

            entity.Property(e => e.PkEntityEntityTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityEntityTypeID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityEntityTypes)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityEntityType_Entity");

            entity.HasOne(d => d.FkEntityType).WithMany(p => p.EntityEntityTypes)
                .HasForeignKey(d => d.FkEntityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityEntityType_EntityType");
        });

        modelBuilder.Entity<EntityGroup>(entity =>
        {
            entity.HasKey(e => e.PkEntityGroupId).HasFillFactor(90);

            entity.ToTable("EntityGroup");

            entity.Property(e => e.PkEntityGroupId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityGroupID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityGroups)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityGroup_Entity");

            entity.HasOne(d => d.FkGroup).WithMany(p => p.EntityGroups)
                .HasForeignKey(d => d.FkGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityGroup_Group");
        });

        modelBuilder.Entity<EntityModule>(entity =>
        {
            entity.HasKey(e => e.PkEntityModuleId).HasFillFactor(90);

            entity.ToTable("EntityModule");

            entity.Property(e => e.PkEntityModuleId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityModuleID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkModuleId).HasColumnName("FkModuleID");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityModules)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityModule_EntityModule");

            entity.HasOne(d => d.FkModule).WithMany(p => p.EntityModules)
                .HasForeignKey(d => d.FkModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityModule_Module");
        });

        modelBuilder.Entity<EntityOrderSweep>(entity =>
        {
            entity.HasKey(e => e.PkEntityOrderSweepId);

            entity.ToTable("EntityOrderSweep");

            entity.Property(e => e.PkEntityOrderSweepId).HasColumnName("PkEntityOrderSweepID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOrderSweepFrequencyId).HasColumnName("FkOrderSweepFrequencyID");
            entity.Property(e => e.LastProcessed).HasColumnType("datetime");

            entity.HasOne(d => d.FkAccount).WithMany(p => p.EntityOrderSweeps)
                .HasForeignKey(d => d.FkAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityOrderSweep_Account");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.EntityOrderSweeps)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityOrderSweep_Entity");

            entity.HasOne(d => d.FkOrderStatus).WithMany(p => p.EntityOrderSweeps)
                .HasForeignKey(d => d.FkOrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityOrderSweep_OrderStatus");

            entity.HasOne(d => d.FkOrderSweepFrequency).WithMany(p => p.EntityOrderSweeps)
                .HasForeignKey(d => d.FkOrderSweepFrequencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityOrderSweep_OrderSweepFrequency");
        });

        modelBuilder.Entity<EntityOrderSweepLog>(entity =>
        {
            entity.HasKey(e => e.PkEntityOrderSweepLogId);

            entity.ToTable("EntityOrderSweepLog");

            entity.Property(e => e.PkEntityOrderSweepLogId).HasColumnName("PkEntityOrderSweepLogID");
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.FkEntityOrderSweepId).HasColumnName("FkEntityOrderSweepID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.Processed).HasDefaultValue(true);
            entity.Property(e => e.SweepDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkEntityOrderSweep).WithMany(p => p.EntityOrderSweepLogs)
                .HasForeignKey(d => d.FkEntityOrderSweepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityOrderSweepLog_EntityOrderSweep");

            entity.HasOne(d => d.FkOrder).WithMany(p => p.EntityOrderSweepLogs)
                .HasForeignKey(d => d.FkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityOrderSweepLog_Order");
        });

        modelBuilder.Entity<EntityPending>(entity =>
        {
            entity.HasKey(e => e.PkEntityId).HasFillFactor(90);

            entity.ToTable("EntityPending");

            entity.Property(e => e.PkEntityId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())", "DF_EntityPending_DateCreated")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntityReferenceAlias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Featured).HasDefaultValue(false);
            entity.Property(e => e.FkEntitySubTypeId).HasColumnName("FkEntitySubTypeID");
            entity.Property(e => e.FkIdentificationTypeId).HasColumnName("FkIdentificationTypeID");
            entity.Property(e => e.FkRequestedUserId).HasColumnName("FkRequestedUserID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsNew).HasDefaultValue(false, "DF_EntityPending_IsNew");
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RejectReason)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatexemption)
                .HasDefaultValue(false)
                .HasColumnName("VATExemption");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
            entity.Property(e => e.Vatregistered)
                .HasDefaultValue(false)
                .HasColumnName("VATRegistered");
            entity.Property(e => e.WbulkLimitSgn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WBulkLimitSgn");
            entity.Property(e => e.WbulkPaymentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WBulkPaymentType");

            entity.HasOne(d => d.FkEntitySubType).WithMany(p => p.EntityPendings)
                .HasForeignKey(d => d.FkEntitySubTypeId)
                .HasConstraintName("FK_EntityPending_EntitySubType");

            entity.HasOne(d => d.FkRequestedUser).WithMany(p => p.EntityPendings)
                .HasForeignKey(d => d.FkRequestedUserId)
                .HasConstraintName("FK_EntityPending_RequestedUser");

            entity.HasOne(d => d.FkStatus).WithMany(p => p.EntityPendings)
                .HasForeignKey(d => d.FkStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityPending_SupplierStatus");
        });

        modelBuilder.Entity<EntityRecordApproval>(entity =>
        {
            entity.HasKey(e => e.PkentityRecordApprovalId).HasName("PK__EntityRe__3C1323580D4AEE57");

            entity.ToTable("EntityRecordApproval");

            entity.Property(e => e.PkentityRecordApprovalId)
                .ValueGeneratedNever()
                .HasColumnName("PKEntityRecordApprovalId");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ApprovalDate).HasColumnType("datetime");
            entity.Property(e => e.FkentityDataApprovalId).HasColumnName("FKEntityDataApprovalId");
            entity.Property(e => e.FkentityId).HasColumnName("FKEntityId");
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");

            entity.HasOne(d => d.FkApprovalStatusType).WithMany(p => p.EntityRecordApprovals)
                .HasForeignKey(d => d.FkApprovalStatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityRec__FkApp__75A40FA3");

            entity.HasOne(d => d.FkentityDataApproval).WithMany(p => p.EntityRecordApprovals)
                .HasForeignKey(d => d.FkentityDataApprovalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityDataApprovalId");

            entity.HasOne(d => d.Fkentity).WithMany(p => p.EntityRecordApprovals)
                .HasForeignKey(d => d.FkentityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityRec__FKEnt__73BBC731");

            entity.HasOne(d => d.Fkuser).WithMany(p => p.EntityRecordApprovals)
                .HasForeignKey(d => d.FkuserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntityRec__FKUse__74AFEB6A");
        });

        modelBuilder.Entity<EntityStatus>(entity =>
        {
            entity.HasKey(e => e.PkStatusId)
                .HasName("PK_SupplierStatus")
                .HasFillFactor(90);

            entity.ToTable("EntityStatus");

            entity.Property(e => e.PkStatusId)
                .ValueGeneratedNever()
                .HasColumnName("PkStatusID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EntityStatusLog>(entity =>
        {
            entity.HasKey(e => e.PkentityStatusLogId).HasName("PK__EntitySt__C13F0735774CACDF");

            entity.ToTable("EntityStatusLog");

            entity.Property(e => e.PkentityStatusLogId)
                .ValueGeneratedNever()
                .HasColumnName("PKEntityStatusLogId");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkentityId).HasColumnName("FKEntityId");
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedStatus)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Fkentity).WithMany(p => p.EntityStatusLogs)
                .HasForeignKey(d => d.FkentityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntitySta__FKEnt__65A2B204");

            entity.HasOne(d => d.Fkuser).WithMany(p => p.EntityStatusLogs)
                .HasForeignKey(d => d.FkuserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntitySta__FKUse__6696D63D");
        });

        modelBuilder.Entity<EntitySubType>(entity =>
        {
            entity.HasKey(e => e.PkEntitySubTypeId).HasFillFactor(90);

            entity.ToTable("EntitySubType");

            entity.Property(e => e.PkEntitySubTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntitySubTypeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EntityType>(entity =>
        {
            entity.HasKey(e => e.PkEntityTypeId)
                .HasName("PK_SupplierType")
                .HasFillFactor(90);

            entity.ToTable("EntityType");

            entity.Property(e => e.PkEntityTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkEntityTypeID");
            entity.Property(e => e.DefaultLink)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EtguuserType>(entity =>
        {
            entity.HasKey(e => e.PkEtguuserTypeId)
                .HasName("PK_EntityTypeGroupUserType")
                .HasFillFactor(90);

            entity.ToTable("ETGUUserType");

            entity.Property(e => e.PkEtguuserTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkETGUUserTypeID");
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");
            entity.Property(e => e.FkUserTypeId).HasColumnName("FkUserTypeID");
            entity.Property(e => e.FkuserId).HasColumnName("FKUserID");

            entity.HasOne(d => d.FkEntityType).WithMany(p => p.EtguuserTypes)
                .HasForeignKey(d => d.FkEntityTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityTypeGroupUserType_EntityType");

            entity.HasOne(d => d.FkGroup).WithMany(p => p.EtguuserTypes)
                .HasForeignKey(d => d.FkGroupId)
                .HasConstraintName("FK_EntityTypeGroupUserType_Group");

            entity.HasOne(d => d.FkUserType).WithMany(p => p.EtguuserTypes)
                .HasForeignKey(d => d.FkUserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EntityTypeGroupUserType_UserType");

            entity.HasOne(d => d.Fkuser).WithMany(p => p.EtguuserTypes)
                .HasForeignKey(d => d.FkuserId)
                .HasConstraintName("FK_ETGUUserType_User");
        });

        modelBuilder.Entity<Fnb3PimApiCallLog>(entity =>
        {
            entity.ToTable("Fnb3PimApiCallLog");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EndToEndId).HasMaxLength(140);
            entity.Property(e => e.EndpointName).HasMaxLength(50);
            entity.Property(e => e.Environment).HasMaxLength(80);
            entity.Property(e => e.HttpMethod).HasMaxLength(10);
            entity.Property(e => e.InstructionId).HasMaxLength(120);
            entity.Property(e => e.MaskedErrorSummary).HasMaxLength(2000);
            entity.Property(e => e.MsgId).HasMaxLength(80);
            entity.Property(e => e.OrderId).HasMaxLength(64);
            entity.Property(e => e.PmtInfId).HasMaxLength(80);
            entity.Property(e => e.RequestHash).HasMaxLength(128);
            entity.Property(e => e.ResponseHash).HasMaxLength(128);
            entity.Property(e => e.UrlTemplate).HasMaxLength(500);
            entity.Property(e => e.XrequestId)
                .HasMaxLength(120)
                .HasColumnName("XRequestId");
        });

        modelBuilder.Entity<Fnb3PimImportWatermark>(entity =>
        {
            entity.ToTable("Fnb3PimImportWatermark");

            entity.HasIndex(e => e.AccountId, "UX_Fnb3PimImportWatermark_AccountId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AccountId).HasMaxLength(64);
            entity.Property(e => e.InvestmentAccountNumber).HasMaxLength(64);
            entity.Property(e => e.LastError).HasMaxLength(2000);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Fnb3PimOrderQueue>(entity =>
        {
            entity.ToTable("Fnb3PimOrderQueue");

            entity.HasIndex(e => new { e.Status, e.NextAttemptUtc, e.LockedUntilUtc }, "IX_Fnb3PimOrderQueue_Worker");

            entity.HasIndex(e => e.OrderId, "UX_Fnb3PimOrderQueue_ActiveOrder")
                .IsUnique()
                .HasFilter("([Status]<>'Cancelled' AND [Status]<>'Failed' AND [Status]<>'Reconciled')");

            entity.HasIndex(e => e.InstructionId, "UX_Fnb3PimOrderQueue_InstructionId")
                .IsUnique()
                .HasFilter("([InstructionId] IS NOT NULL)");

            entity.HasIndex(e => e.MsgId, "UX_Fnb3PimOrderQueue_MsgId").IsUnique();

            entity.HasIndex(e => e.PmtInfId, "UX_Fnb3PimOrderQueue_PmtInfId").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreditorAccountNumber).HasMaxLength(64);
            entity.Property(e => e.CreditorBranchCode).HasMaxLength(16);
            entity.Property(e => e.CreditorName).HasMaxLength(140);
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Debtor3PimInvestmentAccount).HasMaxLength(64);
            entity.Property(e => e.DebtorAgentTrustAccountNumber).HasMaxLength(64);
            entity.Property(e => e.EndToEndId).HasMaxLength(140);
            entity.Property(e => e.FromAccountId).HasMaxLength(64);
            entity.Property(e => e.InstructionId).HasMaxLength(120);
            entity.Property(e => e.LastErrorCode).HasMaxLength(100);
            entity.Property(e => e.LastErrorDescription).HasMaxLength(2000);
            entity.Property(e => e.LockedBy).HasMaxLength(120);
            entity.Property(e => e.LogicalRequestHash).HasMaxLength(128);
            entity.Property(e => e.MsgId).HasMaxLength(80);
            entity.Property(e => e.OrderId).HasMaxLength(64);
            entity.Property(e => e.OrderNumber).HasMaxLength(80);
            entity.Property(e => e.PmtInfId).HasMaxLength(80);
            entity.Property(e => e.ReferenceNumber).HasMaxLength(120);
            entity.Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.ServiceLevel).HasMaxLength(20);
            entity.Property(e => e.ServiceType).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Frequency>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Frequenc__3214EC2778C08975")
                .HasFillFactor(90);

            entity.ToTable("Frequency");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GenericLookUpTable>(entity =>
        {
            entity.HasKey(e => e.PkGenericLookUpTableId).HasFillFactor(90);

            entity.ToTable("GenericLookUpTable");

            entity.Property(e => e.PkGenericLookUpTableId)
                .ValueGeneratedNever()
                .HasColumnName("PkGenericLookUpTableID");
            entity.Property(e => e.FkModuleId).HasColumnName("FkModuleID");
            entity.Property(e => e.PageName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkModule).WithMany(p => p.GenericLookUpTables)
                .HasForeignKey(d => d.FkModuleId)
                .HasConstraintName("FK_GenericLookUpTable_Module");
        });

        modelBuilder.Entity<GetProductTransactionCodeId>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetProductTransactionCodeID");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.TransactionCodeId).HasColumnName("TransactionCodeID");
            entity.Property(e => e.TransactionDescription).HasMaxLength(200);
        });

        modelBuilder.Entity<GlobalPublicRecipient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GlobalPublicRecipient");

            entity.Property(e => e.Branch)
                .HasMaxLength(50)
                .HasColumnName("BRANCH");
            entity.Property(e => e.Name0)
                .HasMaxLength(50)
                .HasColumnName("NAME0");
            entity.Property(e => e.RecipNo)
                .HasMaxLength(50)
                .HasColumnName("RECIP_NO");
        });

        modelBuilder.Entity<Grid>(entity =>
        {
            entity.HasKey(e => e.PkGridId).HasFillFactor(90);

            entity.ToTable("Grid");

            entity.Property(e => e.PkGridId)
                .ValueGeneratedNever()
                .HasColumnName("PkGridID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GridZone>(entity =>
        {
            entity.HasKey(e => e.PkGridZoneId).HasFillFactor(90);

            entity.ToTable("GridZone");

            entity.Property(e => e.PkGridZoneId)
                .ValueGeneratedNever()
                .HasColumnName("PkGridZoneID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkGridId).HasColumnName("FkGridID");

            entity.HasOne(d => d.FkGrid).WithMany(p => p.GridZones)
                .HasForeignKey(d => d.FkGridId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GridZone_Grid");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.PkGroupId).HasFillFactor(90);

            entity.ToTable("Group");

            entity.Property(e => e.PkGroupId)
                .ValueGeneratedNever()
                .HasColumnName("PkGroupID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Deleted).HasDefaultValue(false, "DF_Group_Deleted");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<H2hheader>(entity =>
        {
            entity.HasKey(e => e.PkSequenceId)
                .HasName("PK_H2HSync")
                .HasFillFactor(90);

            entity.ToTable("H2HHeader");

            entity.Property(e => e.PkSequenceId).HasColumnName("PkSequenceID");
            entity.Property(e => e.Error).HasDefaultValue(false);
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FinAudDate).HasColumnType("datetime");
            entity.Property(e => e.IncreaseSequence).HasDefaultValue(true);
            entity.Property(e => e.IntAudDate).HasColumnType("datetime");
            entity.Property(e => e.SendDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<H2hheaderAbsa>(entity =>
        {
            entity.HasKey(e => e.PkSequenceId)
                .HasName("PK_H2HABSASync")
                .HasFillFactor(90);

            entity.ToTable("H2HHeaderABSA");

            entity.Property(e => e.PkSequenceId).HasColumnName("PkSequenceID");
            entity.Property(e => e.Error).HasDefaultValue(false);
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FinAudDate).HasColumnType("datetime");
            entity.Property(e => e.IncreaseSequence).HasDefaultValue(true);
            entity.Property(e => e.IntAudDate).HasColumnType("datetime");
            entity.Property(e => e.SendDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<H2hheaderFnb>(entity =>
        {
            entity.HasKey(e => e.PkSequenceId).HasFillFactor(90);

            entity.ToTable("H2HHeaderFNB");

            entity.Property(e => e.PkSequenceId).HasColumnName("PkSequenceID");
            entity.Property(e => e.AccountHash)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.AmountHash)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.SendDate).HasColumnType("datetime");
            entity.Property(e => e.UserCode)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<H2hheaderNedbank>(entity =>
        {
            entity.HasKey(e => e.PkSequenceId)
                .HasName("PK__H2HHeade__BF625F6323B427BF")
                .HasFillFactor(90);

            entity.ToTable("H2HHeaderNedbank");

            entity.Property(e => e.PkSequenceId).HasColumnName("PkSequenceID");
            entity.Property(e => e.ClientCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ErrorMessage).HasMaxLength(50);
            entity.Property(e => e.SendDate)
                .HasDefaultValueSql("(getdate())", "DF__H2HHeader__SendD__259C7031")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<H2hvalidationStatus>(entity =>
        {
            entity.HasKey(e => e.PkH2hvalidationStatusId).HasFillFactor(90);

            entity.ToTable("H2HValidationStatus");

            entity.Property(e => e.PkH2hvalidationStatusId)
                .ValueGeneratedNever()
                .HasColumnName("PkH2HValidationStatusID");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HelpContent>(entity =>
        {
            entity.HasKey(e => e.PkHelpContentId).HasName("PK__HelpCont__1AE43F302E12AEEB");

            entity.ToTable("HelpContent");

            entity.Property(e => e.PkHelpContentId)
                .ValueGeneratedNever()
                .HasColumnName("PkHelpContentID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.LegacyId).HasColumnName("LegacyID");
            entity.Property(e => e.Title)
                .HasMaxLength(64)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistoryCategory>(entity =>
        {
            entity.HasKey(e => e.PkHistoryCategoryId).HasName("PK__HistoryC__CAF622A33BA0271D");

            entity.ToTable("HistoryCategory");

            entity.Property(e => e.PkHistoryCategoryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PkHistoryCategoryID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Holiday__3214EC274FDD13F4")
                .HasFillFactor(90);

            entity.ToTable("Holiday");

            entity.HasIndex(e => e.Date, "ncIdx_Holiday_Date");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Note)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IdentificationType>(entity =>
        {
            entity.HasKey(e => e.PkIdentificationTypeId).HasFillFactor(90);

            entity.ToTable("IdentificationType");

            entity.Property(e => e.PkIdentificationTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkIdentificationTypeID");
            entity.Property(e => e.IdentificationTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IntegratedSystem>(entity =>
        {
            entity.HasKey(e => e.PkSystemId)
                .HasName("PK__Integrat__BC57CF68A4EA0A9B")
                .HasFillFactor(90);

            entity.ToTable("IntegratedSystem");

            entity.Property(e => e.PkSystemId).HasColumnName("PkSystemID");
            entity.Property(e => e.Active).HasDefaultValue(true, "DF__Integrate__Activ__3B0D59BA");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())", "DF__Integrate__DateC__6A26A4A8")
                .HasColumnType("datetime");
            entity.Property(e => e.FkEntityConfigId).HasColumnName("FkEntityConfigID");
            entity.Property(e => e.IsApi)
                .HasDefaultValue(true, "DF__Integrate__IsAPI__3A193581")
                .HasColumnName("IsAPI");
            entity.Property(e => e.IsNative).HasDefaultValue(false, "DF__Integrate__IsNat__6009D43F");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Origin)
                .HasMaxLength(255)
                .HasDefaultValue("http://localhost:8080", "DF__Integrate__Origi__17AE438D");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.FkEntityConfig).WithMany(p => p.IntegratedSystems)
                .HasForeignKey(d => d.FkEntityConfigId)
                .HasConstraintName("FkEntityConfig");
        });

        modelBuilder.Entity<IntegrationError>(entity =>
        {
            entity.HasKey(e => e.PkIntegrationErrorId).HasFillFactor(90);

            entity.ToTable("IntegrationError");

            entity.Property(e => e.PkIntegrationErrorId)
                .ValueGeneratedNever()
                .HasColumnName("PkIntegrationErrorID");
            entity.Property(e => e.EntryDateTime)
                .HasDefaultValueSql("(getdate())", "DF_IntegrationError_EntryDateTime")
                .HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(5000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<IpAddress>(entity =>
        {
            entity.HasKey(e => e.PkIpAddressId).HasFillFactor(90);

            entity.ToTable("IpAddress");

            entity.Property(e => e.PkIpAddressId)
                .ValueGeneratedNever()
                .HasColumnName("PkIpAddressID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DateAdded).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InUse).HasDefaultValue(true);
            entity.Property(e => e.IpAddress1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IpAddress");
        });

        modelBuilder.Entity<IsopainMessagesAbsa>(entity =>
        {
            entity.HasKey(e => e.PkisoPainMessageId);

            entity.ToTable("ISOPainMessagesABSA");

            entity.Property(e => e.PkisoPainMessageId).HasColumnName("PKIsoPainMessageId");
            entity.Property(e => e.Accepted).HasDefaultValue(false, "DF_ISOPainMessagesABSA_Accepted");
            entity.Property(e => e.AcceptedDate).HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MessageDate).HasColumnType("datetime");
            entity.Property(e => e.MessageId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pending).HasDefaultValue(false, "DF_ISOPainMessagesABSA_Pending");
            entity.Property(e => e.Rejected).HasDefaultValue(false, "DF_ISOPainMessagesABSA_Rejected");
            entity.Property(e => e.RejectedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<IsopainMessagesFnb>(entity =>
        {
            entity.HasKey(e => e.PkisoPainMessageId);

            entity.ToTable("ISOPainMessagesFNB");

            entity.Property(e => e.PkisoPainMessageId).HasColumnName("PKIsoPainMessageId");
            entity.Property(e => e.Accepted).HasDefaultValue(false, "DF_ISOPainMessagesFNB_Accepted");
            entity.Property(e => e.AcceptedDate).HasColumnType("datetime");
            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MessageDate).HasColumnType("datetime");
            entity.Property(e => e.MessageId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pending).HasDefaultValue(false, "DF_ISOPainMessagesFNB_Pending");
            entity.Property(e => e.Rejected).HasDefaultValue(false, "DF_ISOPainMessagesFNB_Rejected");
            entity.Property(e => e.RejectedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ItemClass>(entity =>
        {
            entity.HasKey(e => e.PkClassId)
                .HasName("PK_OMIDFClass")
                .HasFillFactor(90);

            entity.ToTable("ItemClass");

            entity.Property(e => e.PkClassId)
                .HasDefaultValueSql("(newid())", "DF_OMIDFClass_PkClassID")
                .HasColumnName("PkClassID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ClassDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ItemClassCatIncluded>(entity =>
        {
            entity.HasKey(e => e.PkCategoryIncludedId).HasFillFactor(90);

            entity.ToTable("ItemClassCatIncluded");

            entity.Property(e => e.PkCategoryIncludedId)
                .ValueGeneratedNever()
                .HasColumnName("PkCategoryIncludedID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.Included).HasDefaultValue(true);
        });

        modelBuilder.Entity<ItemClassCatagory>(entity =>
        {
            entity.HasKey(e => e.PkClassCatagoryId)
                .HasName("PK_OMIDFClassCatagory")
                .HasFillFactor(90);

            entity.ToTable("ItemClassCatagory");

            entity.Property(e => e.PkClassCatagoryId)
                .HasDefaultValueSql("(newid())", "DF_OMIDFClassCatagory_PkClassCatagoryID")
                .HasColumnName("PkClassCatagoryID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ClassCatDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Enabled).HasDefaultValue(true);
            entity.Property(e => e.FkClassId).HasColumnName("FkClassID");
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<LoginAudit>(entity =>
        {
            entity.HasKey(e => e.PkLoginAuditId).HasFillFactor(90);

            entity.ToTable("LoginAudit");

            entity.Property(e => e.PkLoginAuditId)
                .HasDefaultValueSql("(newid())", "DF_LoginAudit_PkLoginAuditID")
                .HasColumnName("PkLoginAuditID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.LoginDate).HasColumnType("datetime");
            entity.Property(e => e.LoginSuccess).HasDefaultValue(false);
            entity.Property(e => e.Pin)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Psssword)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.FkUser).WithMany(p => p.LoginAudits)
                .HasForeignKey(d => d.FkUserId)
                .HasConstraintName("FK_LoginAudit_User");
        });

        modelBuilder.Entity<LowBalanceNotification>(entity =>
        {
            entity.HasKey(e => e.PkLowBalancaNotificationId).HasFillFactor(90);

            entity.ToTable("LowBalanceNotification");

            entity.Property(e => e.PkLowBalancaNotificationId)
                .HasDefaultValueSql("(newid())", "DF_LowBalanceNotification_PkLowBalancaNotificationID")
                .HasColumnName("PkLowBalancaNotificationID");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityAccountId).HasColumnName("FkEntityAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkSupplierClientId).HasColumnName("FkSupplierClientID");
            entity.Property(e => e.LowBalanceRecipients)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LowBalanceRuleSetting)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LowBalanceValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ProcessedForTheDay).HasDefaultValue(false);
        });

        modelBuilder.Entity<MagisterialDistrict>(entity =>
        {
            entity.HasKey(e => e.PkMagisterialDistrictId).HasFillFactor(90);

            entity.ToTable("MagisterialDistrict");

            entity.Property(e => e.PkMagisterialDistrictId)
                .ValueGeneratedNever()
                .HasColumnName("PkMagisterialDistrictID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkProvinceId).HasColumnName("FkProvinceID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.FkProvince).WithMany(p => p.MagisterialDistricts)
                .HasForeignKey(d => d.FkProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MagisterialDistrict_Province");
        });

        modelBuilder.Entity<MagisterialDistrictGridZone>(entity =>
        {
            entity.HasKey(e => e.PkMagisterialDistrictGridZoneId).HasFillFactor(90);

            entity.ToTable("MagisterialDistrictGridZone");

            entity.Property(e => e.PkMagisterialDistrictGridZoneId)
                .ValueGeneratedNever()
                .HasColumnName("PkMagisterialDistrictGridZoneID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkGridZoneId).HasColumnName("FkGridZoneID");
            entity.Property(e => e.FkMagisterialDistrictId).HasColumnName("FkMagisterialDistrictID");

            entity.HasOne(d => d.FkGridZone).WithMany(p => p.MagisterialDistrictGridZones)
                .HasForeignKey(d => d.FkGridZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MagisterialDistrictGridZone_GridZone");

            entity.HasOne(d => d.FkMagisterialDistrict).WithMany(p => p.MagisterialDistrictGridZones)
                .HasForeignKey(d => d.FkMagisterialDistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MagisterialDistrictGridZone_MagisterialDistrict");
        });

        modelBuilder.Entity<MailTemplate>(entity =>
        {
            entity.HasKey(e => e.PkMailTemplate).HasFillFactor(90);

            entity.ToTable("MailTemplate");

            entity.Property(e => e.PkMailTemplate)
                .ValueGeneratedNever()
                .HasColumnName("pkMailTemplate");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.BodyText).HasColumnType("text");
            entity.Property(e => e.IsSms)
                .HasDefaultValue(false)
                .HasColumnName("IsSMS");
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MainView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("MainView");

            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DeviceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Make)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.PkMenuItemId).HasFillFactor(90);

            entity.ToTable("Menu");

            entity.Property(e => e.PkMenuItemId)
                .ValueGeneratedNever()
                .HasColumnName("PkMenuItemID");
            entity.Property(e => e.FkParentMenuItemId).HasColumnName("FkParentMenuItemID");
            entity.Property(e => e.IOrder).HasColumnName("iOrder");
            entity.Property(e => e.MenuName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SecondLevel).HasDefaultValue(false);
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("URL");

            entity.HasOne(d => d.FkParentMenuItem).WithMany(p => p.InverseFkParentMenuItem)
                .HasForeignKey(d => d.FkParentMenuItemId)
                .HasConstraintName("FK_Menu_Menu");
        });

        modelBuilder.Entity<Menu1>(entity =>
        {
            entity.HasKey(e => e.MenuId);

            entity.ToTable("Menus");

            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Url).HasMaxLength(100);
        });

        modelBuilder.Entity<MobileMartAccountStatus>(entity =>
        {
            entity.HasKey(e => e.PkMobileMartAccountStatusId);

            entity.ToTable("MobileMartAccountStatus");

            entity.HasIndex(e => e.FkMobileMartResponseId, "IX_MobileMartAccountStatus_FkMobileMartResponseId");

            entity.Property(e => e.PkMobileMartAccountStatusId).ValueGeneratedNever();

            entity.HasOne(d => d.FkMobileMartResponse).WithMany(p => p.MobileMartAccountStatuses).HasForeignKey(d => d.FkMobileMartResponseId);
        });

        modelBuilder.Entity<MobileMartProductsCategory>(entity =>
        {
            entity.HasKey(e => e.PkMobileMartProductsCategoryId);

            entity.Property(e => e.PkMobileMartProductsCategoryId).ValueGeneratedNever();
        });

        modelBuilder.Entity<MobileMartResponse>(entity =>
        {
            entity.HasKey(e => e.PkMobileMartResponseId);

            entity.HasIndex(e => e.FkOrderId, "IX_MobileMartResponses_FkOrderId");

            entity.HasIndex(e => e.FkReverseEntityBankStatementId, "IX_MobileMartResponses_FkReverseEntityBankStatementId");

            entity.HasIndex(e => e.FkUserId, "IX_MobileMartResponses_FkUserId");

            entity.Property(e => e.PkMobileMartResponseId).ValueGeneratedNever();
            entity.Property(e => e.ProductAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductAmountDue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductCategory).HasDefaultValue("");

            entity.HasOne(d => d.FkOrder).WithMany(p => p.MobileMartResponses).HasForeignKey(d => d.FkOrderId);

            entity.HasOne(d => d.FkReverseEntityBankStatement).WithMany(p => p.MobileMartResponses).HasForeignKey(d => d.FkReverseEntityBankStatementId);

            entity.HasOne(d => d.FkUser).WithMany(p => p.MobileMartResponses).HasForeignKey(d => d.FkUserId);
        });

        modelBuilder.Entity<MobileMartUtilityConsumerDetail>(entity =>
        {
            entity.HasKey(e => e.PkMobileMartConsumerDetailId);

            entity.HasIndex(e => e.FkMobileMartResponseId, "IX_MobileMartUtilityConsumerDetails_FkMobileMartResponseId");

            entity.Property(e => e.PkMobileMartConsumerDetailId).ValueGeneratedNever();

            entity.HasOne(d => d.FkMobileMartResponse).WithMany(p => p.MobileMartUtilityConsumerDetails).HasForeignKey(d => d.FkMobileMartResponseId);
        });

        modelBuilder.Entity<MobileMartUtilityMunicipalityDetail>(entity =>
        {
            entity.HasKey(e => e.PkMobileMartUtilityMunicipalityDetailId);

            entity.HasIndex(e => e.FkMobileMartResponseId, "IX_MobileMartUtilityMunicipalityDetails_FkMobileMartResponseId");

            entity.Property(e => e.PkMobileMartUtilityMunicipalityDetailId).ValueGeneratedNever();

            entity.HasOne(d => d.FkMobileMartResponse).WithMany(p => p.MobileMartUtilityMunicipalityDetails).HasForeignKey(d => d.FkMobileMartResponseId);
        });

        modelBuilder.Entity<MobileMartUtilityToken>(entity =>
        {
            entity.HasKey(e => e.PkMobileMartUtilityTokenId);

            entity.HasIndex(e => e.FkMobileMartResponseId, "IX_MobileMartUtilityTokens_FkMobileMartResponseId");

            entity.Property(e => e.PkMobileMartUtilityTokenId).ValueGeneratedNever();

            entity.HasOne(d => d.FkMobileMartResponse).WithMany(p => p.MobileMartUtilityTokens).HasForeignKey(d => d.FkMobileMartResponseId);
        });

        modelBuilder.Entity<MobileMartUtilityUnit>(entity =>
        {
            entity.HasKey(e => e.PkMobileMartUtilityUnitId);

            entity.HasIndex(e => e.FkMobileMartUtilityTokenId, "IX_MobileMartUtilityUnits_FkMobileMartUtilityTokenId");

            entity.Property(e => e.PkMobileMartUtilityUnitId).ValueGeneratedNever();

            entity.HasOne(d => d.FkMobileMartUtilityToken).WithMany(p => p.MobileMartUtilityUnits).HasForeignKey(d => d.FkMobileMartUtilityTokenId);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.PkModuleId)
                .HasName("PK_Modules")
                .HasFillFactor(90);

            entity.ToTable("Module");

            entity.Property(e => e.PkModuleId)
                .ValueGeneratedNever()
                .HasColumnName("PkModuleID");
            entity.Property(e => e.FkMenuItemId).HasColumnName("FkMenuItemID");
            entity.Property(e => e.Name)
                .HasMaxLength(65)
                .IsUnicode(false);

            entity.HasOne(d => d.FkMenuItem).WithMany(p => p.Modules)
                .HasForeignKey(d => d.FkMenuItemId)
                .HasConstraintName("FK_Module_Menu");
        });

        modelBuilder.Entity<NedSmtcode>(entity =>
        {
            entity.HasKey(e => e.PkNedSmscodeId)
                .HasName("PK__NedSMTCo__0038ABF92BB15E92")
                .HasFillFactor(90);

            entity.ToTable("NedSMTCode");

            entity.Property(e => e.PkNedSmscodeId).HasColumnName("PkNedSMSCodeId");
            entity.Property(e => e.Code)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Description).HasMaxLength(250);
        });

        modelBuilder.Entity<OpenIddictApplication>(entity =>
        {
            entity.HasIndex(e => e.ClientId, "IX_OpenIddictApplications_ClientId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ApplicationType).HasMaxLength(50);
            entity.Property(e => e.ClientId).HasMaxLength(100);
            entity.Property(e => e.ClientType).HasMaxLength(50);
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.ConsentType).HasMaxLength(50);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
        });

        modelBuilder.Entity<OpenIddictAuthorization>(entity =>
        {
            entity.HasIndex(e => new { e.ApplicationId, e.Status, e.Subject, e.Type }, "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Subject).HasMaxLength(400);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Application).WithMany(p => p.OpenIddictAuthorizations).HasForeignKey(d => d.ApplicationId);
        });

        modelBuilder.Entity<OpenIddictScope>(entity =>
        {
            entity.HasIndex(e => e.Name, "IX_OpenIddictScopes_Name");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0),0))");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<OpenIddictToken>(entity =>
        {
            entity.HasIndex(e => new { e.ApplicationId, e.Status, e.Subject, e.Type }, "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type");

            entity.HasIndex(e => e.AuthorizationId, "IX_OpenIddictTokens_AuthorizationId");

            entity.HasIndex(e => e.ReferenceId, "IX_OpenIddictTokens_ReferenceId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.ReferenceId).HasMaxLength(100);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Subject).HasMaxLength(400);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Application).WithMany(p => p.OpenIddictTokens).HasForeignKey(d => d.ApplicationId);

            entity.HasOne(d => d.Authorization).WithMany(p => p.OpenIddictTokens).HasForeignKey(d => d.AuthorizationId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.PkOrderId).HasFillFactor(90);

            entity.ToTable("Order");

            entity.HasIndex(e => e.OrderNumber, "AK_Order_OrderNumber").IsUnique();

            entity.HasIndex(e => e.FkFromAccountId, "NCI_Order_OrderIDClientIDClientAddrIDOrderDateOrderNumOrderStatusIDBankSeqUserIDBankStatIDGroupIDEtc");

            entity.Property(e => e.PkOrderId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.ExtRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAllocatedVendorId).HasColumnName("FkAllocatedVendorID");
            entity.Property(e => e.FkClientAddressId).HasColumnName("FkClientAddressID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkEntityBankStatementId).HasColumnName("FkEntityBankStatementID");
            entity.Property(e => e.FkFromAccountId).HasColumnName("FkFromAccountID");
            entity.Property(e => e.FkH2hvalidationStatusId).HasColumnName("FkH2HValidationStatusID");
            entity.Property(e => e.FkIsopainMessageId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FkISOPainMessageId");
            entity.Property(e => e.FkLinkedOrderId).HasColumnName("FkLinkedOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOrderTypeId)
                .HasDefaultValue(new Guid("93f857b1-6ae7-4107-b1ea-f60b776a8921"), "DF_Order_FkOrderTypeID_1")
                .HasColumnName("FkOrderTypeID");
            entity.Property(e => e.FkSupplierClientId).HasColumnName("FkSupplierClientID");
            entity.Property(e => e.FkToAccountId).HasColumnName("FkToAccountID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.FkWholesalerId).HasColumnName("FkWholesalerID");
            entity.Property(e => e.ForwardRequisition).HasDefaultValue(false);
            entity.Property(e => e.Grnreference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GRNReference");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber).ValueGeneratedOnAdd();
            entity.Property(e => e.Receipt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ServiceType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValue("01", "OrderProcessing");
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkAllocatedVendor).WithMany(p => p.OrderFkAllocatedVendors)
                .HasForeignKey(d => d.FkAllocatedVendorId)
                .HasConstraintName("FK_Order_Entity1");

            entity.HasOne(d => d.FkClientAddress).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkClientAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_EntityAddress");

            entity.HasOne(d => d.FkClient).WithMany(p => p.OrderFkClients)
                .HasForeignKey(d => d.FkClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Entity");

            entity.HasOne(d => d.FkEntityBankStatement).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkEntityBankStatementId)
                .HasConstraintName("FK_Order_EntityBankStatement");

            entity.HasOne(d => d.FkFromAccount).WithMany(p => p.OrderFkFromAccounts)
                .HasForeignKey(d => d.FkFromAccountId)
                .HasConstraintName("FK_Order_Account");

            entity.HasOne(d => d.FkH2hvalidationStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkH2hvalidationStatusId)
                .HasConstraintName("FK_Order_H2HValidationStatus");

            entity.HasOne(d => d.FkLinkedOrder).WithMany(p => p.InverseFkLinkedOrder)
                .HasForeignKey(d => d.FkLinkedOrderId)
                .HasConstraintName("FK_Order_Order");

            entity.HasOne(d => d.FkOrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkOrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_OrderStatus");

            entity.HasOne(d => d.FkOrderType).WithMany(p => p.Orders)
                .HasForeignKey(d => d.FkOrderTypeId)
                .HasConstraintName("FK_Order_OrderType");

            entity.HasOne(d => d.FkToAccount).WithMany(p => p.OrderFkToAccounts)
                .HasForeignKey(d => d.FkToAccountId)
                .HasConstraintName("FK_Order_Account1");

            entity.HasOne(d => d.FkWholesaler).WithMany(p => p.OrderFkWholesalers)
                .HasForeignKey(d => d.FkWholesalerId)
                .HasConstraintName("FK__Order__FkWholesa__66C02818");
        });

        modelBuilder.Entity<OrderAction>(entity =>
        {
            entity.HasKey(e => e.PkOrderActionId).HasFillFactor(90);

            entity.ToTable("OrderAction");

            entity.Property(e => e.PkOrderActionId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderActionID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderApproval>(entity =>
        {
            entity.HasKey(e => e.PkOrderApprovalId).HasFillFactor(90);

            entity.ToTable("OrderApproval");

            entity.Property(e => e.PkOrderApprovalId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderApprovalID");
            entity.Property(e => e.AdditionalInfo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ApprovalDate).HasColumnType("datetime");
            entity.Property(e => e.FkApprovalStatusTypeId).HasColumnName("FkApprovalStatusTypeID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

            entity.HasOne(d => d.FkApprovalStatusType).WithMany(p => p.OrderApprovals)
                .HasForeignKey(d => d.FkApprovalStatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderApproval_ApprovalStatusType");

            entity.HasOne(d => d.FkOrder).WithMany(p => p.OrderApprovals)
                .HasForeignKey(d => d.FkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderApproval_Order");

            entity.HasOne(d => d.FkOrderStatus).WithMany(p => p.OrderApprovals)
                .HasForeignKey(d => d.FkOrderStatusId)
                .HasConstraintName("FK_OrderApproval_OrderStatus");

            entity.HasOne(d => d.FkUser).WithMany(p => p.OrderApprovals)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderApproval_User");
        });

        modelBuilder.Entity<OrderCommission>(entity =>
        {
            entity.HasKey(e => e.PkOrderCommissionId).HasFillFactor(90);

            entity.ToTable("OrderCommission");

            entity.Property(e => e.PkOrderCommissionId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderCommissionID");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkCommissionTypeId).HasColumnName("FkCommissionTypeID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.VatRegistered).HasDefaultValue(false);

            entity.HasOne(d => d.FkAgent).WithMany(p => p.OrderCommissionFkAgents)
                .HasForeignKey(d => d.FkAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderCommission_Agent");

            entity.HasOne(d => d.FkClient).WithMany(p => p.OrderCommissionFkClients)
                .HasForeignKey(d => d.FkClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderCommission_Entity");

            entity.HasOne(d => d.FkCommissionType).WithMany(p => p.OrderCommissions)
                .HasForeignKey(d => d.FkCommissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderCommission_CommissionType");

            entity.HasOne(d => d.FkOrder).WithMany(p => p.OrderCommissions)
                .HasForeignKey(d => d.FkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderCommission_Order");
        });

        modelBuilder.Entity<OrderCommissionStatementRelation>(entity =>
        {
            entity.HasKey(e => e.PkOrderCommissionStatementId);

            entity.ToTable("OrderCommissionStatementRelation");

            entity.Property(e => e.PkOrderCommissionStatementId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<OrderFile>(entity =>
        {
            entity.HasKey(e => e.PkOrderFileId);

            entity.ToTable("OrderFile");

            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.FileName).IsUnicode(false);
            entity.Property(e => e.FilePath).IsUnicode(false);
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())", "DF_OrderFile_LastUpdated")
                .HasColumnType("datetime");
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("(getdate())", "DF_OrderFile_UploadDate")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FkLastUpdatedUser).WithMany(p => p.OrderFileFkLastUpdatedUsers)
                .HasForeignKey(d => d.FkLastUpdatedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderFile_User1");

            entity.HasOne(d => d.FkOrder).WithMany(p => p.OrderFiles)
                .HasForeignKey(d => d.FkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderFile_Order");

            entity.HasOne(d => d.FkUploadUser).WithMany(p => p.OrderFileFkUploadUsers)
                .HasForeignKey(d => d.FkUploadUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderFile_User");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.PkOrderHistoryId).HasFillFactor(90);

            entity.ToTable("OrderHistory");

            entity.HasIndex(e => e.FkOrderId, "IX_OrderHistory_FkOrderId");

            entity.Property(e => e.PkOrderHistoryId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderHistoryID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FkHistoryCategoryId).HasColumnName("FkHistoryCategoryID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Note).HasMaxLength(500);

            entity.HasOne(d => d.FkHistoryCategory).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.FkHistoryCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderHist__FkHis__46A85E41");

            entity.HasOne(d => d.FkOrder).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.FkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderHistory_Order");

            entity.HasOne(d => d.FkOrderStatus).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.FkOrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderHistory_OrderStatus");

            entity.HasOne(d => d.FkUser).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.FkUserId)
                .HasConstraintName("FK_OrderHistory_User");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.PkOrderItemId).HasFillFactor(90);

            entity.ToTable("OrderItem");

            entity.HasIndex(e => new { e.FkProductZoneId, e.Active }, "Index_OrderItems_01");

            entity.HasIndex(e => e.Active, "Index_OrderItems_02");

            entity.HasIndex(e => e.FkOrderId, "NCI_OrderItem_FkProductZoneID");

            entity.Property(e => e.PkOrderItemId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderItemID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.AddedToBasketFromPlan).HasDefaultValue(false);
            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Discount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkLinkedOrderItemId).HasColumnName("FkLinkedOrderItemID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOutwardBopcategoryId).HasColumnName("FkOutwardBOPCategoryId");
            entity.Property(e => e.FkProductZoneId).HasColumnName("FkProductZoneID");
            entity.Property(e => e.FkStatusChangeUserId).HasColumnName("FkStatusChangeUserID");
            entity.Property(e => e.MaxOrder).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.MinOrder).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PodreferenceNumber).HasColumnName("PODReferenceNumber");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Servicetype).HasMaxLength(2);
            entity.Property(e => e.StatusChangeDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkOrder).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.FkOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");

            entity.HasOne(d => d.FkOrderStatus).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.FkOrderStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_OrderStatus");

            entity.HasOne(d => d.FkOutwardBopcategory).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.FkOutwardBopcategoryId)
                .HasConstraintName("FK__OrderItem__FkOutwardBOPCategoryId");

            entity.HasOne(d => d.FkProductZone).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.FkProductZoneId)
                .HasConstraintName("FK_OrderItem_ProductZone");

            entity.HasOne(d => d.FkStatusChangeUser).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.FkStatusChangeUserId)
                .HasConstraintName("FK_OrderItem_User");
        });

        modelBuilder.Entity<OrderItemApproval>(entity =>
        {
            entity.HasKey(e => e.PkOrderItemApprovalId).HasFillFactor(90);

            entity.ToTable("OrderItemApproval");

            entity.Property(e => e.PkOrderItemApprovalId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderItemApprovalID");
            entity.Property(e => e.AdditionalInfo)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FkApprovalStatusTypeId).HasColumnName("FkApprovalStatusTypeID");
            entity.Property(e => e.FkOrderItemId).HasColumnName("FkOrderItemID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

            entity.HasOne(d => d.FkApprovalStatusType).WithMany(p => p.OrderItemApprovals)
                .HasForeignKey(d => d.FkApprovalStatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItemApproval_ApprovalStatusType");

            entity.HasOne(d => d.FkOrderItem).WithMany(p => p.OrderItemApprovals)
                .HasForeignKey(d => d.FkOrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItemApproval_OrderItem");

            entity.HasOne(d => d.FkUser).WithMany(p => p.OrderItemApprovals)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItemApproval_User");
        });

        modelBuilder.Entity<OrderItemCommission>(entity =>
        {
            entity.HasKey(e => e.PkOrderItemCommissionId).HasFillFactor(90);

            entity.ToTable("OrderItemCommission");

            entity.Property(e => e.PkOrderItemCommissionId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderItemCommissionID");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.FkCommissionTypeId).HasColumnName("FkCommissionTypeID");
            entity.Property(e => e.FkOrderItemId).HasColumnName("FkOrderItemID");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.VatRegistered).HasDefaultValue(false);

            entity.HasOne(d => d.FkAgent).WithMany(p => p.OrderItemCommissions)
                .HasForeignKey(d => d.FkAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItemCommission_Supplier");

            entity.HasOne(d => d.FkCommissionType).WithMany(p => p.OrderItemCommissions)
                .HasForeignKey(d => d.FkCommissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItemCommission_CommissionType");

            entity.HasOne(d => d.FkOrderItem).WithMany(p => p.OrderItemCommissions)
                .HasForeignKey(d => d.FkOrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItemCommission_OrderItem");
        });

        modelBuilder.Entity<OrderItemReference>(entity =>
        {
            entity.HasKey(e => e.PkOrderItemReference)
                .HasName("PkAccountID")
                .HasFillFactor(90);

            entity.ToTable("OrderItemReference");

            entity.Property(e => e.PkOrderItemReference).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())", "DF_OrderItemReference_CreateDate")
                .HasColumnType("datetime");
            entity.Property(e => e.Reference).HasMaxLength(255);

            entity.HasOne(d => d.FkOrderItemNavigation).WithMany(p => p.OrderItemReferences)
                .HasForeignKey(d => d.FkOrderItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItemReference_OrderItem");
        });

        modelBuilder.Entity<OrderItemStatus>(entity =>
        {
            entity.HasKey(e => e.PkOrderItemStatusId)
                .HasName("PK_OrderStatus")
                .HasFillFactor(90);

            entity.ToTable("OrderItemStatus");

            entity.Property(e => e.PkOrderItemStatusId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderItemStatusID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OrderStatementRelation>(entity =>
        {
            entity.HasKey(e => e.PkOrderStatementId);

            entity.ToTable("OrderStatementRelation");

            entity.Property(e => e.PkOrderStatementId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.PkOrderStatusId)
                .HasName("PK_OrderStatus_1")
                .HasFillFactor(90);

            entity.ToTable("OrderStatus");

            entity.Property(e => e.PkOrderStatusId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderStatusID");
            entity.Property(e => e.ApprovalStatus).HasDefaultValue(false);
            entity.Property(e => e.IOrder).HasColumnName("iOrder");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderStatusId)
                .ValueGeneratedOnAdd()
                .HasColumnName("OrderStatusID");
            entity.Property(e => e.SysStatus).HasDefaultValue(true);
        });

        modelBuilder.Entity<OrderSweepFrequency>(entity =>
        {
            entity.HasKey(e => e.PkOrderSweepFrequencyId);

            entity.ToTable("OrderSweepFrequency");

            entity.Property(e => e.PkOrderSweepFrequencyId).HasColumnName("PkOrderSweepFrequencyID");
            entity.Property(e => e.Active).HasDefaultValue(true);
        });

        modelBuilder.Entity<OrderType>(entity =>
        {
            entity.HasKey(e => e.PkOrderTypeId).HasFillFactor(90);

            entity.ToTable("OrderType");

            entity.Property(e => e.PkOrderTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkOrderTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OutwardBopcategory>(entity =>
        {
            entity.HasKey(e => e.PkoutwardBopcategoryId).HasFillFactor(90);

            entity.ToTable("OutwardBOPCategory");

            entity.Property(e => e.PkoutwardBopcategoryId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PKOutwardBOPCategoryId");
            entity.Property(e => e.Code).IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OutwardBopcategoryCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OutwardBOPCat__FKCreatedUser");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OutwardBopcategoryUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OutwardBOPCat__FKUpdatedUser");
        });

        modelBuilder.Entity<PaymentRequest>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__PaymentR__3214EC27385954FF")
                .HasFillFactor(90);

            entity.ToTable("PaymentRequest");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ID");
            entity.Property(e => e.Amount).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.Completed).HasDefaultValue(false);
            entity.Property(e => e.DateOfRequest)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IntegratedSystemId).HasColumnName("IntegratedSystemID");
            entity.Property(e => e.Msisdn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MSISDN");
            entity.Property(e => e.OriginUserId).HasColumnName("OriginUserID");
            entity.Property(e => e.Reason)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasDefaultValue("Unknown");
            entity.Property(e => e.Reference)
                .HasMaxLength(29)
                .IsUnicode(false);

            entity.HasOne(d => d.IntegratedSystem).WithMany(p => p.PaymentRequests)
                .HasForeignKey(d => d.IntegratedSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentRe__Integ__74A4331B");

            entity.HasOne(d => d.OriginUser).WithMany(p => p.PaymentRequests)
                .HasForeignKey(d => d.OriginUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaymentRe__Origi__75985754");
        });

        modelBuilder.Entity<Plan>(entity =>
        {
            entity.HasKey(e => e.PkPlanId);

            entity.ToTable("Plan");

            entity.Property(e => e.PkPlanId).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.FkPlanStatusId).HasDefaultValue(new Guid("838782ce-992c-40f3-98b1-cc49ed79bf33"));
            entity.Property(e => e.LastRequisitionDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PlanStatus>(entity =>
        {
            entity.HasKey(e => e.PkPlanStatusId);

            entity.ToTable("PlanStatus");

            entity.Property(e => e.PkPlanStatusId).HasDefaultValueSql("(newid())", "DF_PlanStatus_PkPlanStatusId");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.PkProductId).HasFillFactor(90);

            entity.ToTable("Product");

            entity.HasIndex(e => new { e.Active, e.Code, e.ShortName }, "IX_Product_Search_Code_Name").HasFilter("([Active]=(1) AND [Deleted]=(0) AND [FkCategoryID]<>'9F9437DF-9616-4B76-A17C-D3BE2D6413F8' AND [FkCategoryID]<>'05565F05-C97E-4224-908B-5B81040F2074' AND [FkCategoryID]<>'ED1DBAAA-EA62-4B26-9280-50078F302DAD')");

            entity.Property(e => e.PkProductId)
                .ValueGeneratedNever()
                .HasColumnName("PkProductID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.AlwaysCustomPrice).HasDefaultValue(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ContributionInclVat).HasDefaultValue(false);
            entity.Property(e => e.CustomPriceLess).HasDefaultValue(false);
            entity.Property(e => e.CustomPriceMore).HasDefaultValue(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkBaseProductId).HasColumnName("FkBaseProductID");
            entity.Property(e => e.FkCategoryId).HasColumnName("FkCategoryID");
            entity.Property(e => e.FkUnitId).HasColumnName("FkUnitID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MarkupInclVat).HasDefaultValue(false);
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.MaxOrder).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.MinOrder).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Size).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Specification)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Vatincl)
                .HasDefaultValue(true)
                .HasColumnName("VATIncl");
            entity.Property(e => e.Vatrule)
                .HasDefaultValue(false)
                .HasColumnName("VATRule");

            entity.HasOne(d => d.FkBaseProduct).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkBaseProductId)
                .HasConstraintName("FK_Product_BaseProduct");

            entity.HasOne(d => d.FkCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkCategoryId)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.FkUnit).WithMany(p => p.Products)
                .HasForeignKey(d => d.FkUnitId)
                .HasConstraintName("FK_Product_Unit");
        });

        modelBuilder.Entity<ProductAllocationOverridge>(entity =>
        {
            entity.HasKey(e => e.PkAllocationOverId)
                .HasName("PK_ProductAllocationOverridge_1")
                .HasFillFactor(90);

            entity.ToTable("ProductAllocationOverridge");

            entity.Property(e => e.PkAllocationOverId)
                .ValueGeneratedNever()
                .HasColumnName("PkAllocationOverID");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkProductZoneId).HasColumnName("FkProductZoneID");
            entity.Property(e => e.OverridgeQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OverridgeTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductAllocationQty)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("ProductAllocationQTY");
            entity.Property(e => e.ProductAllocationTerms)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkProductZone).WithMany(p => p.ProductAllocationOverridges)
                .HasForeignKey(d => d.FkProductZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductAl__FkPro__01A9F3D5");
        });

        modelBuilder.Entity<ProductCommission>(entity =>
        {
            entity.HasKey(e => e.PkProductCommissionId).HasFillFactor(90);

            entity.ToTable("ProductCommission");

            entity.Property(e => e.PkProductCommissionId)
                .ValueGeneratedNever()
                .HasColumnName("PkProductCommissionID");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.FkCommissionTypeId).HasColumnName("FkCommissionTypeID");
            entity.Property(e => e.FkProductSupplierId).HasColumnName("FkProductSupplierID");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.FkAgent).WithMany(p => p.ProductCommissions)
                .HasForeignKey(d => d.FkAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductCommission_Supplier");

            entity.HasOne(d => d.FkCommissionType).WithMany(p => p.ProductCommissions)
                .HasForeignKey(d => d.FkCommissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductCommission_CommissionType");

            entity.HasOne(d => d.FkProductSupplier).WithMany(p => p.ProductCommissions)
                .HasForeignKey(d => d.FkProductSupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductCo__FkPro__5031C87B");
        });

        modelBuilder.Entity<ProductExcluded>(entity =>
        {
            entity.HasKey(e => e.PkProductExludedId)
                .HasName("PK_ProductExluded")
                .HasFillFactor(90);

            entity.ToTable("ProductExcluded");

            entity.Property(e => e.PkProductExludedId)
                .ValueGeneratedNever()
                .HasColumnName("PkProductExludedID");
            entity.Property(e => e.FkProductId).HasColumnName("FkProductID");
            entity.Property(e => e.FkSupplierClientId).HasColumnName("FkSupplierClientID");

            entity.HasOne(d => d.FkProduct).WithMany(p => p.ProductExcludeds)
                .HasForeignKey(d => d.FkProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductExluded_Product");

            entity.HasOne(d => d.FkSupplierClient).WithMany(p => p.ProductExcludeds)
                .HasForeignKey(d => d.FkSupplierClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductExluded_SupplierClient");
        });

        modelBuilder.Entity<ProductStatus>(entity =>
        {
            entity.HasKey(e => e.PkStatusId).HasFillFactor(90);

            entity.ToTable("ProductStatus");

            entity.Property(e => e.PkStatusId)
                .ValueGeneratedNever()
                .HasColumnName("PkStatusID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductSupplier>(entity =>
        {
            entity.HasKey(e => e.PkProductSupplierId)
                .HasName("PK__ProductS__9DDF46D6D13151C5")
                .HasFillFactor(90);

            entity.ToTable("ProductSupplier");

            entity.HasIndex(e => new { e.Active, e.Deleted, e.FkSupplierId, e.FkProductId }, "IX_Product_Search_ProductSupplier_Active_Join").HasFilter("([Active]=(1) AND [Deleted]=(0))");

            entity.HasIndex(e => new { e.FkSupplierId, e.FkProductId, e.Active, e.Deleted }, "IX_Product_Search_ProductSupplier_Lookup");

            entity.HasIndex(e => e.Active, "Index_ProductSupplier_01");

            entity.HasIndex(e => new { e.FkSupplierId, e.Active }, "Index_ProductSupplier_02");

            entity.Property(e => e.PkProductSupplierId)
                .ValueGeneratedNever()
                .HasColumnName("PkProductSupplierID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkProductId).HasColumnName("FkProductID");
            entity.Property(e => e.FkProductStatusId)
                .HasDefaultValue(new Guid("31b58119-dd66-4899-a3b1-05aae7ede0f5"))
                .HasColumnName("FkProductStatusID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.FkProduct).WithMany(p => p.ProductSuppliers)
                .HasForeignKey(d => d.FkProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSu__FkPro__1AC9DC03");

            entity.HasOne(d => d.FkSupplier).WithMany(p => p.ProductSuppliers)
                .HasForeignKey(d => d.FkSupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSu__FkSup__1BBE003C");
        });

        modelBuilder.Entity<ProductZone>(entity =>
        {
            entity.HasKey(e => e.PkProductZoneId).HasFillFactor(90);

            entity.ToTable("ProductZone");

            entity.HasIndex(e => e.Active, "Index_ProductZone_01");

            entity.HasIndex(e => new { e.Active, e.FkProductSupplierId }, "Index_ProductZone_02");

            entity.Property(e => e.PkProductZoneId)
                .ValueGeneratedNever()
                .HasColumnName("PkProductZoneID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Differential).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Discount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkDifferentialTypeId)
                .HasDefaultValue(new Guid("706337c2-3474-401f-8939-7f2579cb0e2c"), "DF_ProductZone_FkDifferentialTypeID")
                .HasColumnName("FkDifferentialTypeID");
            entity.Property(e => e.FkDiscountTypeId).HasColumnName("FkDiscountTypeID");
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");
            entity.Property(e => e.FkProductSupplierId).HasColumnName("FkProductSupplierID");
            entity.Property(e => e.FkSupplierZoneId).HasColumnName("FkSupplierZoneID");
            entity.Property(e => e.Levy).HasDefaultValue(false);
            entity.Property(e => e.MaxPurchaseQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.MinPurchaseQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OverwritePrice).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ParentCommission).HasDefaultValue(true, "DF_ProductZone_ParentCommission");

            entity.HasOne(d => d.FkDifferentialType).WithMany(p => p.ProductZones)
                .HasForeignKey(d => d.FkDifferentialTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductZone_DifferentialType");

            entity.HasOne(d => d.FkDiscountType).WithMany(p => p.ProductZones)
                .HasForeignKey(d => d.FkDiscountTypeId)
                .HasConstraintName("FK_ProductZone_DiscountType");

            entity.HasOne(d => d.FkProductSupplier).WithMany(p => p.ProductZones)
                .HasForeignKey(d => d.FkProductSupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductZo__FkPro__1CB22475");

            entity.HasOne(d => d.FkSupplierZone).WithMany(p => p.ProductZones)
                .HasForeignKey(d => d.FkSupplierZoneId)
                .HasConstraintName("FK_ProductZone_SupplierZone");
        });

        modelBuilder.Entity<ProductZoneCommission>(entity =>
        {
            entity.HasKey(e => e.PkProductZoneCommissionId)
                .HasName("PK_Commission")
                .HasFillFactor(90);

            entity.ToTable("ProductZoneCommission");

            entity.Property(e => e.PkProductZoneCommissionId)
                .ValueGeneratedNever()
                .HasColumnName("PkProductZoneCommissionID");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.FkCommissionTypeId).HasColumnName("FkCommissionTypeID");
            entity.Property(e => e.FkProductZoneId).HasColumnName("FkProductZoneID");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.FkAgent).WithMany(p => p.ProductZoneCommissions)
                .HasForeignKey(d => d.FkAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commission_Agent");

            entity.HasOne(d => d.FkAgentNavigation).WithMany(p => p.ProductZoneCommissions)
                .HasForeignKey(d => d.FkAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductZoneCommission_Supplier");

            entity.HasOne(d => d.FkCommissionType).WithMany(p => p.ProductZoneCommissions)
                .HasForeignKey(d => d.FkCommissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commission_CommissionType");

            entity.HasOne(d => d.FkProductZone).WithMany(p => p.ProductZoneCommissions)
                .HasForeignKey(d => d.FkProductZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Commission_ProductZone");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.PkProvinceId).HasFillFactor(90);

            entity.ToTable("Province");

            entity.Property(e => e.PkProvinceId)
                .ValueGeneratedNever()
                .HasColumnName("PkProvinceID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkCountryId).HasColumnName("FkCountryID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RewardsUploadFile>(entity =>
        {
            entity.HasKey(e => e.PkrewardsUploadFileId).HasName("PK__RewardsU__B7A5502486CE7F09");

            entity.ToTable("RewardsUploadFile");

            entity.Property(e => e.PkrewardsUploadFileId).HasColumnName("PKRewardsUploadFileId");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FileName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FilePath).IsUnicode(false);
            entity.Property(e => e.FkentityId).HasColumnName("FKEntityId");
            entity.Property(e => e.FkuploadedUserId).HasColumnName("FKUploadedUserId");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");

            entity.HasOne(d => d.Fkentity).WithMany(p => p.RewardsUploadFiles)
                .HasForeignKey(d => d.FkentityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RewardsUp__FKEnt__60DDFCE7");

            entity.HasOne(d => d.FkuploadedUser).WithMany(p => p.RewardsUploadFiles)
                .HasForeignKey(d => d.FkuploadedUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RewardsUp__FKUpl__61D22120");
        });

        modelBuilder.Entity<RuleType>(entity =>
        {
            entity.HasKey(e => e.PkRuleTypeId).HasFillFactor(90);

            entity.Property(e => e.PkRuleTypeId)
                .HasDefaultValueSql("(newid())", "DF_RuleTypes_PkRuleTypeID")
                .HasColumnName("PkRuleTypeID");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Share>(entity =>
        {
            entity.HasKey(e => e.PkShareId);

            entity.Property(e => e.PkShareId).ValueGeneratedNever();
            entity.Property(e => e.Ceded).HasDefaultValue(false);
            entity.Property(e => e.CededDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Signal>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Message)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatementRelation>(entity =>
        {
            entity.HasKey(e => e.PkStatementRelationId).HasName("PK__Statemen__050DED090D0D57DB");

            entity.ToTable("StatementRelation");

            entity.Property(e => e.PkStatementRelationId).HasColumnName("PkStatementRelationID");
            entity.Property(e => e.DateLinked).HasColumnType("datetime");
            entity.Property(e => e.FkEntityBankStatementId).HasColumnName("FkEntityBankStatementID");
            entity.Property(e => e.FkParentEntityBankStatementId).HasColumnName("FkParentEntityBankStatementID");
            entity.Property(e => e.FkStatementRelationTypeId).HasColumnName("FkStatementRelationTypeID");

            entity.HasOne(d => d.FkEntityBankStatement).WithMany(p => p.StatementRelationFkEntityBankStatements)
                .HasForeignKey(d => d.FkEntityBankStatementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Statement__FkEnt__48D09DD5");

            entity.HasOne(d => d.FkParentEntityBankStatement).WithMany(p => p.StatementRelationFkParentEntityBankStatements)
                .HasForeignKey(d => d.FkParentEntityBankStatementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Statement__FkPar__47DC799C");

            entity.HasOne(d => d.FkStatementRelationType).WithMany(p => p.StatementRelations)
                .HasForeignKey(d => d.FkStatementRelationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Statement__FkSta__49C4C20E");
        });

        modelBuilder.Entity<StatementRelationType>(entity =>
        {
            entity.HasKey(e => e.PkStatementRelationTypeId).HasName("PK__Statemen__2DFC61463F9B6854");

            entity.ToTable("StatementRelationType");

            entity.Property(e => e.PkStatementRelationTypeId).HasColumnName("PkStatementRelationTypeID");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.PkSubscriptionId).HasFillFactor(90);

            entity.Property(e => e.PkSubscriptionId).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Destination)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SupplierClient>(entity =>
        {
            entity.HasKey(e => e.PkSupplierClientId).HasFillFactor(90);

            entity.ToTable("SupplierClient");

            entity.HasIndex(e => e.FkEntityTypeId, "NCIDX_SupplierClient_PkSupplierClientID_FkSupplierID_FkClient_SupplierClientRef_Others");

            entity.HasIndex(e => new { e.FkSupplierId, e.FkClientId }, "ncidx_SupplierClient_SupplierID_ClientID_AccountID");

            entity.Property(e => e.PkSupplierClientId)
                .ValueGeneratedNever()
                .HasColumnName("PkSupplierClientID");
            entity.Property(e => e.AllocatedFacility).HasDefaultValue(false);
            entity.Property(e => e.AllocationDateSet).HasColumnType("datetime");
            entity.Property(e => e.AutomaticAllocation).HasDefaultValue(false);
            entity.Property(e => e.Contribution).HasDefaultValue(false);
            entity.Property(e => e.CreditLimit).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Drawdown).HasDefaultValue(false);
            entity.Property(e => e.ExcludeAllGoods).HasDefaultValue(false);
            entity.Property(e => e.FkClientBankInfoId).HasColumnName("FkClientBankInfoID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkSupplierBankInfoId).HasColumnName("FkSupplierBankInfoID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.FkSweepId).HasColumnName("FkSweepID");
            entity.Property(e => e.ForwardGuarantee).HasDefaultValue(false);
            entity.Property(e => e.ForwardPrice).HasDefaultValue(false);
            entity.Property(e => e.ForwardRequisition).HasDefaultValue(false);
            entity.Property(e => e.IncludeCategories).HasDefaultValue(false);
            entity.Property(e => e.Markup).HasDefaultValue(false);
            entity.Property(e => e.Ppamount)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("PPAmount");
            entity.Property(e => e.Pporder).HasColumnName("PPOrder");
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierClientRefrence)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkClient).WithMany(p => p.SupplierClientFkClients)
                .HasForeignKey(d => d.FkClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierClient_Entity");

            entity.HasOne(d => d.FkEntityType).WithMany(p => p.SupplierClients)
                .HasForeignKey(d => d.FkEntityTypeId)
                .HasConstraintName("FK_SupplierClient_EntityType");

            entity.HasOne(d => d.FkSupplier).WithMany(p => p.SupplierClientFkSuppliers)
                .HasForeignKey(d => d.FkSupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierClient_Supplier");
        });

        modelBuilder.Entity<SupplierClientAccount>(entity =>
        {
            entity.HasKey(e => e.PkSupplierClientAccountId).HasFillFactor(90);

            entity.ToTable("SupplierClientAccount");

            entity.Property(e => e.PkSupplierClientAccountId)
                .ValueGeneratedNever()
                .HasColumnName("PkSupplierClientAccountID");
            entity.Property(e => e.FkClientAccountId).HasColumnName("FkClientAccountID");
            entity.Property(e => e.FkSupplierAccountId).HasColumnName("FkSupplierAccountID");
            entity.Property(e => e.FkSupplierClientId).HasColumnName("FkSupplierClientID");

            entity.HasOne(d => d.FkClientAccount).WithMany(p => p.SupplierClientAccountFkClientAccounts)
                .HasForeignKey(d => d.FkClientAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierClientAccount_Account");

            entity.HasOne(d => d.FkSupplierAccount).WithMany(p => p.SupplierClientAccountFkSupplierAccounts)
                .HasForeignKey(d => d.FkSupplierAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierClientAccount_Account1");

            entity.HasOne(d => d.FkSupplierClient).WithMany(p => p.SupplierClientAccounts)
                .HasForeignKey(d => d.FkSupplierClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierClientAccount_SupplierClient");
        });

        modelBuilder.Entity<SupplierCommission>(entity =>
        {
            entity.HasKey(e => e.PkSupplierCommissionId).HasFillFactor(90);

            entity.ToTable("SupplierCommission");

            entity.Property(e => e.PkSupplierCommissionId)
                .ValueGeneratedNever()
                .HasColumnName("PkSupplierCommissionID");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkCommissionTypeId).HasColumnName("FkCommissionTypeID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Value).HasColumnType("decimal(19, 4)");

            entity.HasOne(d => d.FkAgent).WithMany(p => p.SupplierCommissionFkAgents)
                .HasForeignKey(d => d.FkAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierCommission_Agent");

            entity.HasOne(d => d.FkClient).WithMany(p => p.SupplierCommissionFkClients)
                .HasForeignKey(d => d.FkClientId)
                .HasConstraintName("FK_SupplierCommission_Entity");

            entity.HasOne(d => d.FkCommissionType).WithMany(p => p.SupplierCommissions)
                .HasForeignKey(d => d.FkCommissionTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierCommission_CommissionType");

            entity.HasOne(d => d.FkSupplier).WithMany(p => p.SupplierCommissionFkSuppliers)
                .HasForeignKey(d => d.FkSupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierCommission_Supplier");
        });

        modelBuilder.Entity<SupplierZone>(entity =>
        {
            entity.HasKey(e => e.PkSupplierZoneId).HasFillFactor(90);

            entity.ToTable("SupplierZone");

            entity.Property(e => e.PkSupplierZoneId)
                .ValueGeneratedNever()
                .HasColumnName("PkSupplierZoneID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.FkSupplier).WithMany(p => p.SupplierZones)
                .HasForeignKey(d => d.FkSupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SupplierZone_Entity");
        });

        modelBuilder.Entity<Sweep>(entity =>
        {
            entity.HasKey(e => e.PkSweepId)
                .HasName("PK__Sweep__7684BCC67C520CA9")
                .HasFillFactor(90);

            entity.ToTable("Sweep");

            entity.Property(e => e.PkSweepId)
                .HasDefaultValueSql("(newid())", "DF__Sweep__PkSweepID__6EEB59C5")
                .HasColumnName("PkSweepID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.ExpiryAmount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ExpiryAmountResetDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.FkFrequencyId).HasColumnName("FkFrequencyID");
            entity.Property(e => e.FksupplierClientId).HasColumnName("FKSupplierClientID");
            entity.Property(e => e.LastProcessed).HasColumnType("datetime");
            entity.Property(e => e.MinimumRequiredBalance).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PaymentType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasDefaultValue("P", "DF__Sweep__PaymentTy__70D3A237");
            entity.Property(e => e.PaymentValue)
                .HasDefaultValue(100m, "DF__Sweep__PaymentVa__6FDF7DFE")
                .HasColumnType("decimal(16, 2)");
            entity.Property(e => e.ProcTime)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RequiredRemainingBalance).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Sequence).HasDefaultValue(1, "DF__Sweep__Sequence__71C7C670");
            entity.Property(e => e.SuccessfullProcessed).HasColumnType("datetime");
            entity.Property(e => e.SweepClientReference)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TotalSweepAmount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.UseExpiryAmount).HasDefaultValue(false);

            entity.HasOne(d => d.FkFrequency).WithMany(p => p.Sweeps)
                .HasForeignKey(d => d.FkFrequencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Sweep__FkFrequen__768C7B8D");
        });

        modelBuilder.Entity<SweepChangeLog>(entity =>
        {
            entity.HasKey(e => e.PkSweepChangeLogId).HasName("PK__SweepCha__ECF5E3022BB79059");

            entity.ToTable("SweepChangeLog");

            entity.Property(e => e.PkSweepChangeLogId)
                .ValueGeneratedNever()
                .HasColumnName("PkSweepChangeLogID");
            entity.Property(e => e.FkSweepId).HasColumnName("FkSweepID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogDescription).IsUnicode(false);
        });

        modelBuilder.Entity<SweepLog>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__SweepLog__3214EC273FD3B929")
                .HasFillFactor(90);

            entity.ToTable("SweepLog");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DateSwept)
                .HasDefaultValueSql("(getdate())", "DF__SweepLog__DateSw__72BBEAA9")
                .HasColumnType("datetime");
            entity.Property(e => e.EntityBankStatementId).HasColumnName("EntityBankStatementID");
            entity.Property(e => e.MainTransaction).HasDefaultValue(true);
            entity.Property(e => e.SweepAmount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.SweepId).HasColumnName("SweepID");

            entity.HasOne(d => d.EntityBankStatement).WithMany(p => p.SweepLogs)
                .HasForeignKey(d => d.EntityBankStatementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SweepLog__Entity__77809FC6");

            entity.HasOne(d => d.Sweep).WithMany(p => p.SweepLogs)
                .HasForeignKey(d => d.SweepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SweepLog__SweepI__7874C3FF");
        });

        modelBuilder.Entity<TermsAndConditionsAudit>(entity =>
        {
            entity.HasKey(e => e.PkTermsId);

            entity.ToTable("TermsAndConditionsAudit");

            entity.Property(e => e.PkTermsId).ValueGeneratedNever();
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DateAccepted).HasColumnType("datetime");
            entity.Property(e => e.DateReset).HasColumnType("datetime");
            entity.Property(e => e.GpayTermsAccepted).HasColumnName("GPayTermsAccepted");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.HasKey(e => e.PkTitleId).HasFillFactor(90);

            entity.ToTable("Title");

            entity.Property(e => e.PkTitleId)
                .ValueGeneratedNever()
                .HasColumnName("PkTitleID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Town>(entity =>
        {
            entity.HasKey(e => e.PkTownId).HasFillFactor(90);

            entity.ToTable("Town");

            entity.Property(e => e.PkTownId)
                .ValueGeneratedNever()
                .HasColumnName("PkTownID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkProvinceId).HasColumnName("FkProvinceID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.FkProvince).WithMany(p => p.Towns)
                .HasForeignKey(d => d.FkProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Town_Province");
        });

        modelBuilder.Entity<TownSupplierZone>(entity =>
        {
            entity.HasKey(e => e.PkTownSupplierZoneId).HasFillFactor(90);

            entity.ToTable("TownSupplierZone");

            entity.Property(e => e.PkTownSupplierZoneId)
                .ValueGeneratedNever()
                .HasColumnName("PkTownSupplierZoneID");
            entity.Property(e => e.FkSupplierZoneId).HasColumnName("FkSupplierZoneID");
            entity.Property(e => e.FkTownId).HasColumnName("FkTownID");

            entity.HasOne(d => d.FkSupplierZone).WithMany(p => p.TownSupplierZones)
                .HasForeignKey(d => d.FkSupplierZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TownSupplierZone_SupplierZone");

            entity.HasOne(d => d.FkTown).WithMany(p => p.TownSupplierZones)
                .HasForeignKey(d => d.FkTownId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TownSupplierZone_Town");
        });

        modelBuilder.Entity<TransactionCode>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Transact__3214EC27204728BD")
                .HasFillFactor(90);

            entity.ToTable("TransactionCode");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.AppliesCharges).HasDefaultValue(true);
            entity.Property(e => e.ApplyOnDebit).HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransactionCodeMap>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Transact__3214EC271FAF1869")
                .HasFillFactor(90);

            entity.ToTable("TransactionCodeMap");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Term).HasMaxLength(200);
            entity.Property(e => e.TransactionCodeId).HasDefaultValue(1);

            entity.HasOne(d => d.Product).WithMany(p => p.TransactionCodeMaps)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Transacti__Produ__2295E654");

            entity.HasOne(d => d.TransactionCode).WithMany(p => p.TransactionCodeMaps)
                .HasForeignKey(d => d.TransactionCodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__Trans__06C2E356");
        });

        modelBuilder.Entity<TransactionMappingRule>(entity =>
        {
            entity.HasKey(e => e.PkRuleId).HasFillFactor(90);

            entity.Property(e => e.PkRuleId)
                .HasDefaultValueSql("(newid())", "DF_TransactionMappingRules_PkRuleID")
                .HasColumnName("PkRuleID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkClassCatagoryId).HasColumnName("FkClassCatagoryID");
            entity.Property(e => e.FkEntityBankStatementId).HasColumnName("FkEntityBankStatementID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkRuleTypeId).HasColumnName("FkRuleTypeID");
            entity.Property(e => e.MappedDate).HasColumnType("datetime");
            entity.Property(e => e.RuleText)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDescription)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TransactionMappingRuleLink>(entity =>
        {
            entity.HasKey(e => e.PkRuleLink).HasFillFactor(90);

            entity.Property(e => e.PkRuleLink).HasDefaultValueSql("(newid())", "DF_TransactionMappingRuleLinks_PkRuleID");
            entity.Property(e => e.FkEntityBankStatementId).HasColumnName("FkEntityBankStatementID");
            entity.Property(e => e.FkRuleId).HasColumnName("FkRuleID");
            entity.Property(e => e.MappedDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkEntityBankStatement).WithMany(p => p.TransactionMappingRuleLinks)
                .HasForeignKey(d => d.FkEntityBankStatementId)
                .HasConstraintName("FK_TransactionMappingRuleLinks_EntityBankStatement");

            entity.HasOne(d => d.FkRule).WithMany(p => p.TransactionMappingRuleLinks)
                .HasForeignKey(d => d.FkRuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TransactionMappingRuleLinks_TransactionMappingRules");
        });

        modelBuilder.Entity<TransactionStore>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Transact__3213E83FCCA70625");

            entity.ToTable("TransactionStore");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BankDefinedValue).HasColumnType("money");
            entity.Property(e => e.BeneficiaryValue).HasColumnType("money");
            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.DepositValue).HasColumnType("money");
            entity.Property(e => e.ElectricityValue).HasColumnType("money");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EntityAddress)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.EntityName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Msisdn)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentValue).HasColumnType("money");
            entity.Property(e => e.PrepaidValue).HasColumnType("money");
            entity.Property(e => e.RefundValue).HasColumnType("money");
            entity.Property(e => e.SendMoneyValue).HasColumnType("money");
            entity.Property(e => e.TopupValue).HasColumnType("money");
            entity.Property(e => e.TransferValue).HasColumnType("money");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.WalletWithdrawalValue).HasColumnType("money");
            entity.Property(e => e.WithdrawalValue).HasColumnType("money");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.PkUnitId).HasFillFactor(90);

            entity.ToTable("Unit");

            entity.Property(e => e.PkUnitId)
                .ValueGeneratedNever()
                .HasColumnName("PkUnitID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UploadHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UploadHi__3214EC077827AFE0");

            entity.ToTable("UploadHistory");

            entity.Property(e => e.FileName).IsUnicode(false);
            entity.Property(e => e.OriginalPath).IsUnicode(false);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<UploadReference>(entity =>
        {
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.PkUserId)
                .HasName("PK_User2")
                .HasFillFactor(90);

            entity.ToTable("User");

            entity.Property(e => e.PkUserId)
                .ValueGeneratedNever()
                .HasColumnName("PkUserID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Cellphone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmailNotification).HasDefaultValue(false);
            entity.Property(e => e.Enabled).HasDefaultValue(false);
            entity.Property(e => e.FkDefaultAccountId).HasColumnName("FkDefaultAccountID");
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");
            entity.Property(e => e.GpayTermsAccepted)
                .HasDefaultValue(false)
                .HasColumnName("GPayTermsAccepted");
            entity.Property(e => e.IdentityReset).HasDefaultValue(false);
            entity.Property(e => e.IgnoreDuplicateLoginChecks).HasDefaultValue(false);
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.Locked).HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PasswordExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Reviewed).HasDefaultValue(false);
            entity.Property(e => e.SmsNotification).HasDefaultValue(false);
            entity.Property(e => e.TermsAccepted).HasDefaultValue(false);
            entity.Property(e => e.Uipersistance)
                .HasColumnType("xml")
                .HasColumnName("UIPersistance");

            entity.HasOne(d => d.FkGroup).WithMany(p => p.Users)
                .HasForeignKey(d => d.FkGroupId)
                .HasConstraintName("FK_User_Group");
        });

        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.PkUserEntityId).HasFillFactor(90);

            entity.ToTable("UserEntity");

            entity.Property(e => e.PkUserEntityId)
                .ValueGeneratedNever()
                .HasColumnName("PkUserEntityID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.UserEntities)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserEntity_Entity");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserEntities)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserEntity_User");
        });

        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasKey(e => e.PkUserGroupId)
                .HasName("PK_UserSector")
                .HasFillFactor(90);

            entity.ToTable("UserGroup");

            entity.Property(e => e.PkUserGroupId)
                .ValueGeneratedNever()
                .HasColumnName("PkUserGroupID");
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

            entity.HasOne(d => d.FkGroup).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.FkGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroup_Group");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserGroups)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserGroup_User");
        });

        modelBuilder.Entity<UserNedbankWallet>(entity =>
        {
            entity.HasKey(e => e.PkWalletId).HasFillFactor(90);

            entity.ToTable("UserNedbankWallet");

            entity.Property(e => e.PkWalletId)
                .ValueGeneratedNever()
                .HasColumnName("PkWalletID");
            entity.Property(e => e.CallbackId).HasColumnName("CallbackID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.WalletAccountNumber)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserNedbankWallets)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_UserNedbankWallet");
        });

        modelBuilder.Entity<UserPasswordChangeLog>(entity =>
        {
            entity.HasKey(e => e.PkuserPasswordChangeLogId).HasName("PK__UserPass__44253CA88081714D");

            entity.ToTable("UserPasswordChangeLog");

            entity.Property(e => e.PkuserPasswordChangeLogId)
                .ValueGeneratedNever()
                .HasColumnName("PKUserPasswordChangeLogId");
            entity.Property(e => e.FkuserId).HasColumnName("FKUserId");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Fkuser).WithMany(p => p.UserPasswordChangeLogs)
                .HasForeignKey(d => d.FkuserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPassw__FKUse__7C86175C");
        });

        modelBuilder.Entity<UserPin>(entity =>
        {
            entity.HasKey(e => e.PkUserPin).HasName("PK__UserPin__B717D3DCC61ECDC7");

            entity.ToTable("UserPin");

            entity.Property(e => e.PkUserPin).ValueGeneratedNever();
            entity.Property(e => e.ExpiresAt).HasColumnType("datetime");
            entity.Property(e => e.FkSystemId).HasColumnName("FkSystemID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.IssuedAt).HasColumnType("datetime");
            entity.Property(e => e.Pin)
                .HasMaxLength(512)
                .HasColumnName("PIN");
            entity.Property(e => e.RedeemedAt).HasColumnType("datetime");

            entity.HasOne(d => d.FkSystem).WithMany(p => p.UserPins)
                .HasForeignKey(d => d.FkSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPin__FkSyste__3BEBA403");
        });

        modelBuilder.Entity<UserSecurityAnswer>(entity =>
        {
            entity.HasKey(e => e.PkUserSercurityAnswerId).HasFillFactor(90);

            entity.Property(e => e.PkUserSercurityAnswerId)
                .HasDefaultValueSql("(newid())", "DF_UserSecurityAnswers_PkUserSercurityAnswerID")
                .HasColumnName("PkUserSercurityAnswerID");
            entity.Property(e => e.Answer)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.FkUserSecurityQuestionsId).HasColumnName("FkUserSecurityQuestionsID");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserSecurityAnswers)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserSecurityAnswers_User");
        });

        modelBuilder.Entity<UserSecurityQuestion>(entity =>
        {
            entity.HasKey(e => e.PkUserSecurityQuestionId).HasFillFactor(90);

            entity.Property(e => e.PkUserSecurityQuestionId)
                .HasDefaultValueSql("(newid())", "DF_UserSecurityQuestions_PkUserSecurityQuestionID")
                .HasColumnName("PkUserSecurityQuestionID");
            entity.Property(e => e.SecurityQuestion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserSelection>(entity =>
        {
            entity.HasKey(e => e.PkUserSelectionId).HasName("PK__UserSele__F28DDED190459206");

            entity.ToTable("UserSelection");

            entity.Property(e => e.PkUserSelectionId)
                .ValueGeneratedNever()
                .HasColumnName("PkUserSelectionID");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkAddressId).HasColumnName("FkAddressID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.FkVendorId).HasColumnName("FkVendorID");
            entity.Property(e => e.LastChange).HasColumnType("datetime");

            entity.HasOne(d => d.FkAccount).WithMany(p => p.UserSelections)
                .HasForeignKey(d => d.FkAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSelec__FkAcc__5793BE78");

            entity.HasOne(d => d.FkAddress).WithMany(p => p.UserSelections)
                .HasForeignKey(d => d.FkAddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSelec__FkAdd__597C06EA");

            entity.HasOne(d => d.FkEntity).WithMany(p => p.UserSelectionFkEntities)
                .HasForeignKey(d => d.FkEntityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSelec__FkEnt__569F9A3F");

            entity.HasOne(d => d.FkGroup).WithMany(p => p.UserSelections)
                .HasForeignKey(d => d.FkGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSelec__FkGro__55AB7606");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserSelections)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserSelec__FkUse__54B751CD");

            entity.HasOne(d => d.FkVendor).WithMany(p => p.UserSelectionFkVendors)
                .HasForeignKey(d => d.FkVendorId)
                .HasConstraintName("FK__UserSelec__FkVen__5887E2B1");
        });

        modelBuilder.Entity<UserSupplierCustomPrice>(entity =>
        {
            entity.HasKey(e => e.PkUserSupplierId).HasFillFactor(90);

            entity.ToTable("UserSupplierCustomPrice");

            entity.Property(e => e.PkUserSupplierId)
                .ValueGeneratedNever()
                .HasColumnName("PkUserSupplierID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");

            entity.HasOne(d => d.FkSupplier).WithMany(p => p.UserSupplierCustomPrices)
                .HasForeignKey(d => d.FkSupplierId)
                .HasConstraintName("FK_UserSupplierCustomPrice_Entity");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserSupplierCustomPrices)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserSupplierCustomPrice_User");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.PkUserTypeId)
                .HasName("PK_UserTypes")
                .HasFillFactor(90);

            entity.ToTable("UserType");

            entity.Property(e => e.PkUserTypeId)
                .ValueGeneratedNever()
                .HasColumnName("pkUserTypeID");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserTypeModule>(entity =>
        {
            entity.HasKey(e => e.PkUserTypeModuleId).HasFillFactor(90);

            entity.ToTable("UserTypeModule");

            entity.Property(e => e.PkUserTypeModuleId)
                .ValueGeneratedNever()
                .HasColumnName("pkUserTypeModuleID");
            entity.Property(e => e.FkModuleId).HasColumnName("fkModuleID");
            entity.Property(e => e.FkUserTypeId).HasColumnName("fkUserTypeID");

            entity.HasOne(d => d.FkModule).WithMany(p => p.UserTypeModules)
                .HasForeignKey(d => d.FkModuleId)
                .HasConstraintName("FK_UserTypeModule_Modules");

            entity.HasOne(d => d.FkUserType).WithMany(p => p.UserTypeModules)
                .HasForeignKey(d => d.FkUserTypeId)
                .HasConstraintName("FK_UserTypeModule_UserType");
        });

        modelBuilder.Entity<UserTypeUserType>(entity =>
        {
            entity.HasKey(e => e.PkUserTypeUserTypeId).HasFillFactor(90);

            entity.ToTable("UserTypeUserType");

            entity.Property(e => e.PkUserTypeUserTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkUserTypeUserTypeID");
            entity.Property(e => e.FkUserTypeId).HasColumnName("FkUserTypeID");
            entity.Property(e => e.FkUserTypesId).HasColumnName("FkUserTypesID");

            entity.HasOne(d => d.FkUserType).WithMany(p => p.UserTypeUserTypeFkUserTypes)
                .HasForeignKey(d => d.FkUserTypeId)
                .HasConstraintName("FK_UserTypeUserType_UserType");

            entity.HasOne(d => d.FkUserTypes).WithMany(p => p.UserTypeUserTypeFkUserTypesNavigation)
                .HasForeignKey(d => d.FkUserTypesId)
                .HasConstraintName("FK_UserTypeUserType_UserType1");
        });

        modelBuilder.Entity<UserUserType>(entity =>
        {
            entity.HasKey(e => e.PkUserUserTypeId).HasFillFactor(90);

            entity.ToTable("UserUserType");

            entity.Property(e => e.PkUserUserTypeId)
                .ValueGeneratedNever()
                .HasColumnName("PkUserUserTypeID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.FkUserTypeId).HasColumnName("FkUserTypeID");

            entity.HasOne(d => d.FkUser).WithMany(p => p.UserUserTypes)
                .HasForeignKey(d => d.FkUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserUserType_User");

            entity.HasOne(d => d.FkUserType).WithMany(p => p.UserUserTypes)
                .HasForeignKey(d => d.FkUserTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserUserType_UserType");
        });

        modelBuilder.Entity<VAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAccount");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.AccountStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Balance).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankUserCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode).HasMaxLength(20);
            entity.Property(e => e.BranchName).HasMaxLength(50);
            entity.Property(e => e.ControllingEntity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CountryCurrencyDisplay).HasMaxLength(106);
            entity.Property(e => e.CountryCurrencyId).HasColumnName("CountryCurrencyID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FacilityValue).HasColumnType("money");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.FkControllingEntityId).HasColumnName("FkControllingEntityID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.HasCharges)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.HasFacilityText)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.PkAccountId).HasColumnName("PkAccountID");
            entity.Property(e => e.ResourceType).HasMaxLength(20);
        });

        modelBuilder.Entity<VAccountPending>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAccountPending");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.AccountStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankUserCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode).HasMaxLength(20);
            entity.Property(e => e.BranchName).HasMaxLength(50);
            entity.Property(e => e.ControllingEntity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CountryCurrencyDisplay).HasMaxLength(106);
            entity.Property(e => e.CountryCurrencyId).HasColumnName("CountryCurrencyID");
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountStatusId).HasColumnName("FkAccountStatusID");
            entity.Property(e => e.FkAccountTypeId).HasColumnName("FkAccountTypeID");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.FkControllingEntityId).HasColumnName("FkControllingEntityID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.PkAccountPendingId).HasColumnName("PkAccountPendingID");
            entity.Property(e => e.ResourceType).HasMaxLength(20);
        });

        modelBuilder.Entity<VAgentSupplier>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAgentSuppliers");

            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
        });

        modelBuilder.Entity<VAllCountryBank>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vAllCountryBank");

            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkBankId).HasColumnName("PkBankID");
        });

        modelBuilder.Entity<VBankStatementGbbilist>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vBankStatementGBBIList");

            entity.Property(e => e.Deposits).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
        });

        modelBuilder.Entity<VBankTime>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vBankTimes");

            entity.Property(e => e.Amount).HasColumnType("decimal(16, 4)");
            entity.Property(e => e.Bactive).HasColumnName("BActive");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkBankTimeId).HasColumnName("PkBankTimeID");
        });

        modelBuilder.Entity<VBaseProductPrice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vBaseProductPrice");

            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.PkBaseProductPriceId).HasColumnName("PkBaseProductPriceID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<VBasket>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vBasket");

            entity.Property(e => e.Discount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.PkOrderItemId).HasColumnName("PkOrderItemID");
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VClientApproval>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vClientApproval");

            entity.Property(e => e.Client)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IOrder)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("iOrder");
            entity.Property(e => e.PkClientApprovalId).HasColumnName("PkClientApprovalID");
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserTo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VClientBankStatement>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vClientBankStatement");

            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkAccountId).HasColumnName("PkAccountID");
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.Test)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("test");
            entity.Property(e => e.ToEntityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VClientDataApproval>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vClientDataApproval");

            entity.Property(e => e.Client)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IOrder)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("iOrder");
            entity.Property(e => e.PkEntityApprovalId).HasColumnName("PkEntityApprovalID");
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.RequestedUserId).HasColumnName("RequestedUserID");
            entity.Property(e => e.RequestedUserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VClientSupplier>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vClientSuppliers");

            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
        });

        modelBuilder.Entity<VClientTotal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vClientTotal");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Expr1).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.Expr2).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkFromAccountId).HasColumnName("FkFromAccountID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderTypeId).HasColumnName("FkOrderTypeID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Vatexemption).HasColumnName("VATExemption");
            entity.Property(e => e.Vatrule).HasColumnName("VATRule");
        });

        modelBuilder.Entity<VClusterNotification>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vClusterNotification");

            entity.Property(e => e.Destination)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VCommissionList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vCommissionList");

            entity.Property(e => e.CommissionSum).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
        });

        modelBuilder.Entity<VCountryBank>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vCountryBank");

            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CountryBankId).HasColumnName("CountryBankID");
            entity.Property(e => e.PkBankId).HasColumnName("PkBankID");
        });

        modelBuilder.Entity<VCountryCurrency>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vCountryCurrency");

            entity.Property(e => e.CcIso)
                .HasMaxLength(2)
                .HasColumnName("cc_iso");
            entity.Property(e => e.Code)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("code");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName).HasMaxLength(100);
            entity.Property(e => e.DisplayValue).HasMaxLength(106);
            entity.Property(e => e.Symbol).HasMaxLength(50);
        });

        modelBuilder.Entity<VDevice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vDevices");

            entity.Property(e => e.Cluster)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Consumer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DeviceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeviceStatus)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FkConsumerId).HasColumnName("FkConsumerID");
            entity.Property(e => e.FkDriverId).HasColumnName("FkDriverID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.StatusDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VDeviceLink>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vDeviceLink");

            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DeviceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeviceStatus)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.FkConsumerId).HasColumnName("FkConsumerID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.Make)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusDate).HasColumnType("datetime");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VEntityAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityAccount");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AvailableBalance).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode).HasMaxLength(20);
            entity.Property(e => e.BranchName).HasMaxLength(50);
            entity.Property(e => e.ControllingEntity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Currency).HasMaxLength(106);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EntityOwner)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Facility).HasColumnType("money");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkAccountTypeId).HasColumnName("FkAccountTypeID");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.FkControllingEntityId).HasColumnName("FkControllingEntityID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.FkentityOwnerId).HasColumnName("FKEntityOwnerID");
            entity.Property(e => e.HasCharges)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityAccountId).HasColumnName("PkEntityAccountID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ResourceType).HasMaxLength(20);
            entity.Property(e => e.ServiceType)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionCost).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<VEntityAccountLink>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityAccountLinks");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.ResourceType).HasMaxLength(20);
        });

        modelBuilder.Entity<VEntityAccountOptimized>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityAccountOptimized");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AvailableBalance).HasColumnType("money");
            entity.Property(e => e.Balance).HasColumnType("money");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode).HasMaxLength(20);
            entity.Property(e => e.BranchName).HasMaxLength(50);
            entity.Property(e => e.ControllingEntity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EntityOwner)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Facility).HasColumnType("money");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkAccountTypeId).HasColumnName("FkAccountTypeID");
            entity.Property(e => e.FkBankId).HasColumnName("FkBankID");
            entity.Property(e => e.FkControllingEntityId).HasColumnName("FkControllingEntityID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.FkentityOwnerId).HasColumnName("FKEntityOwnerID");
            entity.Property(e => e.HasCharges)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityAccountId).HasColumnName("PkEntityAccountID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ResourceType).HasMaxLength(20);
            entity.Property(e => e.ServiceType)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.StatusDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionCost).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<VEntityAgentList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityAgentList");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VEntityBankStatement>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityBankStatement");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FormattedAccountDetails).HasMaxLength(183);
            entity.Property(e => e.IsCopy)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.Reconciled)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ResourceType).HasMaxLength(20);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.TranCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VEntityBankStatementDeleted>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityBankStatementDeleted");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FormattedAccountDetails).HasMaxLength(183);
            entity.Property(e => e.IsCopy)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.Reconciled)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ResourceType).HasMaxLength(20);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VEntityBankStatementIaccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityBankStatementIAccount");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.EventNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FormattedAccountDetails).HasMaxLength(183);
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.Reconciled)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.TransactionCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VEntityBankStatementMappedToClass>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityBankStatementMappedToClass");

            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.Mapped)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VEntityBankStatementMapping>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityBankStatementMapping");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateMapped).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkRuleTypeId).HasColumnName("FkRuleTypeID");
            entity.Property(e => e.FormattedAccountDetails).HasMaxLength(183);
            entity.Property(e => e.MappedCat)
                .HasMaxLength(182)
                .IsUnicode(false);
            entity.Property(e => e.MappedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RuleText)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VEntityBankStatementMvU>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityBankStatementMvU");

            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.Mapped)
                .HasMaxLength(9)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VEntityBeneficiaryList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityBeneficiaryList");

            entity.Property(e => e.AccountNo).HasMaxLength(30);
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntityReferenceAlias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VEntityBeneficiaryListDistinct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityBeneficiaryListDistinct");

            entity.Property(e => e.AccountNo).HasMaxLength(30);
            entity.Property(e => e.AllocationQty)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("AllocationQTY");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Currency).HasMaxLength(104);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntityReferenceAlias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EntityTypeIndicator)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Facility).HasColumnType("money");
            entity.Property(e => e.FkClientAccountId).HasColumnName("FkClientAccountID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.MaxPurchaseQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.MinPurchaseQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VEntityClientList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityClientList");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VEntityCompactList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityCompactList");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntityReferenceAlias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkEntitySubTypeId).HasColumnName("FkEntitySubTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.Hash)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.QuickInfo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatexemption).HasColumnName("VATExemption");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VEntityEntitiesList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityEntitiesList");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.CreditLimit).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.EntityType).HasMaxLength(100);
            entity.Property(e => e.FkSweepId).HasColumnName("FkSweepID");
            entity.Property(e => e.FormAcc).HasMaxLength(183);
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.Ppamount)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("PPAmount");
            entity.Property(e => e.Pporder).HasColumnName("PPOrder");
            entity.Property(e => e.SupplierClientRefrence)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.ToAcc).HasMaxLength(183);
            entity.Property(e => e.Vcref)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VCRef");
        });

        modelBuilder.Entity<VEntityEntityTypeList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityEntityTypeList");

            entity.Property(e => e.DefaultLink)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EntityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.PkEntityTypeId).HasColumnName("PkEntityTypeID");
        });

        modelBuilder.Entity<VEntityFeesConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityFeesConfig");

            entity.Property(e => e.CommissionType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Consumer)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasColumnType("decimal(25, 8)");
        });

        modelBuilder.Entity<VEntityList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityList");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkEntitySubTypeId).HasColumnName("FkEntitySubTypeID");
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.LastUpdated).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.QuickInfo)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatexemption).HasColumnName("VATExemption");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VEntityOrderSweep>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityOrderSweep");

            entity.Property(e => e.DeletedEntiySweep).HasColumnName("Deleted Entiy sweep");
            entity.Property(e => e.Entity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.FromAccount)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FromAccountNo).HasMaxLength(30);
            entity.Property(e => e.LastProcessed).HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.PkEntityOrderSweepId).HasColumnName("PkEntityOrderSweepID");
        });

        modelBuilder.Entity<VEntityRight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityRights");

            entity.Property(e => e.Name)
                .HasMaxLength(65)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.PkGroupId).HasColumnName("PkGroupID");
            entity.Property(e => e.PkModuleId).HasColumnName("PkModuleID");
            entity.Property(e => e.PkUserId).HasColumnName("PkUserID");
        });

        modelBuilder.Entity<VEntitySupplierClient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntitySupplierClient");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntityTypeId).HasColumnName("EntityTypeID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkEntitySubTypeId).HasColumnName("FkEntitySubTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.QuickInfo)
                .HasMaxLength(736)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierClientTypeId).HasColumnName("SupplierClientTypeID");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VEntitySupplierList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntitySupplierList");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VEntityWithdrawalList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vEntityWithdrawalList");

            entity.Property(e => e.AccountNo).HasMaxLength(30);
            entity.Property(e => e.AccountType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchCode).HasMaxLength(20);
            entity.Property(e => e.BranchName).HasMaxLength(50);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Currency).HasMaxLength(106);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Facility).HasColumnType("money");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SwiftCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
        });

        modelBuilder.Entity<VGoodsFeesPerEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vGoodsFeesPerEntity");

            entity.Property(e => e.Agent)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CommissionType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Value).HasColumnType("decimal(25, 8)");
        });

        modelBuilder.Entity<VGridZone>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vGridZones");

            entity.Property(e => e.Grid)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GridZone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkGridId).HasColumnName("PkGridID");
            entity.Property(e => e.PkGridZoneId).HasColumnName("PkGridZoneID");
            entity.Property(e => e.Status)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vGroup");

            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkGroupId).HasColumnName("PkGroupID");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
        });

        modelBuilder.Entity<VItemClass>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vItemClass");

            entity.Property(e => e.ClassDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkClassId).HasColumnName("PkClassID");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
        });

        modelBuilder.Entity<VItemClassCatIncluded>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vItemClassCatIncluded");

            entity.Property(e => e.ClassCatDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DisplayValue)
                .HasMaxLength(653)
                .IsUnicode(false);
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkClassId).HasColumnName("FkClassID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PkCategoryIncludedId).HasColumnName("PkCategoryIncludedID");
        });

        modelBuilder.Entity<VItemClassCatLinkedToEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vItemClassCatLinkedToEntities");

            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClassCatDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ClassDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DisplayValue)
                .HasMaxLength(173)
                .IsUnicode(false);
            entity.Property(e => e.EntityName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PkClassCatagoryId).HasColumnName("PkClassCatagoryID");
            entity.Property(e => e.PkClassId).HasColumnName("PkClassID");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
        });

        modelBuilder.Entity<VItemClassCategory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vItemClassCategory");

            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClassCatDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ClassDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DisplayValue)
                .HasMaxLength(173)
                .IsUnicode(false);
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PkClassCatagoryId).HasColumnName("PkClassCatagoryID");
            entity.Property(e => e.PkClassId).HasColumnName("PkClassID");
            entity.Property(e => e.Status)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
        });

        modelBuilder.Entity<VItemClassCategoryIe>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vItemClassCategoryIE");

            entity.Property(e => e.ClassCatDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FkClassId).HasColumnName("FkClassID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PkClassCatagoryId).HasColumnName("PkClassCatagoryID");
        });

        modelBuilder.Entity<VLineChartDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vLineChartData");

            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkClassCategoryId).HasColumnName("FkClassCategoryID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.PkClassId).HasColumnName("PkClassID");
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VLoginAudit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vLoginAudit");

            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IPAddress");
            entity.Property(e => e.LoginDate).HasColumnType("datetime");
            entity.Property(e => e.LoginSuccess)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Passsword)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Pin)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PkLoginAuditId).HasColumnName("PkLoginAuditID");
            entity.Property(e => e.Username)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VMagisterialDistrictProvince>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vMagisterialDistrictProvince");

            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PkMagisterialDistrictId).HasColumnName("PkMagisterialDistrictID");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VMyPlan>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vMyPlans");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(560)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.ExtRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderTotal)
                .HasMaxLength(51)
                .IsUnicode(false);
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.PkOrderItemId).HasColumnName("PkOrderItemID");
            entity.Property(e => e.Price)
                .HasMaxLength(51)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Vendor)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VOrderApprovalList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderApprovalList");

            entity.Property(e => e.BeneficiaryAccountInfo).HasMaxLength(183);
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClientTaname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ClientTAName");
            entity.Property(e => e.FkClientAddressId).HasColumnName("FkClientAddressID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOwnerEntityId).HasColumnName("FkOwnerEntityID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(9)
                .IsUnicode(false);
            entity.Property(e => e.PkGroupId).HasColumnName("PkGroupID");
            entity.Property(e => e.PkOrderApprovalId).HasColumnName("PkOrderApprovalID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RequisitionAmount).HasColumnType("money");
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VOrderCommissionList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderCommissionList");

            entity.Property(e => e.CommissionSum).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
        });

        modelBuilder.Entity<VOrderCommissionListBase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderCommissionListBase");

            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.TotalFixedComm).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.TotalPerQcomm)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("TotalPerQComm");
            entity.Property(e => e.TotalPercComm).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.VatIncl)
                .HasColumnType("decimal(38, 7)")
                .HasColumnName("VAT Incl");
        });

        modelBuilder.Entity<VOrderHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderHistory");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.Note).HasMaxLength(500);
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkOrderHistoryId).HasColumnName("PkOrderHistoryID");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VOrderItemCommissionList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderItemCommissionList");

            entity.Property(e => e.CommissionSum).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.PkOrderItemId).HasColumnName("PkOrderItemID");
        });

        modelBuilder.Entity<VOrderItemCommissionListBase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderItemCommissionListBase");

            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.FkAgentId).HasColumnName("FkAgentID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.PkOrderItemId).HasColumnName("PkOrderItemID");
            entity.Property(e => e.TotalFixedComm).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalPerQcomm)
                .HasColumnType("decimal(38, 4)")
                .HasColumnName("TotalPerQComm");
            entity.Property(e => e.TotalPercComm).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.VatIncl)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("VAT Incl");
        });

        modelBuilder.Entity<VOrderItemGboclist>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderItemGBOCList");

            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkFromAccountId).HasColumnName("FkFromAccountID");
            entity.Property(e => e.SumVatincl)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("SumVATIncl");
        });

        modelBuilder.Entity<VOrderItemGbolist>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderItemGBOList");

            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BankUserCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.ExtRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAllocatedVendorId).HasColumnName("FkAllocatedVendorID");
            entity.Property(e => e.FkClientAddressId).HasColumnName("FkClientAddressID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkFromAccountId).HasColumnName("FkFromAccountID");
            entity.Property(e => e.FkFromBankId).HasColumnName("FkFromBankID");
            entity.Property(e => e.FkLinkedOrderId).HasColumnName("FkLinkedOrderID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOrderTypeId).HasColumnName("FkOrderTypeID");
            entity.Property(e => e.FkOutwardBopcategoryId).HasColumnName("FkOutwardBOPCategoryId");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.FkToAccountId).HasColumnName("FkToAccountID");
            entity.Property(e => e.Grnreference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GRNReference");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.InstantPayment)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Receipt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SumCommissionExclVat).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.SumExclComm).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.SumVat)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("SumVAT");
            entity.Property(e => e.SumVatincl)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("SumVATIncl");
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VOrderItemList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderItemList");

            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AllocationOverridgeQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.BankUserCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CommissionExclVat).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.CommissionInclVat).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.CommissionVat).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.ExtRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAllocatedVendorId).HasColumnName("FkAllocatedVendorID");
            entity.Property(e => e.FkClientAddressId).HasColumnName("FkClientAddressID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkFromAccountId).HasColumnName("FkFromAccountID");
            entity.Property(e => e.FkFromBankId).HasColumnName("FkFromBankID");
            entity.Property(e => e.FkLinkedOrderId).HasColumnName("FkLinkedOrderID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOrderTypeId).HasColumnName("FkOrderTypeID");
            entity.Property(e => e.FkOutwardBopcategoryId).HasColumnName("FkOutwardBOPCategoryId");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.FkToAccountId).HasColumnName("FkToAccountID");
            entity.Property(e => e.Grnreference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GRNReference");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.InstantPayment)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OrderTotalVatincl)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("OrderTotalVATIncl");
            entity.Property(e => e.OverridgeQty).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.OverridgeTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkOrderItemId).HasColumnName("PkOrderItemID");
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PriceExclComm).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.ProductAllocationQty)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("ProductAllocationQTY");
            entity.Property(e => e.ProductAllocationTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Receipt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(38, 7)");
            entity.Property(e => e.TotalExclComm).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.TotalFixedComm).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalPercComm).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalQuantity).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.Vat)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("VAT");
            entity.Property(e => e.Vatexemption).HasColumnName("VATExemption");
            entity.Property(e => e.Vatincl)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("VATIncl");
            entity.Property(e => e.Vatrule).HasColumnName("VATRule");
        });

        modelBuilder.Entity<VOrderItemListBase>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderItemListBase");

            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AllocationOverridgeQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.BankUserCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.ExtRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkAllocatedVendorId).HasColumnName("FkAllocatedVendorID");
            entity.Property(e => e.FkClientAddressId).HasColumnName("FkClientAddressID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkFromAccountId).HasColumnName("FkFromAccountID");
            entity.Property(e => e.FkFromBankId).HasColumnName("FkFromBankID");
            entity.Property(e => e.FkLinkedOrderId).HasColumnName("FkLinkedOrderID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOrderTypeId).HasColumnName("FkOrderTypeID");
            entity.Property(e => e.FkOutwardBopcategoryId).HasColumnName("FkOutwardBOPCategoryId");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.FkToAccountId).HasColumnName("FkToAccountID");
            entity.Property(e => e.Grnreference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GRNReference");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.InstantPayment)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OrderTotalVatincl)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("OrderTotalVATIncl");
            entity.Property(e => e.OverridgeQty).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.OverridgeTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkOrderItemId).HasColumnName("PkOrderItemID");
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ProductAllocationQty)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("ProductAllocationQTY");
            entity.Property(e => e.ProductAllocationTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Receipt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(38, 7)");
            entity.Property(e => e.TotalFixedComm).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalPercComm).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.TotalQuantity).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.Vat)
                .HasColumnType("numeric(38, 6)")
                .HasColumnName("VAT");
            entity.Property(e => e.Vatexemption).HasColumnName("VATExemption");
            entity.Property(e => e.Vatincl)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("VATIncl");
            entity.Property(e => e.Vatrule).HasColumnName("VATRule");
        });

        modelBuilder.Entity<VOrderItemListSupplier>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderItemListSupplier");

            entity.Property(e => e.Bopcode)
                .IsUnicode(false)
                .HasColumnName("BOPCode");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkLinkedOrderItemId).HasColumnName("FkLinkedOrderItemID");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.InclVat).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OverridgeQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OverridgeTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkOrderItemId).HasColumnName("PkOrderItemID");
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Reference).HasMaxLength(255);
            entity.Property(e => e.ServiceType)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Total).HasColumnType("decimal(38, 7)");
            entity.Property(e => e.Vat)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("VAT");
            entity.Property(e => e.Vatincl).HasColumnName("VATIncl");
            entity.Property(e => e.Vatrule).HasColumnName("VATRule");
        });

        modelBuilder.Entity<VOrderItemOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderItemOrder");

            entity.Property(e => e.DeliveredQuantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Discount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkProductZoneId).HasColumnName("FkProductZoneID");
            entity.Property(e => e.FkStatusChangeUserId).HasColumnName("FkStatusChangeUserID");
            entity.Property(e => e.PkOrderItemId).HasColumnName("PkOrderItemID");
            entity.Property(e => e.PodreferenceNumber).HasColumnName("PODReferenceNumber");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.StatusChangeDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VOrderList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderList");

            entity.Property(e => e.AllocatedName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AllocatedNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Bopcode)
                .IsUnicode(false)
                .HasColumnName("BOPCode");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClientTa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ClientTA");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.ExtRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkFromAccountId).HasColumnName("FkFromAccountID");
            entity.Property(e => e.FkLinkedOrderId).HasColumnName("FkLinkedOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOrderTypeId).HasColumnName("FkOrderTypeID");
            entity.Property(e => e.FkToAccountId).HasColumnName("FkToAccountID");
            entity.Property(e => e.Grnreference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GRNReference");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.HasAttachment)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.InstantPayment)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PkClientId).HasColumnName("PkClientID");
            entity.Property(e => e.PkForwardedId).HasColumnName("PkForwardedID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.PkSupplierId).HasColumnName("PkSupplierID");
            entity.Property(e => e.Receipt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SumVatincl)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("SumVATIncl");
            entity.Property(e => e.SupplierBankReference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VOrderListChart>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderList_Charts");

            entity.Property(e => e.AllocatedName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.AllocatedNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClientTa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ClientTA");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.ExtRef)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkFromAccountId).HasColumnName("FkFromAccountID");
            entity.Property(e => e.FkLinkedOrderId).HasColumnName("FkLinkedOrderID");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.FkOrderTypeId).HasColumnName("FkOrderTypeID");
            entity.Property(e => e.FkToAccountId).HasColumnName("FkToAccountID");
            entity.Property(e => e.Grnreference)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GRNReference");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.InvoiceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PkClientId).HasColumnName("PkClientID");
            entity.Property(e => e.PkForwardedId).HasColumnName("PkForwardedID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.PkSupplierId).HasColumnName("PkSupplierID");
            entity.Property(e => e.Receipt)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VOrderOfPaymentGrandTotalsPerMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderOfPaymentGrandTotalsPerMonth");

            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Gt)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("GT");
            entity.Property(e => e.LastPaymentAmount).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.LastPaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.ValidDateRange)
                .HasMaxLength(53)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VOrderOfPaymentView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderOfPaymentView");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.LastPaymentAmount).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.LastPaymentDate).HasColumnType("datetime");
            entity.Property(e => e.MonthToDate).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.Ppamount)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("PPAmount");
            entity.Property(e => e.Pporder).HasColumnName("PPOrder");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.ValidDateRange)
                .HasMaxLength(53)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VOrderSupplier>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderSuppliers");

            entity.Property(e => e.FkOrderId).HasColumnName("FkOrderID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
        });

        modelBuilder.Entity<VOrderSweepFrequency>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vOrderSweepFrequency");

            entity.Property(e => e.PkOrderSweepFrequencyId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PkOrderSweepFrequencyID");
        });

        modelBuilder.Entity<VPlanning>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vPlanning");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.LastRequisitionDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.PlanNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PlanStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlanValue).HasColumnType("numeric(38, 6)");
        });

        modelBuilder.Entity<VProductApproval>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductApproval");

            entity.Property(e => e.Client)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IOrder).HasColumnName("iOrder");
            entity.Property(e => e.PkClientId).HasColumnName("PkClientID");
            entity.Property(e => e.PkClientProductApprovalId).HasColumnName("PkClientProductApprovalID");
            entity.Property(e => e.PkSupplierId).HasColumnName("PkSupplierID");
            entity.Property(e => e.Product)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Supplier)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VProductEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductEntity");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkBaseProductId).HasColumnName("FkBaseProductID");
            entity.Property(e => e.FkCategoryId).HasColumnName("FkCategoryID");
            entity.Property(e => e.FkProductStatusId).HasColumnName("FkProductStatusID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.FkUnitId).HasColumnName("FkUnitID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Size).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Specification)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Supplier)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VProductEntityAllAvailableProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductEntityAllAvailableProducts");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Supplier)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Vatincl).HasColumnName("VATIncl");
            entity.Property(e => e.Vatrule).HasColumnName("VATRule");
        });

        modelBuilder.Entity<VProductEntityWithExtra>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductEntityWithExtra");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkBaseProductId).HasColumnName("FkBaseProductID");
            entity.Property(e => e.FkCategoryId).HasColumnName("FkCategoryID");
            entity.Property(e => e.FkProductStatusId).HasColumnName("FkProductStatusID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Size).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Specification)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Supplier)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Vatincl).HasColumnName("VATIncl");
            entity.Property(e => e.Vatrule).HasColumnName("VATRule");
        });

        modelBuilder.Entity<VProductExcluded>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductExcluded");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkClientId).HasColumnName("PkClientID");
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VProductSupplierZone>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductSupplierZone");

            entity.Property(e => e.FkProductId).HasColumnName("FkProductID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PkSupplierZoneId).HasColumnName("PkSupplierZoneID");
        });

        modelBuilder.Entity<VRequisitionApproval>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vRequisitionApproval");

            entity.Property(e => e.ApprovalDate).HasColumnType("datetime");
            entity.Property(e => e.ApproverUser)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApproverUserId).HasColumnName("ApproverUserID");
            entity.Property(e => e.BeneficiaryAccountInfo).HasMaxLength(183);
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClientTaname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ClientTAName");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkClientId).HasColumnName("PkClientID");
            entity.Property(e => e.PkOrderApprovalId).HasColumnName("PkOrderApprovalID");
            entity.Property(e => e.PkOrderId).HasColumnName("PkOrderID");
            entity.Property(e => e.PkSupplierId).HasColumnName("PkSupplierID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RequisitionAmount).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VRewardsUploadFile>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vRewardsUploadFile");

            entity.Property(e => e.Filename)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("filename");
            entity.Property(e => e.Filepath)
                .IsUnicode(false)
                .HasColumnName("filepath");
            entity.Property(e => e.FkentityId).HasColumnName("FKEntityId");
            entity.Property(e => e.FkuploadedUserId).HasColumnName("FKUploadedUserId");
            entity.Property(e => e.PkrewardsUploadFileId).HasColumnName("PKRewardsUploadFileId");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
            entity.Property(e => e.UploadedEntity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UploadedUser)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VShare>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vShares");

            entity.Property(e => e.CededDate).HasColumnType("datetime");
            entity.Property(e => e.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Quantity).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VSubscription>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSubscriptions");

            entity.Property(e => e.Destination)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Subject)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
        });

        modelBuilder.Entity<VSupplierClient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClient");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VSupplierClientAccount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClientAccount");

            entity.Property(e => e.ClientAccountNumber).HasMaxLength(183);
            entity.Property(e => e.FkClientAccountId).HasColumnName("FkClientAccountID");
            entity.Property(e => e.FkSupplierAccountId).HasColumnName("FkSupplierAccountID");
            entity.Property(e => e.FkSupplierClientId).HasColumnName("FkSupplierClientID");
            entity.Property(e => e.PkSupplierClientAccountId).HasColumnName("PkSupplierClientAccountID");
            entity.Property(e => e.SupplierAccountNumber).HasMaxLength(183);
        });

        modelBuilder.Entity<VSupplierClientCu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClientCU");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VSupplierClientFilteredOnExcludedProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClientFilteredOnExcludedProducts");

            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
        });

        modelBuilder.Entity<VSupplierClientForPaymentPriority>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClientForPaymentPriority");

            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EntityType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FkClientAccountId).HasColumnName("FkClientAccountID");
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkSupplierAccountId).HasColumnName("FkSupplierAccountID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.Ppamount)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("PPAmount");
            entity.Property(e => e.Pporder).HasColumnName("PPOrder");
            entity.Property(e => e.SupplierClientRefrence)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VSupplierClientForPaymentPriorityOop>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClientForPaymentPriorityOOP");

            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EntityType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.Ppamount)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("PPAmount");
            entity.Property(e => e.Pporder).HasColumnName("PPOrder");
            entity.Property(e => e.SupplierClientRefrence)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VSupplierClientList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClientList");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VSupplierClientRefrenceNo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClientRefrenceNo");

            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.SupplierClientRefrence)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VSupplierClientSu>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSupplierClientSU");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ClientName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VSweepRecord>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vSweepRecord");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ExpiryAmount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.ExpiryAmountResetDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.FromAccount)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FromAccountNo).HasMaxLength(30);
            entity.Property(e => e.FromBank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastProcessed).HasColumnType("datetime");
            entity.Property(e => e.MinimumRequiredBalance).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PaymentValue).HasColumnType("decimal(16, 2)");
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.PkSweepId).HasColumnName("PkSweepID");
            entity.Property(e => e.RequiredRemainingBalance).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.SuccessfullProcessed).HasColumnType("datetime");
            entity.Property(e => e.ToAccount)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ToAccountNo).HasMaxLength(30);
            entity.Property(e => e.ToBank)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalSweepAmount).HasColumnType("decimal(19, 4)");
        });

        modelBuilder.Entity<VTermsAndConditionsAudit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTermsAndConditionsAudit");

            entity.Property(e => e.DateAccepted).HasColumnType("datetime");
            entity.Property(e => e.DateReset).HasColumnType("datetime");
            entity.Property(e => e.GpayTermsAccepted).HasColumnName("GPayTermsAccepted");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(8000)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VTownProvince>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTownProvince");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkTownId).HasColumnName("PkTownID");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VTransactionMappingRule>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTransactionMappingRules");

            entity.Property(e => e.AccountLabel)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.AccountNumber).HasMaxLength(30);
            entity.Property(e => e.Category)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.EntityTa)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EntityTA");
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkClassCatagoryId).HasColumnName("FkClassCatagoryID");
            entity.Property(e => e.FkRuleTypeId).HasColumnName("FkRuleTypeID");
            entity.Property(e => e.FormattedAccountDetails).HasMaxLength(183);
            entity.Property(e => e.MappedDate).HasColumnType("datetime");
            entity.Property(e => e.RuleOrigin)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.RuleType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDescription)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VTxCodeMap>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vTxCodeMap");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Term).HasMaxLength(200);
            entity.Property(e => e.TransactionCode)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VUnallocatedFund>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUnallocatedFunds");

            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.FkAccountId).HasColumnName("FkAccountID");
            entity.Property(e => e.FkEntityId).HasColumnName("FkEntityID");
            entity.Property(e => e.PkEntityBankStatementId).HasColumnName("PkEntityBankStatementID");
            entity.Property(e => e.ReferenceNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VUploadHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUploadHistory");

            entity.Property(e => e.FileName).IsUnicode(false);
            entity.Property(e => e.UploadBy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VUploadReference>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUploadReference");

            entity.Property(e => e.ControllingEntity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VUserAgent>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserAgents");

            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkAgentId).HasColumnName("PkAgentID");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
        });

        modelBuilder.Entity<VUserClient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserClients");

            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.Taname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("TAName");
        });

        modelBuilder.Entity<VUserClientAddress>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserClientAddress");

            entity.Property(e => e.Address1)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.FkAddressTypeId).HasColumnName("FkAddressTypeID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityAddressId).HasColumnName("PkEntityAddressID");
        });

        modelBuilder.Entity<VUserEntity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserEntities");

            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
        });

        modelBuilder.Entity<VUserGroup>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserGroup");

            entity.Property(e => e.AccountStatus)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.PkUserId).HasColumnName("PkUserID");
            entity.Property(e => e.Uipersistance)
                .HasColumnType("xml")
                .HasColumnName("UIPersistance");
        });

        modelBuilder.Entity<VUserGroupRight>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserGroupRights");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Group)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkGroupId).HasColumnName("PkGroupID");
            entity.Property(e => e.PkUserId).HasColumnName("PkUserID");
            entity.Property(e => e.User)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VUserSupplier>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserSuppliers");

            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
        });

        modelBuilder.Entity<VUserSuppliersClient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserSuppliersClients");

            entity.Property(e => e.FkClientId).HasColumnName("FkClientID");
            entity.Property(e => e.FkSupplierId).HasColumnName("FkSupplierID");
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.SupplierClientRefrence)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VUserTypesAllowedPerUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vUserTypesAllowedPerUser");

            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.FkUserTypesId).HasColumnName("FkUserTypesID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkUserTypeId).HasColumnName("pkUserTypeID");
        });

        modelBuilder.Entity<VallProduct>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VAllProduct");

            entity.Property(e => e.Category)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Client)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ClientAddress)
                .HasMaxLength(408)
                .IsUnicode(false);
            entity.Property(e => e.ClientAddressId).HasColumnName("ClientAddressID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContribType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ContribValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Differential).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.DifferentialType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkDifferentialTypeId).HasColumnName("FkDifferentialTypeID");
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.GridZone)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MarkupType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MarkupValue).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.MaxOrder).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.MinOrder).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OverridgeQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OverridgeTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.Price).HasColumnType("decimal(38, 6)");
            entity.Property(e => e.ProductAllocationQty)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("ProductAllocationQTY");
            entity.Property(e => e.ProductAllocationTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductSupplierId).HasColumnName("ProductSupplierID");
            entity.Property(e => e.ProductZoneId).HasColumnName("ProductZoneID");
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Size).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Specification)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.Supplier)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VatText)
                .HasMaxLength(11)
                .IsUnicode(false);
            entity.Property(e => e.Vatexemption).HasColumnName("VATExemption");
            entity.Property(e => e.Vatincl).HasColumnName("VATIncl");
            entity.Property(e => e.Vatrule).HasColumnName("VATRule");
        });

        modelBuilder.Entity<Vatamount>(entity =>
        {
            entity.HasKey(e => e.PkVatId).HasFillFactor(90);

            entity.ToTable("VATAmount");

            entity.Property(e => e.PkVatId)
                .ValueGeneratedNever()
                .HasColumnName("PkVatID");
            entity.Property(e => e.Amount).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.Expired).HasColumnType("datetime");
        });

        modelBuilder.Entity<VbaseProductAll>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VBaseProductAll");

            entity.Property(e => e.BaseProductCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BaseProductDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.BaseProductGridId).HasColumnName("BaseProductGridID");
            entity.Property(e => e.BaseProductId).HasColumnName("BaseProductID");
            entity.Property(e => e.BaseProductPrice).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.BaseProductPriceId).HasColumnName("BaseProductPriceID");
            entity.Property(e => e.Differential).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
            entity.Property(e => e.GridDescription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GridZoneDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GridZOneDescription");
            entity.Property(e => e.MagisterialDistrictGridZoneId).HasColumnName("MagisterialDistrictGridZoneID");
            entity.Property(e => e.MagisterialDistrictName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vehicles__3213E83FF2CE9D36");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active).HasDefaultValue(true);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Make)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TransactionsPerDay)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.VinNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VentityApprovalList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VEntityApprovalList");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Comments)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(2500)
                .IsUnicode(false);
            entity.Property(e => e.EntityReferenceAlias)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkEntitySubTypeId).HasColumnName("FkEntitySubTypeID");
            entity.Property(e => e.FkIdentificationTypeId).HasColumnName("FkIdentificationTypeID");
            entity.Property(e => e.FkRequestedUserId).HasColumnName("FkRequestedUserID");
            entity.Property(e => e.FkStatusId).HasColumnName("FkStatusID");
            entity.Property(e => e.Idnumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("IDNumber");
            entity.Property(e => e.ImageLocation)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LastSyncDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkEntityId).HasColumnName("PkEntityID");
            entity.Property(e => e.RegistrationNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RejectReason)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Taname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TAName");
            entity.Property(e => e.Vatexemption).HasColumnName("VATExemption");
            entity.Property(e => e.Vatnumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("VATNumber");
            entity.Property(e => e.Vatregistered).HasColumnName("VATRegistered");
            entity.Property(e => e.WbulkLimitSgn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WBulkLimitSgn");
            entity.Property(e => e.WbulkPaymentType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WBulkPaymentType");
        });

        modelBuilder.Entity<VetguuserType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VETGUUserType");

            entity.Property(e => e.EntityTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FkEntityTypeId).HasColumnName("FkEntityTypeID");
            entity.Property(e => e.FkGroupId).HasColumnName("FkGroupID");
            entity.Property(e => e.FkUserTypeId).HasColumnName("FkUserTypeID");
            entity.Property(e => e.FkuserId).HasColumnName("FKUserID");
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkEtguuserTypeId).HasColumnName("PkETGUUserTypeID");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VproductAllocationOver>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VProductAllocationOvers");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated).HasColumnType("datetime");
            entity.Property(e => e.OverridgeQty).HasColumnType("decimal(19, 4)");
            entity.Property(e => e.OverridgeTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PkAllocationOverId).HasColumnName("PkAllocationOverID");
            entity.Property(e => e.PkProductId).HasColumnName("PkProductID");
            entity.Property(e => e.PkProductZoneId).HasColumnName("PkProductZoneID");
            entity.Property(e => e.PkSupplierClientId).HasColumnName("PkSupplierClientID");
            entity.Property(e => e.ProductAllocationQty)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("ProductAllocationQTY");
            entity.Property(e => e.ProductAllocationTerms)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SupplierZoneName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VuserSecurity>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VUserSecurity");

            entity.Property(e => e.Answer)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FkUserId).HasColumnName("FkUserID");
            entity.Property(e => e.PkUserSecurityQuestionId).HasColumnName("PkUserSecurityQuestionID");
            entity.Property(e => e.PkUserSercurityAnswerId).HasColumnName("PkUserSercurityAnswerID");
            entity.Property(e => e.SecurityQuestion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
