using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VDeviceLink
{
    public int Id { get; set; }

    public string DeviceNumber { get; set; } = null!;

    public string DeviceStatus { get; set; } = null!;

    public Guid? FkEntityId { get; set; }

    public int? FkVehicleId { get; set; }

    public Guid? FkConsumerId { get; set; }

    public DateTime? StatusDate { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? Make { get; set; }

    public string? Model { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Comments { get; set; }

    public bool? Active { get; set; }
}
