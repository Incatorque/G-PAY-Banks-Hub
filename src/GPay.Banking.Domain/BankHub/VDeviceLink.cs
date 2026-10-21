using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VDeviceLink
{
    [Column("id")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DeviceNumber { get; set; } = null!;

    [StringLength(8)]
    [Unicode(false)]
    public string DeviceStatus { get; set; } = null!;

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    public int? FkVehicleId { get; set; }

    [Column("FkConsumerID")]
    public Guid? FkConsumerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StatusDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? IssueDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Make { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Model { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? RegistrationNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Surname { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comments { get; set; }

    public bool? Active { get; set; }
}


