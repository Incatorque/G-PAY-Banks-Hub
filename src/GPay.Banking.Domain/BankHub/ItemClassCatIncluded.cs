using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ItemClassCatIncluded")]
public partial class ItemClassCatIncluded : Entity<Guid>
{
    [Key]
    [Column("PkCategoryIncludedID")]
    public override Guid Id { get; protected set; }

    [Column("FkClassCategoryID")]
    public Guid FkClassCategoryId { get; set; }

    public bool? Included { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }
}


