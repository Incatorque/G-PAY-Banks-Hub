using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Driver
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Surname { get; set; }

    public string? IdNumber { get; set; }

    public string? MobileNumber { get; set; }

    public string? LicenseNumber { get; set; }

    public string? Comments { get; set; }

    public Guid? FkEntityId { get; set; }

    public int FkVehicleId { get; set; }

    public int? FkDeviceId { get; set; }

    public bool? Active { get; set; }
}
