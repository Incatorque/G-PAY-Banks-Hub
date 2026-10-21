using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountStatus")]
public partial class AccountStatus : Entity<Guid>
{
    [Key]
    [Column("PkAccountStatusID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [InverseProperty("FkAccountStatus")]
    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}


