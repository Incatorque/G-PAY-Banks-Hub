using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class Driver : Entity<int>
{
    [Key]
    [Column("id")]
    public override int Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Surname { get; set; }

    [StringLength(13)]
    [Unicode(false)]
    public string? IdNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? MobileNumber { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? LicenseNumber { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comments { get; set; }

    public Guid? FkEntityId { get; set; }

    public int FkVehicleId { get; set; }

    public int? FkDeviceId { get; set; }

    public bool? Active { get; set; }
}


