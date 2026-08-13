using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class DataSheet
{
    public string? Image { get; set; }

    public string? Code { get; set; }

    public string? ShortName { get; set; }

    public string? Description { get; set; }

    public string? Specification { get; set; }

    public decimal? Size { get; set; }

    public string? Unit { get; set; }

    public decimal? NoInPackage { get; set; }

    public decimal? Price { get; set; }

    public decimal? ProductCategory { get; set; }
}
