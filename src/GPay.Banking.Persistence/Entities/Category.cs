using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Category
{
    public Guid PkCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
