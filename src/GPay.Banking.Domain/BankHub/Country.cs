using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Country")]
[Index("CcFips", Name = "idx_cc_fips")]
[Index("CcIso", Name = "idx_cc_iso")]
public partial class Country : Entity<int>
{
    [Key]
    [Column("CountryID")]
    public override int Id { get; protected set; }

    [Column("cc_fips")]
    [StringLength(2)]
    public string CcFips { get; set; } = null!;

    [Column("cc_iso")]
    [StringLength(2)]
    public string CcIso { get; set; } = null!;

    [Column("tld")]
    [StringLength(3)]
    public string Tld { get; set; } = null!;

    [StringLength(100)]
    public string CountryName { get; set; } = null!;

    [InverseProperty("Country")]
    public virtual ICollection<CountryBank> CountryBanks { get; set; } = new List<CountryBank>();
}


