using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityClientList
{
    public Guid PkEntityId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Taname { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Vatnumber { get; set; }

    public DateTime? LastSyncDate { get; set; }

    public string? Comments { get; set; }

    public string? Description { get; set; }

    public Guid FkStatusId { get; set; }

    public Guid FkEntityTypeId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public string Status { get; set; } = null!;

    public string? ImageLocation { get; set; }

    public string? GroupName { get; set; }
}
