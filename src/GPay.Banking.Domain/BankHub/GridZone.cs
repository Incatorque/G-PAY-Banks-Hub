using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("GridZone")]
public partial class GridZone : Entity<Guid>
{
    [Key]
    [Column("PkGridZoneID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [Column("FkGridID")]
    public Guid FkGridId { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkGridZone")]
    public virtual ICollection<BaseProductZone> BaseProductZones { get; set; } = new List<BaseProductZone>();

    [ForeignKey("FkGridId")]
    [InverseProperty("GridZones")]
    public virtual Grid FkGrid { get; set; } = null!;

    [InverseProperty("FkGridZone")]
    public virtual ICollection<MagisterialDistrictGridZone> MagisterialDistrictGridZones { get; set; } = new List<MagisterialDistrictGridZone>();
}


