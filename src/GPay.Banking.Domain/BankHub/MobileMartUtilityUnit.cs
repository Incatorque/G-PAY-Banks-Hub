using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("FkMobileMartUtilityTokenId", Name = "IX_MobileMartUtilityUnits_FkMobileMartUtilityTokenId")]
public partial class MobileMartUtilityUnit : Entity<Guid>
{
    [Key]
    [Column("PkMobileMartUtilityUnitId")]
    public override Guid Id { get; protected set; }

    public int? Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public bool? Free { get; set; }

    public string Tariff { get; set; } = null!;

    public Guid FkMobileMartUtilityTokenId { get; set; }

    [ForeignKey("FkMobileMartUtilityTokenId")]
    [InverseProperty("MobileMartUtilityUnits")]
    public virtual MobileMartUtilityToken FkMobileMartUtilityToken { get; set; } = null!;
}


