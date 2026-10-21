using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class MainView
{
    [Column("id")]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Make { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Model { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? RegistrationNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DeviceNumber { get; set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    public bool Active { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Surname { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Comments { get; set; }
}


