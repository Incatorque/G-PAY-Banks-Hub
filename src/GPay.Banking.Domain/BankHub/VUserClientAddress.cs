using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUserClientAddress
{
    [Column("ClientID")]
    public Guid ClientId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("PkEntityAddressID")]
    public Guid PkEntityAddressId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Address1 { get; set; } = null!;

    [Column("FkAddressTypeID")]
    public Guid FkAddressTypeId { get; set; }
}


