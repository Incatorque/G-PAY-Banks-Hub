using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityEntitiesList
{
    [Column("PkSupplierClientID")]
    public Guid PkSupplierClientId { get; set; }

    [Column("ClientID")]
    public Guid ClientId { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [Column("SupplierID")]
    public Guid SupplierId { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? SupplierName { get; set; }

    [StringLength(100)]
    public string? EntityType { get; set; }

    public bool? AllocatedFacility { get; set; }

    public bool? ForwardRequisition { get; set; }

    [Column("PPOrder")]
    public int? Pporder { get; set; }

    [Column("PPAmount", TypeName = "decimal(19, 4)")]
    public decimal? Ppamount { get; set; }

    public bool? Drawdown { get; set; }

    public bool? IncludeCategories { get; set; }

    [Column("FkSweepID")]
    public Guid? FkSweepId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierClientRefrence { get; set; }

    public bool? ForwardGuarantee { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? CreditLimit { get; set; }

    public int? PaymentTerms { get; set; }

    public bool? ForwardPrice { get; set; }

    [Column("VCRef")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Vcref { get; set; }

    [StringLength(183)]
    public string? ToAcc { get; set; }

    [StringLength(183)]
    public string? FormAcc { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }
}


