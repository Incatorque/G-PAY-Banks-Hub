using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserSecurityQuestion
{
    public Guid PkUserSecurityQuestionId { get; set; }

    public string SecurityQuestion { get; set; } = null!;
}
