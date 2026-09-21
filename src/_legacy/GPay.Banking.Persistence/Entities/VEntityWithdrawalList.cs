using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityWithdrawalList
{
    public Guid PkEntityId { get; set; }

    public Guid PkSupplierClientId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Taname { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Vatnumber { get; set; }

    public DateTime? LastSyncDate { get; set; }

    public string? Comments { get; set; }

    public string? Description { get; set; }

    public Guid FkStatusId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public string Status { get; set; } = null!;

    public string? ImageLocation { get; set; }

    public string? GroupName { get; set; }

    public string AccountNo { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string BranchCode { get; set; } = null!;

    public string? SwiftCode { get; set; }

    public string AccountType { get; set; } = null!;

    public string Bank { get; set; } = null!;

    public Guid FkSupplierId { get; set; }

    public Guid FkClientId { get; set; }

    public bool? HasFacility { get; set; }

    public Guid FromAccountId { get; set; }

    public decimal Facility { get; set; }

    public string? Currency { get; set; }
}
