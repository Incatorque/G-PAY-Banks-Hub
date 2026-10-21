using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityType")]
public partial class EntityType : Entity<Guid>
{
    [Key]
    [Column("PkEntityTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? DefaultLink { get; set; }

    [InverseProperty("FkEntityType")]
    public virtual ICollection<EntityEntityType> EntityEntityTypes { get; set; } = new List<EntityEntityType>();

    [InverseProperty("FkEntityType")]
    public virtual ICollection<EtguuserType> EtguuserTypes { get; set; } = new List<EtguuserType>();

    [InverseProperty("FkEntityType")]
    public virtual ICollection<SupplierClient> SupplierClients { get; set; } = new List<SupplierClient>();
}


