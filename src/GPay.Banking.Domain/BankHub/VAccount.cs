using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VAccount
{
    [Column("PkAccountID")]
    public Guid PkAccountId { get; set; }

    [StringLength(30)]
    public string AccountNumber { get; set; } = null!;

    [StringLength(50)]
    public string BranchName { get; set; } = null!;

    [StringLength(20)]
    public string? ResourceType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BankName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AccountType { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Entity { get; set; }

    public bool? Active { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? AccountLabel { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AccountStatus { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ControllingEntity { get; set; }

    [Column("CountryCurrencyID")]
    public int? CountryCurrencyId { get; set; }

    [StringLength(106)]
    public string? CountryCurrencyDisplay { get; set; }

    [Column("FkControllingEntityID")]
    public Guid? FkControllingEntityId { get; set; }

    [Column("FkBankID")]
    public Guid FkBankId { get; set; }

    public bool? Primarary { get; set; }

    [StringLength(20)]
    public string BranchCode { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? BankUserCode { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column(TypeName = "decimal(38, 4)")]
    public decimal Balance { get; set; }

    public bool? HasFacility { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string HasFacilityText { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal? FacilityValue { get; set; }

    [StringLength(5)]
    [Unicode(false)]
    public string HasCharges { get; set; } = null!;
}


