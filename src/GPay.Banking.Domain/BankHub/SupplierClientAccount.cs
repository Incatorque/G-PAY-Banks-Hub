using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("SupplierClientAccount")]
public partial class SupplierClientAccount : Entity<Guid>
{
    [Key]
    [Column("PkSupplierClientAccountID")]
    public override Guid Id { get; protected set; }

    [Column("FkClientAccountID")]
    public Guid FkClientAccountId { get; set; }

    [Column("FkSupplierAccountID")]
    public Guid FkSupplierAccountId { get; set; }

    [Column("FkSupplierClientID")]
    public Guid FkSupplierClientId { get; set; }

    [ForeignKey("FkClientAccountId")]
    [InverseProperty("SupplierClientAccountFkClientAccounts")]
    public virtual Account FkClientAccount { get; set; } = null!;

    [ForeignKey("FkSupplierAccountId")]
    [InverseProperty("SupplierClientAccountFkSupplierAccounts")]
    public virtual Account FkSupplierAccount { get; set; } = null!;

    [ForeignKey("FkSupplierClientId")]
    [InverseProperty("SupplierClientAccounts")]
    public virtual SupplierClient FkSupplierClient { get; set; } = null!;
}


