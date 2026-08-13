using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityBankInfo
{
    public Guid PkBankInfoId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkBankId { get; set; }

    public string AccountNo { get; set; } = null!;

    public string? BranchCode { get; set; }

    public Guid FkAccountTypeId { get; set; }

    public bool? Primarary { get; set; }

    public bool? ProcurementAccount { get; set; }

    public string? BranchName { get; set; }

    public bool? FacilityAccount { get; set; }

    public virtual AccountType FkAccountType { get; set; } = null!;

    public virtual Bank FkBank { get; set; } = null!;

    public virtual Entity FkEntity { get; set; } = null!;
}
