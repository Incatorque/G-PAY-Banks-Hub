using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityAddress")]
public partial class EntityAddress : Entity<Guid>
{
    [Key]
    [Column("PkEntityAddressID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkProvinceID")]
    public Guid FkProvinceId { get; set; }

    [Column("FkTownID")]
    public Guid? FkTownId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Address1 { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Address2 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Address3 { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Code { get; set; }

    [Column("FkAddressTypeID")]
    public Guid FkAddressTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Telephone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Fax { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cellphone { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    public bool? Primaray { get; set; }

    [Column("FkMagisterialDistrictID")]
    public Guid? FkMagisterialDistrictId { get; set; }

    [Column("WCode")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Wcode { get; set; }

    [Column("WSiteName")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WsiteName { get; set; }

    [Column("WCostStructPrefix")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WcostStructPrefix { get; set; }

    [Column("WCostStructItem")]
    [StringLength(50)]
    [Unicode(false)]
    public string? WcostStructItem { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AdrReference { get; set; }

    public bool? Active { get; set; }

    [Column("FkCountryID")]
    public int? FkCountryId { get; set; }

    [ForeignKey("FkAddressTypeId")]
    [InverseProperty("EntityAddresses")]
    public virtual AddressType FkAddressType { get; set; } = null!;

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityAddresses")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkMagisterialDistrictId")]
    [InverseProperty("EntityAddresses")]
    public virtual MagisterialDistrict? FkMagisterialDistrict { get; set; }

    [ForeignKey("FkProvinceId")]
    [InverseProperty("EntityAddresses")]
    public virtual Province FkProvince { get; set; } = null!;

    [ForeignKey("FkTownId")]
    [InverseProperty("EntityAddresses")]
    public virtual Town? FkTown { get; set; }

    [InverseProperty("FkClientAddress")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("FkAddress")]
    public virtual ICollection<UserSelection> UserSelections { get; set; } = new List<UserSelection>();
}


