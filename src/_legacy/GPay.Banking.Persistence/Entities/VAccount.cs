using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VAccount
{
    public Guid PkAccountId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string? ResourceType { get; set; }

    public string? BankName { get; set; }

    public string? AccountType { get; set; }

    public string? Entity { get; set; }

    public bool? Active { get; set; }

    public string? AccountLabel { get; set; }

    public string? AccountStatus { get; set; }

    public string? ControllingEntity { get; set; }

    public int? CountryCurrencyId { get; set; }

    public string? CountryCurrencyDisplay { get; set; }

    public Guid? FkControllingEntityId { get; set; }

    public Guid FkBankId { get; set; }

    public bool? Primarary { get; set; }

    public string BranchCode { get; set; } = null!;

    public string? BankUserCode { get; set; }

    public Guid? FkEntityId { get; set; }

    public DateTime CreateDate { get; set; }

    public decimal Balance { get; set; }

    public bool? HasFacility { get; set; }

    public string HasFacilityText { get; set; } = null!;

    public decimal? FacilityValue { get; set; }

    public string HasCharges { get; set; } = null!;
}
