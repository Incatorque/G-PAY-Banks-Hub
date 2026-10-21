using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Grid")]
public partial class Grid : Entity<Guid>
{
    [Key]
    [Column("PkGridID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    public bool? Active { get; set; }

    [InverseProperty("FkGrid")]
    public virtual ICollection<BaseProduct> BaseProducts { get; set; } = new List<BaseProduct>();

    [InverseProperty("FkGrid")]
    public virtual ICollection<GridZone> GridZones { get; set; } = new List<GridZone>();
}


