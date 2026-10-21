using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Order")]
[Index("OrderNumber", Name = "AK_Order_OrderNumber", IsUnique = true)]
[Index("FkFromAccountId", Name = "NCI_Order_OrderIDClientIDClientAddrIDOrderDateOrderNumOrderStatusIDBankSeqUserIDBankStatIDGroupIDEtc")]
public partial class Order : Entity<Guid>
{
    [Key]
    [Column("PkOrderID")]
    public override Guid Id { get; protected set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkClientAddressID")]
    public Guid FkClientAddressId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    public int OrderNumber { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    public int? BankSequence { get; set; }

    [Column("FkUserID")]
    public Guid? FkUserId { get; set; }

    [Column("FkEntityBankStatementID")]
    public Guid? FkEntityBankStatementId { get; set; }

    [Column("GroupID")]
    public Guid? GroupId { get; set; }

    [Column("FkAllocatedVendorID")]
    public Guid? FkAllocatedVendorId { get; set; }

    [Column("FkLinkedOrderID")]
    public Guid? FkLinkedOrderId { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [Column("FkH2HValidationStatusID")]
    public Guid? FkH2hvalidationStatusId { get; set; }

    [Column("FkOrderTypeID")]
    public Guid? FkOrderTypeId { get; set; }

    [Column("FkWholesalerID")]
    public Guid? FkWholesalerId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? InvoiceNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Receipt { get; set; }

    [Column("FkFromAccountID")]
    public Guid? FkFromAccountId { get; set; }

    [Column("FkToAccountID")]
    public Guid? FkToAccountId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DeliveryDate { get; set; }

    [Column("GRNReference")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Grnreference { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ExtRef { get; set; }

    public bool? ForwardRequisition { get; set; }

    [Column("FkSupplierClientID")]
    public Guid? FkSupplierClientId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierBankReference { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string ServiceType { get; set; } = null!;

    public Guid? FkPlanId { get; set; }

    public bool? Active { get; set; }

    [Column("FkISOPainMessageId")]
    [StringLength(50)]
    [Unicode(false)]
    public string? FkIsopainMessageId { get; set; }

    public bool? IsPayShap { get; set; }

    [InverseProperty("FkOrder")]
    public virtual ICollection<EntityOrderSweepLog> EntityOrderSweepLogs { get; set; } = new List<EntityOrderSweepLog>();

    [ForeignKey("FkAllocatedVendorId")]
    [InverseProperty("OrderFkAllocatedVendors")]
    public virtual CustomEntity? FkAllocatedVendor { get; set; }

    [ForeignKey("FkClientId")]
    [InverseProperty("OrderFkClients")]
    public virtual CustomEntity FkClient { get; set; } = null!;

    [ForeignKey("FkClientAddressId")]
    [InverseProperty("Orders")]
    public virtual EntityAddress FkClientAddress { get; set; } = null!;

    [ForeignKey("FkEntityBankStatementId")]
    [InverseProperty("Orders")]
    public virtual EntityBankStatement? FkEntityBankStatement { get; set; }

    [ForeignKey("FkFromAccountId")]
    [InverseProperty("OrderFkFromAccounts")]
    public virtual Account? FkFromAccount { get; set; }

    [ForeignKey("FkH2hvalidationStatusId")]
    [InverseProperty("Orders")]
    public virtual H2hvalidationStatus? FkH2hvalidationStatus { get; set; }

    [ForeignKey("FkLinkedOrderId")]
    [InverseProperty("InverseFkLinkedOrder")]
    public virtual Order? FkLinkedOrder { get; set; }

    [ForeignKey("FkOrderStatusId")]
    [InverseProperty("Orders")]
    public virtual OrderStatus FkOrderStatus { get; set; } = null!;

    [ForeignKey("FkOrderTypeId")]
    [InverseProperty("Orders")]
    public virtual OrderType? FkOrderType { get; set; }

    [ForeignKey("FkToAccountId")]
    [InverseProperty("OrderFkToAccounts")]
    public virtual Account? FkToAccount { get; set; }

    [ForeignKey("FkWholesalerId")]
    [InverseProperty("OrderFkWholesalers")]
    public virtual CustomEntity? FkWholesaler { get; set; }

    [InverseProperty("FkLinkedOrder")]
    public virtual ICollection<Order> InverseFkLinkedOrder { get; set; } = new List<Order>();

    [InverseProperty("FkOrder")]
    public virtual ICollection<MobileMartResponse> MobileMartResponses { get; set; } = new List<MobileMartResponse>();

    [InverseProperty("FkOrder")]
    public virtual ICollection<OrderApproval> OrderApprovals { get; set; } = new List<OrderApproval>();

    [InverseProperty("FkOrder")]
    public virtual ICollection<OrderCommission> OrderCommissions { get; set; } = new List<OrderCommission>();

    [InverseProperty("FkOrder")]
    public virtual ICollection<OrderFile> OrderFiles { get; set; } = new List<OrderFile>();

    [InverseProperty("FkOrder")]
    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    [InverseProperty("FkOrder")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}


