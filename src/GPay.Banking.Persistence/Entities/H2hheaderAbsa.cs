using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class H2hheaderAbsa
{
    public int PkSequenceId { get; set; }

    public int? Sequence { get; set; }

    public DateTime SendDate { get; set; }

    public string? ErrorMessage { get; set; }

    public bool? Error { get; set; }

    public bool? IncreaseSequence { get; set; }

    public DateTime? IntAudDate { get; set; }

    public DateTime? FinAudDate { get; set; }

    public int? UserGenerationSequence { get; set; }

    public int? TransactionSequence { get; set; }
}
