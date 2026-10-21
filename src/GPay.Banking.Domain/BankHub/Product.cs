using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Product")]
public partial class Product : Entity<Guid>
{
    [Key]
    [Column("PkProductID")]
    public override Guid Id { get; protected set; }

    [Column("FkCategoryID")]
    public Guid? FkCategoryId { get; set; }

    [Column("FkBaseProductID")]
    public Guid? FkBaseProductId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [StringLength(2500)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [StringLength(2500)]
    [Unicode(false)]
    public string? Specification { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Size { get; set; }

    public int? NumberInPackage { get; set; }

    [Column("FkUnitID")]
    public Guid? FkUnitId { get; set; }

    public bool? Active { get; set; }

    [Column("VATIncl")]
    public bool? Vatincl { get; set; }

    [Column("VATRule")]
    public bool? Vatrule { get; set; }

    public bool? AlwaysCustomPrice { get; set; }

    public bool? CustomPriceLess { get; set; }

    public bool? CustomPriceMore { get; set; }

    public int? RoundingDecimals { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MinOrder { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MaxOrder { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? MarkupType { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? MarkupValue { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ContribType { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? ContribValue { get; set; }

    public bool? ContributionInclVat { get; set; }

    public bool? MarkupInclVat { get; set; }

    public bool? Deleted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [InverseProperty("FkProduct")]
    public virtual ICollection<ClientProductApproval> ClientProductApprovals { get; set; } = new List<ClientProductApproval>();

    [ForeignKey("FkBaseProductId")]
    [InverseProperty("Products")]
    public virtual BaseProduct? FkBaseProduct { get; set; }

    [ForeignKey("FkCategoryId")]
    [InverseProperty("Products")]
    public virtual Category? FkCategory { get; set; }

    [ForeignKey("FkUnitId")]
    [InverseProperty("Products")]
    public virtual Unit? FkUnit { get; set; }

    [InverseProperty("FkProduct")]
    public virtual ICollection<ProductExcluded> ProductExcludeds { get; set; } = new List<ProductExcluded>();

    [InverseProperty("FkProduct")]
    public virtual ICollection<ProductSupplier> ProductSuppliers { get; set; } = new List<ProductSupplier>();

    [InverseProperty("Product")]
    public virtual ICollection<TransactionCodeMap> TransactionCodeMaps { get; set; } = new List<TransactionCodeMap>();
}


