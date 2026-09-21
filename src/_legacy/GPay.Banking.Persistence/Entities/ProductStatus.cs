using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ProductStatus
{
    public Guid PkStatusId { get; set; }

    public string Name { get; set; } = null!;
}
