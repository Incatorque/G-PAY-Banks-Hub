using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityAccount")]
public partial class EntityAccount : Entity<Guid>
{
    [Key]
    [Column("PkEntityAccountID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkAccountID")]
    public Guid FkAccountId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? TransactionCost { get; set; }

    public bool? Active { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string ServiceType { get; set; } = null!;

    public bool? DefaultToThisEntity { get; set; }

    [Column(TypeName = "money")]
    public decimal Facility { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [InverseProperty("FkEntityAccount")]
    public virtual ICollection<EntityAccountCharge> EntityAccountCharges { get; set; } = new List<EntityAccountCharge>();

    [ForeignKey("FkAccountId")]
    [InverseProperty("EntityAccounts")]
    public virtual Account FkAccount { get; set; } = null!;

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityAccounts")]
    public virtual CustomEntity FkEntity { get; set; } = null!;
}


