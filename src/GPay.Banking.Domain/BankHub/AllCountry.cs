using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("CcFips", Name = "UQ_Country_ISO2", IsUnique = true)]
[Index("CcIso", Name = "UQ_Country_ISO3", IsUnique = true)]
[Index("Isonumeric", Name = "UQ_Country_ISONumeric", IsUnique = true)]
public partial class AllCountry : Entity<int>
{
    [Key]
    [Column("CountryID")]
    public override int Id { get; protected set; }

    [Column("cc_fips")]
    [StringLength(2)]
    [Unicode(false)]
    public string CcFips { get; set; } = null!;

    [Column("cc_iso")]
    [StringLength(3)]
    [Unicode(false)]
    public string CcIso { get; set; } = null!;

    [Column("ISONumeric")]
    [StringLength(3)]
    [Unicode(false)]
    public string Isonumeric { get; set; } = null!;

    [StringLength(150)]
    public string CountryName { get; set; } = null!;

    [Column("TLD")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Tld { get; set; }
}


