using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VuserSecurity
{
    public Guid PkUserSecurityQuestionId { get; set; }

    public Guid? PkUserSercurityAnswerId { get; set; }

    public string SecurityQuestion { get; set; } = null!;

    public string? Answer { get; set; }

    public Guid? FkUserId { get; set; }
}
