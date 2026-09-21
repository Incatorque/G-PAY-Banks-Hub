using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VAccountPending
{
    public Guid PkAccountPendingId { get; set; }

    public Guid FkBankId { get; set; }

    public Guid FkAccountTypeId { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string BranchName { get; set; } = null!;

    public string BranchCode { get; set; } = null!;

    public string? ResourceType { get; set; }

    public Guid? FkEntityId { get; set; }

    public bool? Primarary { get; set; }

    public string? AccountLabel { get; set; }

    public string? BankUserCode { get; set; }

    public Guid? FkAccountStatusId { get; set; }

    public bool? MapAccount { get; set; }

    public Guid? FkControllingEntityId { get; set; }

    public int? CountryCurrencyId { get; set; }

    public bool? ToDelete { get; set; }

    public string? BankName { get; set; }

    public string? AccountType { get; set; }

    public string? Entity { get; set; }

    public string? AccountStatus { get; set; }

    public string? ControllingEntity { get; set; }

    public string? CountryCurrencyDisplay { get; set; }
}
