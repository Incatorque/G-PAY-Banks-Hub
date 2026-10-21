using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("IntegratedSystem")]
public partial class IntegratedSystem : Entity<int>
{
    [Key]
    [Column("PkSystemID")]
    public override int Id { get; protected set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    public bool? Active { get; set; }

    [StringLength(255)]
    public string Origin { get; set; } = null!;

    [Column("IsAPI")]
    public bool? IsApi { get; set; }

    [Column("FkEntityConfigID")]
    public Guid? FkEntityConfigId { get; set; }

    public bool? IsNative { get; set; }

    [ForeignKey("FkEntityConfigId")]
    [InverseProperty("IntegratedSystems")]
    public virtual EntityConfig? FkEntityConfig { get; set; }

    [InverseProperty("IntegratedSystem")]
    public virtual ICollection<PaymentRequest> PaymentRequests { get; set; } = new List<PaymentRequest>();

    [InverseProperty("FkSystem")]
    public virtual ICollection<UserPin> UserPins { get; set; } = new List<UserPin>();
}


