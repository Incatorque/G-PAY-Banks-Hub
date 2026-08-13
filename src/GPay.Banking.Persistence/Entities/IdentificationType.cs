using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class IdentificationType
{
    public Guid PkIdentificationTypeId { get; set; }

    public string IdentificationTypeName { get; set; } = null!;
}
