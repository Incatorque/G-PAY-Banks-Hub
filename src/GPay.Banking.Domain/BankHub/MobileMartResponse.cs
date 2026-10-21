using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("FkOrderId", Name = "IX_MobileMartResponses_FkOrderId")]
[Index("FkReverseEntityBankStatementId", Name = "IX_MobileMartResponses_FkReverseEntityBankStatementId")]
[Index("FkUserId", Name = "IX_MobileMartResponses_FkUserId")]
public partial class MobileMartResponse : Entity<Guid>
{
    [Key]
    [Column("PkMobileMartResponseId")]
    public override Guid Id { get; protected set; }

    public string TransactionId { get; set; } = null!;

    public DateTime TransactionTimeStamp { get; set; }

    public string ProductInstructions { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? ProductAmount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
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

    [ForeignKey("FkOrderId")]
    [InverseProperty("MobileMartResponses")]
    public virtual Order? FkOrder { get; set; }

    [ForeignKey("FkReverseEntityBankStatementId")]
    [InverseProperty("MobileMartResponses")]
    public virtual EntityBankStatement? FkReverseEntityBankStatement { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("MobileMartResponses")]
    public virtual User FkUser { get; set; } = null!;

    [InverseProperty("FkMobileMartResponse")]
    public virtual ICollection<MobileMartAccountStatus> MobileMartAccountStatuses { get; set; } = new List<MobileMartAccountStatus>();

    [InverseProperty("FkMobileMartResponse")]
    public virtual ICollection<MobileMartUtilityConsumerDetail> MobileMartUtilityConsumerDetails { get; set; } = new List<MobileMartUtilityConsumerDetail>();

    [InverseProperty("FkMobileMartResponse")]
    public virtual ICollection<MobileMartUtilityMunicipalityDetail> MobileMartUtilityMunicipalityDetails { get; set; } = new List<MobileMartUtilityMunicipalityDetail>();

    [InverseProperty("FkMobileMartResponse")]
    public virtual ICollection<MobileMartUtilityToken> MobileMartUtilityTokens { get; set; } = new List<MobileMartUtilityToken>();
}


