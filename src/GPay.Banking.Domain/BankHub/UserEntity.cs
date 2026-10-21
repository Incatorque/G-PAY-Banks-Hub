using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserEntity")]
public partial class UserEntity : Entity<Guid>
{
    [Key]
    [Column("PkUserEntityID")]
    public override Guid Id { get; protected set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [ForeignKey("FkEntityId")]
    [InverseProperty("UserEntities")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkUserId")]
    [InverseProperty("UserEntities")]
    public virtual User FkUser { get; set; } = null!;
}


