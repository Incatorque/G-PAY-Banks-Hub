using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VDevice
{
    public int Id { get; set; }

    public string DeviceNumber { get; set; } = null!;

    public string DeviceStatus { get; set; } = null!;

    public DateTime? StatusDate { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? Comments { get; set; }

    public string Consumer { get; set; } = null!;

    public string? Cluster { get; set; }

    public bool? Active { get; set; }

    public Guid? FkEntityId { get; set; }

    public int? FkVehicleId { get; set; }

    public Guid? FkConsumerId { get; set; }

    public int? FkDriverId { get; set; }
}
