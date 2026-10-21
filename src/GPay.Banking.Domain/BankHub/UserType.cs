using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserType")]
public partial class UserType : Entity<Guid>
{
    [Key]
    [Column("pkUserTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [InverseProperty("FkUserType")]
    public virtual ICollection<EtguuserType> EtguuserTypes { get; set; } = new List<EtguuserType>();

    [InverseProperty("FkUserType")]
    public virtual ICollection<UserTypeModule> UserTypeModules { get; set; } = new List<UserTypeModule>();

    [InverseProperty("FkUserType")]
    public virtual ICollection<UserTypeUserType> UserTypeUserTypeFkUserTypes { get; set; } = new List<UserTypeUserType>();

    [InverseProperty("FkUserTypes")]
    public virtual ICollection<UserTypeUserType> UserTypeUserTypeFkUserTypesNavigation { get; set; } = new List<UserTypeUserType>();

    [InverseProperty("FkUserType")]
    public virtual ICollection<UserUserType> UserUserTypes { get; set; } = new List<UserUserType>();
}


