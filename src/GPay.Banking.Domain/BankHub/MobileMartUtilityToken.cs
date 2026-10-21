using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("FkMobileMartResponseId", Name = "IX_MobileMartUtilityTokens_FkMobileMartResponseId")]
public partial class MobileMartUtilityToken : Entity<Guid>
{
    [Key]
    [Column("PkMobileMartUtilityTokenId")]
    public override Guid Id { get; protected set; }

    public string Type { get; set; } = null!;

    public string Token { get; set; } = null!;

    public int? Amount { get; set; }

    public int? Tax { get; set; }

    public Guid FkMobileMartResponseId { get; set; }

    [ForeignKey("FkMobileMartResponseId")]
    [InverseProperty("MobileMartUtilityTokens")]
    public virtual MobileMartResponse FkMobileMartResponse { get; set; } = null!;

    [InverseProperty("FkMobileMartUtilityToken")]
    public virtual ICollection<MobileMartUtilityUnit> MobileMartUtilityUnits { get; set; } = new List<MobileMartUtilityUnit>();
}


