using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Holiday
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Name { get; set; } = null!;

    public string? Note { get; set; }
}
