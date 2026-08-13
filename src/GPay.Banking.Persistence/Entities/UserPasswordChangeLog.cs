using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserPasswordChangeLog
{
    public Guid PkuserPasswordChangeLogId { get; set; }

    public DateTime UpdatedDate { get; set; }

    public Guid FkuserId { get; set; }

    public virtual User Fkuser { get; set; } = null!;
}
