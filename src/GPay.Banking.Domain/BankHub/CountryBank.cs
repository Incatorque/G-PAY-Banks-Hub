using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("CountryBank")]
public partial class CountryBank : Entity<int>
{
    [Key]
    [Column("CountryBankId")]
    public override int Id { get; protected set; }

    public Guid? BankId { get; set; }

    public int? CountryId { get; set; }

    [ForeignKey("BankId")]
    [InverseProperty("CountryBanks")]
    public virtual Bank? Bank { get; set; }

    [ForeignKey("CountryId")]
    [InverseProperty("CountryBanks")]
    public virtual Country? Country { get; set; }
}


