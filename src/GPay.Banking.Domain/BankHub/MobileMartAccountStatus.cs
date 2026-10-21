using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("MobileMartAccountStatus")]
[Index("FkMobileMartResponseId", Name = "IX_MobileMartAccountStatus_FkMobileMartResponseId")]
public partial class MobileMartAccountStatus : Entity<Guid>
{
    [Key]
    [Column("PkMobileMartAccountStatusId")]
    public override Guid Id { get; protected set; }

    public string Reason { get; set; } = null!;

    public int? Amount { get; set; }

    public int? TaxAmount { get; set; }

    public int? Outstanding { get; set; }

    public string Tariff { get; set; } = null!;

    public Guid FkMobileMartResponseId { get; set; }

    [ForeignKey("FkMobileMartResponseId")]
    [InverseProperty("MobileMartAccountStatuses")]
    public virtual MobileMartResponse FkMobileMartResponse { get; set; } = null!;
}


