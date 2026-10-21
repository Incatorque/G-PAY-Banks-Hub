using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("FkMobileMartResponseId", Name = "IX_MobileMartUtilityMunicipalityDetails_FkMobileMartResponseId")]
public partial class MobileMartUtilityMunicipalityDetail : Entity<Guid>
{
    [Key]
    [Column("PkMobileMartUtilityMunicipalityDetailId")]
    public override Guid Id { get; protected set; }

    public string Municipality { get; set; } = null!;

    public string MunicipalityVatNumber { get; set; } = null!;

    public Guid FkMobileMartResponseId { get; set; }

    [ForeignKey("FkMobileMartResponseId")]
    [InverseProperty("MobileMartUtilityMunicipalityDetails")]
    public virtual MobileMartResponse FkMobileMartResponse { get; set; } = null!;
}


