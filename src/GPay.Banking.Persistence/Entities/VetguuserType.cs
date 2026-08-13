using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VetguuserType
{
    public Guid PkEtguuserTypeId { get; set; }

    public Guid FkEntityTypeId { get; set; }

    public string EntityTypeName { get; set; } = null!;

    public Guid? FkGroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public Guid? FkuserId { get; set; }

    public string? UserName { get; set; }

    public Guid FkUserTypeId { get; set; }

    public string UserTypeName { get; set; } = null!;
}
