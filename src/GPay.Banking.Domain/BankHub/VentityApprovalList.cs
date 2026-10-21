using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VentityApprovalList
{
    [Column("PkEntityID")]
    public Guid PkEntityId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("TAName")]
    [StringLength(50)]
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

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    public bool? Featured { get; set; }

    [Column("FkEntitySubTypeID")]
    public Guid? FkEntitySubTypeId { get; set; }

    [Column("IDNumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Idnumber { get; set; }

    [Column("VATExemption")]
    public bool? Vatexemption { get; set; }

    [Column("FkRequestedUserID")]
    public Guid? FkRequestedUserId { get; set; }

    [Column("VATRegistered")]
    public bool? Vatregistered { get; set; }

    [Column("FkIdentificationTypeID")]
    public Guid? FkIdentificationTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? EntityReferenceAlias { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? RejectReason { get; set; }

    [Column("WBulkPaymentType")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WbulkPaymentType { get; set; }

    [Column("WBulkLimitSgn")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WbulkLimitSgn { get; set; }

    public bool? IsNew { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    public Guid UserToApprove { get; set; }

    public bool? PreviousLevelIsApproved { get; set; }
}


