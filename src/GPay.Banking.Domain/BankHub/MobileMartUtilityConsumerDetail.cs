using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("FkMobileMartResponseId", Name = "IX_MobileMartUtilityConsumerDetails_FkMobileMartResponseId")]
public partial class MobileMartUtilityConsumerDetail : Entity<Guid>
{
    [Key]
    [Column("PkMobileMartConsumerDetailId")]
    public override Guid Id { get; protected set; }

    public string Name { get; set; } = null!;

    public string Message { get; set; } = null!;

    public Guid FkMobileMartResponseId { get; set; }

    [ForeignKey("FkMobileMartResponseId")]
    [InverseProperty("MobileMartUtilityConsumerDetails")]
    public virtual MobileMartResponse FkMobileMartResponse { get; set; } = null!;
}


