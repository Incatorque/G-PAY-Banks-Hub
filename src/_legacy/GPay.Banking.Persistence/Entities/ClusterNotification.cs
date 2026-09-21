using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ClusterNotification
{
    public Guid PkClusterId { get; set; }

    public Guid? FkGroupId { get; set; }

    public Guid? FkMailtemplateId { get; set; }

    public string? Destination { get; set; }

    public bool? Active { get; set; }
}
