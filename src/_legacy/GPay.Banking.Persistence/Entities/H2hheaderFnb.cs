using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class H2hheaderFnb
{
    public int PkSequenceId { get; set; }

    public int? FileSequence { get; set; }

    public int? HeaderSequence { get; set; }

    public int? TransactionSequence { get; set; }

    public DateTime? SendDate { get; set; }

    public string? ErrorMessage { get; set; }

    public string? UserCode { get; set; }

    public string? AmountHash { get; set; }

    public string? AccountHash { get; set; }
}
