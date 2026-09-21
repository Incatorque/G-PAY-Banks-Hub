using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityEntitiesList
{
    public Guid PkSupplierClientId { get; set; }

    public Guid ClientId { get; set; }

    public string? ClientName { get; set; }

    public Guid SupplierId { get; set; }

    public string? SupplierName { get; set; }

    public string? EntityType { get; set; }

    public bool? AllocatedFacility { get; set; }

    public bool? ForwardRequisition { get; set; }

    public int? Pporder { get; set; }

    public decimal? Ppamount { get; set; }

    public bool? Drawdown { get; set; }

    public bool? IncludeCategories { get; set; }

    public Guid? FkSweepId { get; set; }

    public string? SupplierClientRefrence { get; set; }

    public bool? ForwardGuarantee { get; set; }

    public decimal? CreditLimit { get; set; }

    public int? PaymentTerms { get; set; }

    public bool? ForwardPrice { get; set; }

    public string? Vcref { get; set; }

    public string? ToAcc { get; set; }

    public string? FormAcc { get; set; }

    public DateTime? DateCreated { get; set; }
}
