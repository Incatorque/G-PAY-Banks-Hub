using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderItemList
{
    public decimal? PriceExclComm { get; set; }

    public decimal? TotalExclComm { get; set; }

    public decimal? CommissionExclVat { get; set; }

    public decimal? CommissionVat { get; set; }

    public decimal? CommissionInclVat { get; set; }

    public string? OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public string? Receipt { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public decimal? DeliveredQuantity { get; set; }

    public string Code { get; set; } = null!;

    public string? ShortName { get; set; }

    public string Description { get; set; } = null!;

    public decimal? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Vat { get; set; }

    public decimal? Vatincl { get; set; }

    public decimal? OrderTotalVatincl { get; set; }

    public decimal? Total { get; set; }

    public decimal TotalPercComm { get; set; }

    public decimal TotalFixedComm { get; set; }

    public int? RoundingDecimals { get; set; }

    public string? Grnreference { get; set; }

    public string? ReferenceNumber { get; set; }

    public string InstantPayment { get; set; } = null!;

    public string? SupplierBankReference { get; set; }

    public string? InvoiceNumber { get; set; }

    public int? BankSequence { get; set; }

    public string? BankUserCode { get; set; }

    public decimal? ProductAllocationQty { get; set; }

    public string? ProductAllocationTerms { get; set; }

    public decimal? AllocationOverridgeQty { get; set; }

    public decimal? OverridgeQty { get; set; }

    public decimal? TotalQuantity { get; set; }

    public string? OverridgeTerms { get; set; }

    public string ExtRef { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public bool? AddedToBasketFromPlan { get; set; }

    public string ClientName { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public Guid PkOrderItemId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid? FkAllocatedVendorId { get; set; }

    public Guid PkProductId { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkClientAddressId { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public Guid? FkLinkedOrderId { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? FkOrderTypeId { get; set; }

    public Guid? FkFromAccountId { get; set; }

    public Guid? FkToAccountId { get; set; }

    public Guid FkFromBankId { get; set; }

    public Guid? FkPlanId { get; set; }

    public string? ContribType { get; set; }

    public string? MarkupType { get; set; }

    public decimal? ContribValue { get; set; }

    public decimal? MarkupValue { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? MarkupInclVat { get; set; }

    public bool? InclVat { get; set; }

    public Guid? FkFromProductId { get; set; }

    public bool? Vatexemption { get; set; }

    public bool? Vatrule { get; set; }

    public Guid? FkOutwardBopcategoryId { get; set; }
}
