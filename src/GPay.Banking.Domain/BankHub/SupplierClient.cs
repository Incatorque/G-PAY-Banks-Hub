using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("SupplierClient")]
[Index("FkEntityTypeId", Name = "NCIDX_SupplierClient_PkSupplierClientID_FkSupplierID_FkClient_SupplierClientRef_Others")]
[Index("FkSupplierId", "FkClientId", Name = "ncidx_SupplierClient_SupplierID_ClientID_AccountID")]
public partial class SupplierClient : Entity<Guid>
{
    [Key]
    [Column("PkSupplierClientID")]
    public override Guid Id { get; protected set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierClientRefrence { get; set; }

    public bool? ForwardGuarantee { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? CreditLimit { get; set; }

    public int? PaymentTerms { get; set; }

    [Column("FkSupplierBankInfoID")]
    public Guid? FkSupplierBankInfoId { get; set; }

    [Column("FkClientBankInfoID")]
    public Guid? FkClientBankInfoId { get; set; }

    [Column("FkEntityTypeID")]
    public Guid? FkEntityTypeId { get; set; }

    public bool? ForwardPrice { get; set; }

    public bool? AllocatedFacility { get; set; }

    public bool? ForwardRequisition { get; set; }

    public bool? ExcludeAllGoods { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? AllocationDateSet { get; set; }

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
    public string? SupplierBankReference { get; set; }

    public Guid? FkAccountId { get; set; }

    public bool? Contribution { get; set; }

    public bool? Markup { get; set; }

    public bool? AutomaticAllocation { get; set; }

    public bool? Active { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [ForeignKey("FkClientId")]
    [InverseProperty("SupplierClientFkClients")]
    public virtual CustomEntity FkClient { get; set; } = null!;

    [ForeignKey("FkEntityTypeId")]
    [InverseProperty("SupplierClients")]
    public virtual EntityType? FkEntityType { get; set; }

    [ForeignKey("FkSupplierId")]
    [InverseProperty("SupplierClientFkSuppliers")]
    public virtual CustomEntity FkSupplier { get; set; } = null!;

    [InverseProperty("FkSupplierClient")]
    public virtual ICollection<ProductExcluded> ProductExcludeds { get; set; } = new List<ProductExcluded>();

    [InverseProperty("FkSupplierClient")]
    public virtual ICollection<SupplierClientAccount> SupplierClientAccounts { get; set; } = new List<SupplierClientAccount>();
}


