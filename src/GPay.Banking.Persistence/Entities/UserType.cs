using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserType
{
    public Guid PkUserTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<EtguuserType> EtguuserTypes { get; set; } = new List<EtguuserType>();

    public virtual ICollection<UserTypeModule> UserTypeModules { get; set; } = new List<UserTypeModule>();

    public virtual ICollection<UserTypeUserType> UserTypeUserTypeFkUserTypes { get; set; } = new List<UserTypeUserType>();

    public virtual ICollection<UserTypeUserType> UserTypeUserTypeFkUserTypesNavigation { get; set; } = new List<UserTypeUserType>();

    public virtual ICollection<UserUserType> UserUserTypes { get; set; } = new List<UserUserType>();
}
