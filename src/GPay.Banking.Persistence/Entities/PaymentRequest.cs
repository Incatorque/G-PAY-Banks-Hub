using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class PaymentRequest
{
    public Guid Id { get; set; }

    public string Msisdn { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Reference { get; set; } = null!;

    public int IntegratedSystemId { get; set; }

    public Guid OriginUserId { get; set; }

    public DateTime DateOfRequest { get; set; }

    public bool? Completed { get; set; }

    public string Reason { get; set; } = null!;

    public virtual IntegratedSystem IntegratedSystem { get; set; } = null!;

    public virtual User OriginUser { get; set; } = null!;
}
