using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityAccount
{
    public Guid PkEntityAccountId { get; set; }

    public string? Entity { get; set; }

    public string AccountNumber { get; set; } = null!;

    public decimal TransactionCost { get; set; }

    public bool? Active { get; set; }

    public string? RegistrationNumber { get; set; }

    public Guid FkAccountId { get; set; }

    public Guid FkEntityId { get; set; }

    public string? EntityOwner { get; set; }

    public string? ResourceType { get; set; }

    public string Bank { get; set; } = null!;

    public decimal? Balance { get; set; }

    public decimal? AvailableBalance { get; set; }

    public string AccountType { get; set; } = null!;

    public string BranchCode { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public bool? Primarary { get; set; }

    public string? SwiftCode { get; set; }

    public Guid FkBankId { get; set; }

    public Guid FkAccountTypeId { get; set; }

    public string? AccountLabel { get; set; }

    public string? ReferenceNumber { get; set; }

    public string GroupName { get; set; } = null!;

    public string? StatusDescription { get; set; }

    public string ControllingEntity { get; set; } = null!;

    public Guid? FkControllingEntityId { get; set; }

    public Guid FkStatusId { get; set; }

    public int? CountryCurrencyId { get; set; }

    public Guid? FkentityOwnerId { get; set; }

    public bool? DefaultToThisEntity { get; set; }

    public decimal Facility { get; set; }

    public string? Currency { get; set; }

    public string ServiceType { get; set; } = null!;

    public string HasCharges { get; set; } = null!;

    public DateTime? DateCreated { get; set; }
}
