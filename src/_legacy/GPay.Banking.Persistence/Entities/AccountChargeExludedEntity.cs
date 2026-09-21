using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AccountChargeExludedEntity
{
    public Guid PkAccountChargeExludedEntities { get; set; }

    public Guid FkAccountChargeId { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid FkentityId { get; set; }

    public virtual AccountCharge FkAccountCharge { get; set; } = null!;

    public virtual Entity Fkentity { get; set; } = null!;
}
