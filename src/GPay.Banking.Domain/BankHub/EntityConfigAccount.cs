using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityConfigAccount")]
public partial class EntityConfigAccount : Entity<Guid>
{
    [Key]
    [Column("PkEntityConfigAccountID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityConfigID")]
    public Guid? FkEntityConfigId { get; set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    [Column("FkConfigAccountTypeID")]
    public Guid? FkConfigAccountTypeId { get; set; }

    [ForeignKey("FkAccountId")]
    [InverseProperty("EntityConfigAccounts")]
    public virtual Account? FkAccount { get; set; }

    [ForeignKey("FkConfigAccountTypeId")]
    [InverseProperty("EntityConfigAccounts")]
    public virtual ConfigAccountType? FkConfigAccountType { get; set; }

    [ForeignKey("FkEntityConfigId")]
    [InverseProperty("EntityConfigAccounts")]
    public virtual EntityConfig? FkEntityConfig { get; set; }
}


