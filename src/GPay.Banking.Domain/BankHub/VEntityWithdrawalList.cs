using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityWithdrawalList
{
    [Column("PkEntityID")]
    public Guid PkEntityId { get; set; }

    [Column("PkSupplierClientID")]
    public Guid PkSupplierClientId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("TAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taname { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? RegistrationNumber { get; set; }

    [Column("VATNumber")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Vatnumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastSyncDate { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comments { get; set; }

    [StringLength(2500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("FkStatusID")]
    public Guid FkStatusId { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    public string? GroupName { get; set; }

    [StringLength(30)]
    public string AccountNo { get; set; } = null!;

    [StringLength(50)]
    public string BranchName { get; set; } = null!;

    [StringLength(20)]
    public string BranchCode { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? SwiftCode { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string AccountType { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Bank { get; set; } = null!;

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    public bool? HasFacility { get; set; }

    public Guid FromAccountId { get; set; }

    [Column(TypeName = "money")]
    public decimal Facility { get; set; }

    [StringLength(106)]
    public string? Currency { get; set; }
}


