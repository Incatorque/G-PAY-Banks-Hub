using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Menu1
{
    public int MenuId { get; set; }

    public int ParentMenuId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string Url { get; set; } = null!;
}
