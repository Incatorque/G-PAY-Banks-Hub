using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VSupplierClientList
{
    [Column("ClientID")]
    public Guid ClientId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string ClientName { get; set; } = null!;

    [Column("SupplierID")]
    public Guid SupplierId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SupplierName { get; set; } = null!;

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }
}


