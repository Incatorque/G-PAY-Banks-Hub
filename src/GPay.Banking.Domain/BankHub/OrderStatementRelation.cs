using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderStatementRelation")]
public partial class OrderStatementRelation : Entity<Guid>
{
    [Key]
    [Column("PkOrderStatementId")]
    public override Guid Id { get; protected set; }

    public Guid FkOrderId { get; set; }

    public Guid? FkClientStatementId { get; set; }

    public Guid? FkSupplierStatementId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedDate { get; set; }

    public bool? IsActive { get; set; }
}


