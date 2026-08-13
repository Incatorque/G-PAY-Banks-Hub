using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityBeneficiaryListDistinct
{
    public Guid PkEntityId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Taname { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Vatnumber { get; set; }

    public decimal MinPurchaseQty { get; set; }

    public decimal MaxPurchaseQty { get; set; }

    public decimal AllocationQty { get; set; }

    public DateTime? LastSyncDate { get; set; }

    public string? Comments { get; set; }

    public string? Description { get; set; }

    public Guid FkStatusId { get; set; }

    public bool? Deleted { get; set; }

    public Guid? FkEntityTypeId { get; set; }

    public bool Active { get; set; }

    public DateTime DateCreated { get; set; }

    public string Status { get; set; } = null!;

    public string? GroupName { get; set; }

    public string AccountNo { get; set; } = null!;

    public string Bank { get; set; } = null!;

    public string? EntityTypeIndicator { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkClientAccountId { get; set; }

    public bool? HasFacility { get; set; }

    public decimal Facility { get; set; }

    public string? EntityReferenceAlias { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? SwiftCode { get; set; }

    public string? Currency { get; set; }
}
