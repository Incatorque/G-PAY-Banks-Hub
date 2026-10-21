using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityPending")]
public partial class EntityPending : Entity<Guid>
{
    [Key]
    [Column("PkEntityID")]
    public override Guid Id { get; protected set; }

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

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityAddressPending> EntityAddressPendings { get; set; } = new List<EntityAddressPending>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityBankInfoPending> EntityBankInfoPendings { get; set; } = new List<EntityBankInfoPending>();

    [InverseProperty("FkEntity")]
    public virtual ICollection<EntityContactPending> EntityContactPendings { get; set; } = new List<EntityContactPending>();

    [ForeignKey("FkEntitySubTypeId")]
    [InverseProperty("EntityPendings")]
    public virtual EntitySubType? FkEntitySubType { get; set; }

    [ForeignKey("FkRequestedUserId")]
    [InverseProperty("EntityPendings")]
    public virtual User? FkRequestedUser { get; set; }

    [ForeignKey("FkStatusId")]
    [InverseProperty("EntityPendings")]
    public virtual EntityStatus FkStatus { get; set; } = null!;
}


