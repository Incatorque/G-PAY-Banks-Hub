using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityConfig")]
[Index("FkEntityIdFees", Name = "IX_EntityConfig_FkEntityIdFees")]
public partial class EntityConfig : Entity<Guid>
{
    [Key]
    [Column("PkEntityConfigID")]
    public override Guid Id { get; protected set; }

    [Column("FkGroupID")]
    public Guid FkGroupId { get; set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkAccountID")]
    public Guid FkAccountId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ReportHeader { get; set; } = null!;

    [Column("HomeURL")]
    [StringLength(50)]
    [Unicode(false)]
    public string HomeUrl { get; set; } = null!;

    [Column("APIRoute")]
    [StringLength(25)]
    [Unicode(false)]
    public string Apiroute { get; set; } = null!;

    [Column("RegistrationURL")]
    [StringLength(50)]
    [Unicode(false)]
    public string RegistrationUrl { get; set; } = null!;

    [Column("SMSAuthToken")]
    [StringLength(255)]
    [Unicode(false)]
    public string? SmsauthToken { get; set; }

    public bool? OpenLoop { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EmailPlaceholder { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string VendorDescr { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ConsumerDescr { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string RepresentativeDescr { get; set; } = null!;

    [Column(TypeName = "decimal(18, 0)")]
    public decimal BlanketOver { get; set; }

    public int? StatementTerm { get; set; }

    public bool? AllowUnallocated { get; set; }

    public bool? AddFees { get; set; }

    public bool? DisableDuplicateCheck { get; set; }

    public bool? DisableRegistrationNotification { get; set; }

    public Guid? FkEntityIdFees { get; set; }

    [InverseProperty("FkEntityConfig")]
    public virtual ICollection<EntityConfigAccount> EntityConfigAccounts { get; set; } = new List<EntityConfigAccount>();

    [ForeignKey("FkAccountId")]
    [InverseProperty("EntityConfigs")]
    public virtual Account FkAccount { get; set; } = null!;

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityConfigFkEntities")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkEntityIdFees")]
    [InverseProperty("EntityConfigFkEntityIdFeesNavigations")]
    public virtual CustomEntity? FkEntityIdFeesNavigation { get; set; }

    [ForeignKey("FkGroupId")]
    [InverseProperty("EntityConfigs")]
    public virtual Group FkGroup { get; set; } = null!;

    [ForeignKey("FkUserId")]
    [InverseProperty("EntityConfigs")]
    public virtual User FkUser { get; set; } = null!;

    [InverseProperty("FkEntityConfig")]
    public virtual ICollection<IntegratedSystem> IntegratedSystems { get; set; } = new List<IntegratedSystem>();
}


