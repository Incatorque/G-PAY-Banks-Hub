using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserGroup")]
public partial class UserGroup : Entity<Guid>
{
    [Key]
    [Column("PkUserGroupID")]
    public override Guid Id { get; protected set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkGroupID")]
    public Guid FkGroupId { get; set; }

    [ForeignKey("FkGroupId")]
    [InverseProperty("UserGroups")]
    public virtual Group FkGroup { get; set; } = null!;

    [ForeignKey("FkUserId")]
    [InverseProperty("UserGroups")]
    public virtual User FkUser { get; set; } = null!;
}


