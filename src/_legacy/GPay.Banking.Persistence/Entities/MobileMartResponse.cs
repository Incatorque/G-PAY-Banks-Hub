using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MobileMartResponse
{
    public Guid PkMobileMartResponseId { get; set; }

    public string TransactionId { get; set; } = null!;

    public DateTime TransactionTimeStamp { get; set; }

    public string ProductInstructions { get; set; } = null!;

    public decimal? ProductAmount { get; set; }

    public decimal? ProductAmountDue { get; set; }

    public string MobileMartProductId { get; set; } = null!;

    public string Pin { get; set; } = null!;

    public string SerialNumber { get; set; } = null!;

    public string ContentCreator { get; set; } = null!;

    public string AccountNumber { get; set; } = null!;

    public string AccountHolder { get; set; } = null!;

    public string Reference { get; set; } = null!;

    public string ServiceProviderContact { get; set; } = null!;

    public Guid? FkOrderId { get; set; }

    public Guid? FkReverseEntityBankStatementId { get; set; }

    public string MerchantProductId { get; set; } = null!;

    public string ProductCategory { get; set; } = null!;

    public Guid FkUserId { get; set; }

    public virtual Order? FkOrder { get; set; }

    public virtual EntityBankStatement? FkReverseEntityBankStatement { get; set; }

    public virtual User FkUser { get; set; } = null!;

    public virtual ICollection<MobileMartAccountStatus> MobileMartAccountStatuses { get; set; } = new List<MobileMartAccountStatus>();

    public virtual ICollection<MobileMartUtilityConsumerDetail> MobileMartUtilityConsumerDetails { get; set; } = new List<MobileMartUtilityConsumerDetail>();

    public virtual ICollection<MobileMartUtilityMunicipalityDetail> MobileMartUtilityMunicipalityDetails { get; set; } = new List<MobileMartUtilityMunicipalityDetail>();

    public virtual ICollection<MobileMartUtilityToken> MobileMartUtilityTokens { get; set; } = new List<MobileMartUtilityToken>();
}
