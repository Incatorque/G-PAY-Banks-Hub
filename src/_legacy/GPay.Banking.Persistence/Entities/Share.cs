using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Share
{
    public int PkShareId { get; set; }

    public Guid FkOrderId { get; set; }

    public bool? Ceded { get; set; }

    public DateTime? CededDate { get; set; }
}
