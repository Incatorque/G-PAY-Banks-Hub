using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserSecurityAnswer
{
    public Guid PkUserSercurityAnswerId { get; set; }

    public Guid FkUserId { get; set; }

    public string Answer { get; set; } = null!;

    public Guid FkUserSecurityQuestionsId { get; set; }

    public virtual User FkUser { get; set; } = null!;
}
