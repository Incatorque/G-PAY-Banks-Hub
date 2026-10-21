using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("CountryCurrency")]
public partial class CountryCurrency : Entity<int>
{
    [Key]
    public override int Id { get; protected set; }

    public int CountryId { get; set; }

    public int CurrencyId { get; set; }
}


