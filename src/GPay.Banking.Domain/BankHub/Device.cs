using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class Device : Entity<int>
{
    [Key]
    [Column("id")]
    public override int Id { get; protected set; }

    public Guid? FkEntityId { get; set; }

    public int? FkVehicleId { get; set; }

    public int? FkDriverId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string DeviceNumber { get; set; } = null!;

    public bool? DeviceStatus { get; set; }

    public Guid? FkConsumerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? StatusDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? IssueDate { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comments { get; set; }

    public bool? Active { get; set; }
}


