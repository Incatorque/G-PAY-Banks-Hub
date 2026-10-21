using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityStatusLog")]
public partial class EntityStatusLog : Entity<Guid>
{
    [Key]
    [Column("PKEntityStatusLogId")]
    public override Guid Id { get; protected set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string UpdatedStatus { get; set; } = null!;

    public bool? Active { get; set; }

    [Column("FKEntityId")]
    public Guid FkentityId { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [ForeignKey("FkentityId")]
    [InverseProperty("EntityStatusLogs")]
    public virtual CustomEntity Fkentity { get; set; } = null!;

    [ForeignKey("FkuserId")]
    [InverseProperty("EntityStatusLogs")]
    public virtual User Fkuser { get; set; } = null!;
}


