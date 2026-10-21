using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class AccountChargeExludedEntity : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeExludedEntities")]
    public override Guid Id { get; protected set; }

    [Column("FkAccountChargeID")]
    public Guid FkAccountChargeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [Column("FKEntityID")]
    public Guid FkentityId { get; set; }

    [ForeignKey("FkAccountChargeId")]
    [InverseProperty("AccountChargeExludedEntities")]
    public virtual AccountCharge FkAccountCharge { get; set; } = null!;

    [ForeignKey("FkentityId")]
    [InverseProperty("AccountChargeExludedEntities")]
    public virtual CustomEntity Fkentity { get; set; } = null!;
}


