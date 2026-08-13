using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class BaseProduct
{
    public Guid PkBaseProductId { get; set; }

    public string? Code { get; set; }

    public string? Description { get; set; }

    public Guid? FkGridId { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<BaseProductPrice> BaseProductPrices { get; set; } = new List<BaseProductPrice>();

    public virtual Grid? FkGrid { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
