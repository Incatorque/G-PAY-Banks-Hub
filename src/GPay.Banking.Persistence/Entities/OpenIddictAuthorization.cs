using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OpenIddictAuthorization
{
    public Guid Id { get; set; }

    public Guid? ApplicationId { get; set; }

    public DateTime? CreationDate { get; set; }

    public string? Properties { get; set; }

    public string? Scopes { get; set; }

    public string? Status { get; set; }

    public string? Subject { get; set; }

    public string? Type { get; set; }

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public virtual OpenIddictApplication? Application { get; set; }

    public virtual ICollection<OpenIddictToken> OpenIddictTokens { get; set; } = new List<OpenIddictToken>();
}
