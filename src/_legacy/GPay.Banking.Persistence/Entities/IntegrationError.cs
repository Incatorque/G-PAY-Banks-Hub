using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class IntegrationError
{
    public Guid PkIntegrationErrorId { get; set; }

    public string ErrorMessage { get; set; } = null!;

    public DateTime EntryDateTime { get; set; }
}
