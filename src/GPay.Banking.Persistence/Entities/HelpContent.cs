using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class HelpContent
{
    public Guid PkHelpContentId { get; set; }

    public int LegacyId { get; set; }

    public string Title { get; set; } = null!;

    public string BodyText { get; set; } = null!;

    public bool? Active { get; set; }
}
