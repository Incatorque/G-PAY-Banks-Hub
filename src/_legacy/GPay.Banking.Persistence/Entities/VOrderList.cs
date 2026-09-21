using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderList
{
    public Guid PkOrderId { get; set; }

    public string? ClientName { get; set; }

    public string? ClientTa { get; set; }

    public string? OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public string HasAttachment { get; set; } = null!;

    public string Status { get; set; } = null!;

    public Guid FkOrderStatusId { get; set; }

    public string InstantPayment { get; set; } = null!;

    public string? SupplierName { get; set; }

    public Guid PkSupplierId { get; set; }

    public Guid PkClientId { get; set; }

    public decimal? SumVatincl { get; set; }

    public bool? ForwardGuarantee { get; set; }

    public Guid? PkForwardedId { get; set; }

    public string? AllocatedName { get; set; }

    public Guid? FkLinkedOrderId { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? FkOrderTypeId { get; set; }

    public string? InvoiceNumber { get; set; }

    public Guid? FkFromAccountId { get; set; }

    public Guid? FkToAccountId { get; set; }

    public string? Receipt { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? SupplierBankReference { get; set; }

    public string? AllocatedNumber { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? Grnreference { get; set; }

    public string? ClientTypes { get; set; }

    public string? SupplierTypes { get; set; }

    public string DeliveryAddress { get; set; } = null!;

    public string ExtRef { get; set; } = null!;

    public Guid? FkPlanId { get; set; }

    public string Bopcode { get; set; } = null!;
}
