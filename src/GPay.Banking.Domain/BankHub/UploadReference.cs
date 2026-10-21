using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class UploadReference : Entity<int>
{
    [Key]
    public override int Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Reference { get; set; } = null!;

    public Guid FkEntityId { get; set; }

    public bool? Active { get; set; }
}


