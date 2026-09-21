using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MobileMartProductsCategory
{
    public Guid PkMobileMartProductsCategoryId { get; set; }

    public string MobileMartProductsCategoryDescription { get; set; } = null!;

    public bool? IsPinned { get; set; }
}
