using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Vehicle
{
    public int Id { get; set; }

    public Guid FkEntityId { get; set; }

    public int? FkDeviceId { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Capacity { get; set; }

    public string? RegistrationNumber { get; set; }

    public string VinNo { get; set; } = null!;

    public string? TransactionsPerDay { get; set; }

    public string? Comments { get; set; }

    public bool? Active { get; set; }
}
