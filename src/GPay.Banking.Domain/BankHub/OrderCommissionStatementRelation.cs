using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderCommissionStatementRelation")]
public partial class OrderCommissionStatementRelation : Entity<Guid>
{
    [Key]
    [Column("PkOrderCommissionStatementId")]
    public override Guid Id { get; protected set; }

    public Guid FkOrderId { get; set; }

    public Guid FkCommissionStatementid { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public bool? IsActive { get; set; }
}


