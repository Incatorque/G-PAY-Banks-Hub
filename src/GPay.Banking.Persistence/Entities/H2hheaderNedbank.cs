using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class H2hheaderNedbank
{
    public int PkSequenceId { get; set; }

    public int FileSequence { get; set; }

    public int HeaderSequence { get; set; }

    public long TransactionSequence { get; set; }

    public DateTime SendDate { get; set; }

    public string? ErrorMessage { get; set; }

    public string ClientCode { get; set; } = null!;
}
