using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPay.Banking.BankHub;

public partial class Menu
{
    public Guid PkMenuItemId { get; set; }

    public string? Url { get; set; }

    public Guid? FkParentMenuItemId { get; set; }

    public int? IOrder { get; set; }

    public string? MenuName { get; set; }

    public bool? SecondLevel { get; set; }

    public virtual Menu? FkParentMenuItem { get; set; }

    public virtual ICollection<Menu> InverseFkParentMenuItem { get; set; } = new List<Menu>();

    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();
}
