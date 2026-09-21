using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Currency
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string? Country { get; set; }
}
