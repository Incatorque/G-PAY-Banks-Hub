using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityList
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

    [Column(TypeName = "datetime")]
    public DateTime? LastSyncDate { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comments { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    [StringLength(2500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("FkStatusID")]
    public Guid FkStatusId { get; set; }

    [Column("FkEntityTypeID")]
    public Guid FkEntityTypeId { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string QuickInfo { get; set; } = null!;

    [Column("FkEntitySubTypeID")]
    public Guid? FkEntitySubTypeId { get; set; }

    public string? GroupName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdated { get; set; }

    [Column("IDNumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Idnumber { get; set; }

    [Column("VATExemption")]
    public bool? Vatexemption { get; set; }
}


