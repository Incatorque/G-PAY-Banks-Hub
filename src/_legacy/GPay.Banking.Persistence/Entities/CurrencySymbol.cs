using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class CurrencySymbol
{
    public string? Country { get; set; }

    public string? Currency { get; set; }

    public string? CurrencyCode { get; set; }

    public string? Symbol { get; set; }

    public string? WindowsShortcut { get; set; }

    public string? MacShortcut { get; set; }
}
