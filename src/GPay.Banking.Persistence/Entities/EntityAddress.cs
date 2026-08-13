using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityAddress
{
    public Guid PkEntityAddressId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkProvinceId { get; set; }

    public Guid? FkTownId { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Code { get; set; }

    public Guid FkAddressTypeId { get; set; }

    public string? Telephone { get; set; }

    public string? Fax { get; set; }

    public string? Cellphone { get; set; }

    public string? Email { get; set; }

    public bool? Primaray { get; set; }

    public Guid? FkMagisterialDistrictId { get; set; }

    public string? Wcode { get; set; }

    public string? WsiteName { get; set; }

    public string? WcostStructPrefix { get; set; }

    public string? WcostStructItem { get; set; }

    public string? AdrReference { get; set; }

    public bool? Active { get; set; }

    public int? FkCountryId { get; set; }

    public virtual AddressType FkAddressType { get; set; } = null!;

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual MagisterialDistrict? FkMagisterialDistrict { get; set; }

    public virtual Province FkProvince { get; set; } = null!;

    public virtual Town? FkTown { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<UserSelection> UserSelections { get; set; } = new List<UserSelection>();
}
