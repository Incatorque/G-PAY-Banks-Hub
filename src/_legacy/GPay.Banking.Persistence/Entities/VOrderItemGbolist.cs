using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderItemGbolist
{
    public string? OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public string? Receipt { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? Grnreference { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? SupplierBankReference { get; set; }

    public string? InvoiceNumber { get; set; }

    public int? BankSequence { get; set; }

    public string? BankUserCode { get; set; }

    public string ExtRef { get; set; } = null!;

    public string Address1 { get; set; } = null!;

    public int? TotalItems { get; set; }

    public decimal? SumCommissionExclVat { get; set; }

    public decimal? SumExclComm { get; set; }

    public decimal? SumVatincl { get; set; }

    public decimal? SumVat { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public Guid FkClientAddressId { get; set; }

    public Guid? FkAllocatedVendorId { get; set; }

    public Guid? FkLinkedOrderId { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? FkOrderTypeId { get; set; }

    public Guid? FkFromAccountId { get; set; }

    public Guid? FkToAccountId { get; set; }

    public Guid FkFromBankId { get; set; }

    public decimal? DeliveredQuantity { get; set; }

    public string InstantPayment { get; set; } = null!;

    public Guid? FkPlanId { get; set; }

    public Guid? FkOutwardBopcategoryId { get; set; }
}
