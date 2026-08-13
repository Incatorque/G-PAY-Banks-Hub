using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAccount
{
    public Guid PkEntityAccountId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkAccountId { get; set; }

    public decimal? TransactionCost { get; set; }

    public bool? Active { get; set; }

    public string ServiceType { get; set; } = null!;

    public bool? DefaultToThisEntity { get; set; }

    public decimal Facility { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual ICollection<EntityAccountCharge> EntityAccountCharges { get; set; } = new List<EntityAccountCharge>();

    public virtual Account FkAccount { get; set; } = null!;

    public virtual Entity FkEntity { get; set; } = null!;
}
