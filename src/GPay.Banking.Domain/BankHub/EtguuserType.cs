using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ETGUUserType")]
public partial class EtguuserType : Entity<Guid>
{
    [Key]
    [Column("PkETGUUserTypeID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityTypeID")]
    public Guid FkEntityTypeId { get; set; }

    [Column("FkGroupID")]
    public Guid? FkGroupId { get; set; }

    [Column("FKUserID")]
    public Guid? FkuserId { get; set; }

    [Column("FkUserTypeID")]
    public Guid FkUserTypeId { get; set; }

    [ForeignKey("FkEntityTypeId")]
    [InverseProperty("EtguuserTypes")]
    public virtual EntityType FkEntityType { get; set; } = null!;

    [ForeignKey("FkGroupId")]
    [InverseProperty("EtguuserTypes")]
    public virtual Group? FkGroup { get; set; }

    [ForeignKey("FkUserTypeId")]
    [InverseProperty("EtguuserTypes")]
    public virtual UserType FkUserType { get; set; } = null!;

    [ForeignKey("FkuserId")]
    [InverseProperty("EtguuserTypes")]
    public virtual User? Fkuser { get; set; }
}


