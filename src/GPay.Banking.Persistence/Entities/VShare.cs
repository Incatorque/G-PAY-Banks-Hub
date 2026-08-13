using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VShare
{
    public Guid FkOrderId { get; set; }

    public int PkShareId { get; set; }

    public bool? Ceded { get; set; }

    public DateTime? CededDate { get; set; }

    public DateTime OrderDate { get; set; }

    public int OrderNumber { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public decimal? Price { get; set; }

    public decimal? Quantity { get; set; }

    public string? ReferenceNumber { get; set; }
}
