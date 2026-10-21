using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ClusterNotification")]
public partial class ClusterNotification : Entity<Guid>
{
    [Key]
    [Column("PkClusterId")]
    public override Guid Id { get; protected set; }

    public Guid? FkGroupId { get; set; }

    public Guid? FkMailtemplateId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Destination { get; set; }

    public bool? Active { get; set; }
}


