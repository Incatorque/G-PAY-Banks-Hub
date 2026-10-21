using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityContact")]
[Index("FkEntityId", Name = "NCI_EntityContact_FKEntityID")]
public partial class EntityContact : Entity<Guid>
{
    [Key]
    [Column("PkEntityContactID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkUserID")]
    public Guid? FkUserId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string Initials { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastnName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [Column("FkTitleID")]
    public Guid FkTitleId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Telephone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cellphone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Fax { get; set; }

    [StringLength(550)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("FkEntityAddressID")]
    public Guid? FkEntityAddressId { get; set; }

    public bool? Primaray { get; set; }

    [Column("IDNumber")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Idnumber { get; set; }

    public bool? EmailNotification { get; set; }

    [Column("SMSNotification")]
    public bool? Smsnotification { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityContacts")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkTitleId")]
    [InverseProperty("EntityContacts")]
    public virtual Title FkTitle { get; set; } = null!;

    [ForeignKey("FkUserId")]
    [InverseProperty("EntityContacts")]
    public virtual User? FkUser { get; set; }
}


