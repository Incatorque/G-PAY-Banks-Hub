using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class IsopainMessagesAbsa
{
    public int PkisoPainMessageId { get; set; }

    public string MessageId { get; set; } = null!;

    public DateTime MessageDate { get; set; }

    public bool? Pending { get; set; }

    public bool? Accepted { get; set; }

    public DateTime? AcceptedDate { get; set; }

    public bool? Rejected { get; set; }

    public DateTime? RejectedDate { get; set; }

    public string? ErrorMessage { get; set; }
}
