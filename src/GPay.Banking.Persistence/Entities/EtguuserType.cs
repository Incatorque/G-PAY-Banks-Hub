using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EtguuserType
{
    public Guid PkEtguuserTypeId { get; set; }

    public Guid FkEntityTypeId { get; set; }

    public Guid? FkGroupId { get; set; }

    public Guid? FkuserId { get; set; }

    public Guid FkUserTypeId { get; set; }

    public virtual EntityType FkEntityType { get; set; } = null!;

    public virtual Group? FkGroup { get; set; }

    public virtual UserType FkUserType { get; set; } = null!;

    public virtual User? Fkuser { get; set; }
}
