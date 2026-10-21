using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AddressType")]
public partial class AddressType : Entity<Guid>
{
    [Key]
    [Column("PkAddressTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [InverseProperty("FkAddressType")]
    public virtual ICollection<EntityAddressPending> EntityAddressPendings { get; set; } = new List<EntityAddressPending>();

    [InverseProperty("FkAddressType")]
    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();
}


