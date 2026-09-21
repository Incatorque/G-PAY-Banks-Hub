using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VRequisitionApproval
{
    public Guid PkOrderApprovalId { get; set; }

    public Guid PkOrderId { get; set; }

    public string? OrderNumber { get; set; }

    public string? ClientName { get; set; }

    public string? ClientTaname { get; set; }

    public string? SupplierName { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal? RequisitionAmount { get; set; }

    public string? BeneficiaryAccountInfo { get; set; }

    public string? ReferenceNumber { get; set; }

    public Guid ApproverUserId { get; set; }

    public string? ApproverUser { get; set; }

    public Guid? GroupId { get; set; }

    public Guid PkClientId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public Guid FkOrderStatusId { get; set; }

    public Guid PkSupplierId { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public int Order { get; set; }

    public bool? PreviousLevelIsApproved { get; set; }
}
