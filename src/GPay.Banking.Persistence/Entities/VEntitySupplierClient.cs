using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntitySupplierClient
{
    public Guid PkEntityId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Taname { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Vatnumber { get; set; }

    public DateTime? LastSyncDate { get; set; }

    public string? Comments { get; set; }

    public string? ImageLocation { get; set; }

    public string? Description { get; set; }

    public Guid EntityTypeId { get; set; }

    public Guid? SupplierClientTypeId { get; set; }

    public Guid FkStatusId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public bool? Featured { get; set; }

    public Guid? FkEntitySubTypeId { get; set; }

    public Guid FkClientId { get; set; }

    public string? QuickInfo { get; set; }
}
