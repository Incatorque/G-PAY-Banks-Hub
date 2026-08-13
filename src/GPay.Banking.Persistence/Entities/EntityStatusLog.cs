using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityStatusLog
{
    public Guid PkentityStatusLogId { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string UpdatedStatus { get; set; } = null!;

    public bool? Active { get; set; }

    public Guid FkentityId { get; set; }

    public Guid FkuserId { get; set; }

    public virtual Entity Fkentity { get; set; } = null!;

    public virtual User Fkuser { get; set; } = null!;
}
