using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class MobileMartProductsCategory : Entity<Guid>
{
    [Key]
    [Column("PkMobileMartProductsCategoryId")]
    public override Guid Id { get; protected set; }

    public string MobileMartProductsCategoryDescription { get; set; } = null!;

    public bool? IsPinned { get; set; }
}


