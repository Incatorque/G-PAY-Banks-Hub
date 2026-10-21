using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityAccountOptimized
{
    [Column("PkEntityAccountID")]
    public Guid PkEntityAccountId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Entity { get; set; }

    [StringLength(30)]
    public string AccountNumber { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal TransactionCost { get; set; }

    public bool? Active { get; set; }

    [Column("FkAccountID")]
    public Guid FkAccountId { get; set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? EntityOwner { get; set; }

    [StringLength(20)]
    public string? ResourceType { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Bank { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string AccountType { get; set; } = null!;

    [StringLength(20)]
    public string BranchCode { get; set; } = null!;

    [StringLength(50)]
    public string BranchName { get; set; } = null!;

    public bool? Primarary { get; set; }

    [Column("FkBankID")]
    public Guid FkBankId { get; set; }

    [Column("FkAccountTypeID")]
    public Guid FkAccountTypeId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? AccountLabel { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [Column(TypeName = "money")]
    public decimal? Balance { get; set; }

    [Column(TypeName = "money")]
    public decimal? AvailableBalance { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    public string GroupName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? StatusDescription { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ControllingEntity { get; set; } = null!;

    [Column("FkControllingEntityID")]
    public Guid? FkControllingEntityId { get; set; }

    [Column("FkStatusID")]
    public Guid FkStatusId { get; set; }

    public int? CountryCurrencyId { get; set; }

    [Column("FKEntityOwnerID")]
    public Guid? FkentityOwnerId { get; set; }

    public bool? DefaultToThisEntity { get; set; }

    [Column(TypeName = "money")]
    public decimal Facility { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string ServiceType { get; set; } = null!;

    [StringLength(5)]
    [Unicode(false)]
    public string HasCharges { get; set; } = null!;
}


