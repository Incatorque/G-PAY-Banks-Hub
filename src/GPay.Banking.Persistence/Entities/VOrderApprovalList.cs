using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderApprovalList
{
    public Guid PkOrderApprovalId { get; set; }

    public Guid PkOrderId { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkClientAddressId { get; set; }

    public DateTime OrderDate { get; set; }

    public string? OrderNumber { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public Guid FkUserId { get; set; }

    public string ClientName { get; set; } = null!;

    public string? ClientTaname { get; set; }

    public bool? Active { get; set; }

    public string SupplierName { get; set; } = null!;

    public decimal? RequisitionAmount { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? BeneficiaryAccountInfo { get; set; }

    public Guid? FkOwnerEntityId { get; set; }

    public Guid? PkGroupId { get; set; }

    public string? SupplierBankReference { get; set; }

    public Guid FkSupplierId { get; set; }
}
