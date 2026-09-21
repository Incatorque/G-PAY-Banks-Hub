using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Title
{
    public Guid PkTitleId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<EntityContactPending> EntityContactPendings { get; set; } = new List<EntityContactPending>();

    public virtual ICollection<EntityContact> EntityContacts { get; set; } = new List<EntityContact>();
}
