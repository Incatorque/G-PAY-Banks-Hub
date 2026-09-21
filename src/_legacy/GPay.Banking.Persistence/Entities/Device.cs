using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Device
{
    public int Id { get; set; }

    public Guid? FkEntityId { get; set; }

    public int? FkVehicleId { get; set; }

    public int? FkDriverId { get; set; }

    public string DeviceNumber { get; set; } = null!;

    public bool? DeviceStatus { get; set; }

    public Guid? FkConsumerId { get; set; }

    public DateTime? StatusDate { get; set; }

    public DateTime? IssueDate { get; set; }

    public string? Comments { get; set; }

    public bool? Active { get; set; }
}
