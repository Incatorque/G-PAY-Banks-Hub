using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class MainView
{
    public int Id { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string? RegistrationNumber { get; set; }

    public string? DeviceNumber { get; set; }

    public Guid FkEntityId { get; set; }

    public bool Active { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Comments { get; set; }
}
