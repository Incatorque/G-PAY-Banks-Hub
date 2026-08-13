using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class SupplierClient
{
    public Guid PkSupplierClientId { get; set; }

    public Guid FkSupplierId { get; set; }

    public Guid FkClientId { get; set; }

    public string? SupplierClientRefrence { get; set; }

    public bool? ForwardGuarantee { get; set; }

    public decimal? CreditLimit { get; set; }

    public int? PaymentTerms { get; set; }

    public Guid? FkSupplierBankInfoId { get; set; }

    public Guid? FkClientBankInfoId { get; set; }

    public Guid? FkEntityTypeId { get; set; }

    public bool? ForwardPrice { get; set; }

    public bool? AllocatedFacility { get; set; }

    public bool? ForwardRequisition { get; set; }

    public bool? ExcludeAllGoods { get; set; }

    public DateTime? AllocationDateSet { get; set; }

    public int? Pporder { get; set; }

    public decimal? Ppamount { get; set; }

    public bool? Drawdown { get; set; }

    public bool? IncludeCategories { get; set; }

    public Guid? FkSweepId { get; set; }

    public string? SupplierBankReference { get; set; }

    public Guid? FkAccountId { get; set; }

    public bool? Contribution { get; set; }

    public bool? Markup { get; set; }

    public bool? AutomaticAllocation { get; set; }

    public bool? Active { get; set; }

    public DateTime? DateCreated { get; set; }

    public virtual Entity FkClient { get; set; } = null!;

    public virtual EntityType? FkEntityType { get; set; }

    public virtual Entity FkSupplier { get; set; } = null!;

    public virtual ICollection<ProductExcluded> ProductExcludeds { get; set; } = new List<ProductExcluded>();

    public virtual ICollection<SupplierClientAccount> SupplierClientAccounts { get; set; } = new List<SupplierClientAccount>();
}
