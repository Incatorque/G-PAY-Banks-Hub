using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class Vehicle
{
    [Column("id")]
    public int Id { get; set; }

    public Guid FkEntityId { get; set; }

    public int? FkDeviceId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Make { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    public int Capacity { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? RegistrationNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string VinNo { get; set; } = null!;

    [StringLength(20)]
    public string? TransactionsPerDay { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comments { get; set; }

    public bool? Active { get; set; }
}


