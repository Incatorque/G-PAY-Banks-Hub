using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class CommunicationType
{
    public Guid PkCommTypeId { get; set; }

    public string Name { get; set; } = null!;

    public Guid? FkMailTemplateId { get; set; }
}
