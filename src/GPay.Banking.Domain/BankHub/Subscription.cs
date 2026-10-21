using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class Subscription : Entity<Guid>
{
    [Key]
    [Column("PkSubscriptionId")]
    public override Guid Id { get; protected set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkMailtemplateId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Destination { get; set; }

    public bool? Active { get; set; }
}


