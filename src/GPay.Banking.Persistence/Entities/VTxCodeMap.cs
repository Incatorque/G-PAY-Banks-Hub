using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VTxCodeMap
{
    public int Id { get; set; }

    public string Term { get; set; } = null!;

    public string TransactionCode { get; set; } = null!;

    public int TransactionCodeId { get; set; }

    public bool? Active { get; set; }
}
