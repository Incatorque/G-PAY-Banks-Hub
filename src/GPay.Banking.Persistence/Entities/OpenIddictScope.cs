using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OpenIddictScope
{
    public Guid Id { get; set; }

    public string? Description { get; set; }

    public string? Descriptions { get; set; }

    public string? DisplayName { get; set; }

    public string? DisplayNames { get; set; }

    public string? Name { get; set; }

    public string? Properties { get; set; }

    public string? Resources { get; set; }

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public Guid? LastModifierId { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? DeleterId { get; set; }

    public DateTime? DeletionTime { get; set; }
}
