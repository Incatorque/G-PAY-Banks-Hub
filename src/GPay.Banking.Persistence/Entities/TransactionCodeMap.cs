using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class TransactionCodeMap
{
    public int Id { get; set; }

    public string Term { get; set; } = null!;

    public int TransactionCodeId { get; set; }

    public DateTime DateCreated { get; set; }

    public bool? Active { get; set; }

    public Guid? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual TransactionCode TransactionCode { get; set; } = null!;
}
