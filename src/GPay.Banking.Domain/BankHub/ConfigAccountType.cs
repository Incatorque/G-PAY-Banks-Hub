using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ConfigAccountType")]
public partial class ConfigAccountType : Entity<Guid>
{
    [Key]
    [Column("PkConfigAccountTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [InverseProperty("FkConfigAccountType")]
    public virtual ICollection<EntityConfigAccount> EntityConfigAccounts { get; set; } = new List<EntityConfigAccount>();
}


