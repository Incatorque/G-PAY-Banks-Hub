using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityBeneficiaryListDistinct
{
    [Column("PkEntityID")]
    public Guid PkEntityId { get; set; }

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

    [Column(TypeName = "decimal(19, 4)")]
    public decimal MinPurchaseQty { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal MaxPurchaseQty { get; set; }

    [Column("AllocationQTY", TypeName = "decimal(19, 4)")]
    public decimal AllocationQty { get; set; }

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

    public bool? Deleted { get; set; }

    [Column("FkEntityTypeID")]
    public Guid? FkEntityTypeId { get; set; }

    public bool Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    public string? GroupName { get; set; }

    [StringLength(30)]
    public string AccountNo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Bank { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string? EntityTypeIndicator { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkClientAccountID")]
    public Guid FkClientAccountId { get; set; }

    public bool? HasFacility { get; set; }

    [Column(TypeName = "money")]
    public decimal Facility { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? EntityReferenceAlias { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SwiftCode { get; set; }

    [StringLength(104)]
    public string? Currency { get; set; }
}


