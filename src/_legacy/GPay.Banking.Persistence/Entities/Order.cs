using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Order
{
    public Guid PkOrderId { get; set; }

    public Guid FkClientId { get; set; }

    public Guid FkClientAddressId { get; set; }

    public DateTime OrderDate { get; set; }

    public int OrderNumber { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public int? BankSequence { get; set; }

    public Guid? FkUserId { get; set; }

    public Guid? FkEntityBankStatementId { get; set; }

    public Guid? GroupId { get; set; }

    public Guid? FkAllocatedVendorId { get; set; }

    public Guid? FkLinkedOrderId { get; set; }

    public string? ReferenceNumber { get; set; }

    public Guid? FkH2hvalidationStatusId { get; set; }

    public Guid? FkOrderTypeId { get; set; }

    public Guid? FkWholesalerId { get; set; }

    public string? InvoiceNumber { get; set; }

    public string? Receipt { get; set; }

    public Guid? FkFromAccountId { get; set; }

    public Guid? FkToAccountId { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? Grnreference { get; set; }

    public string? ExtRef { get; set; }

    public bool? ForwardRequisition { get; set; }

    public Guid? FkSupplierClientId { get; set; }

    public string? SupplierBankReference { get; set; }

    public string ServiceType { get; set; } = null!;

    public Guid? FkPlanId { get; set; }

    public bool? Active { get; set; }

    public string? FkIsopainMessageId { get; set; }

    public virtual ICollection<EntityOrderSweepLog> EntityOrderSweepLogs { get; set; } = new List<EntityOrderSweepLog>();

    public virtual Entity? FkAllocatedVendor { get; set; }

    public virtual Entity FkClient { get; set; } = null!;

    public virtual EntityAddress FkClientAddress { get; set; } = null!;

    public virtual EntityBankStatement? FkEntityBankStatement { get; set; }

    public virtual Account? FkFromAccount { get; set; }

    public virtual H2hvalidationStatus? FkH2hvalidationStatus { get; set; }

    public virtual Order? FkLinkedOrder { get; set; }

    public virtual OrderStatus FkOrderStatus { get; set; } = null!;

    public virtual OrderType? FkOrderType { get; set; }

    public virtual Account? FkToAccount { get; set; }

    public virtual Entity? FkWholesaler { get; set; }

    public virtual ICollection<Order> InverseFkLinkedOrder { get; set; } = new List<Order>();

    public virtual ICollection<MobileMartResponse> MobileMartResponses { get; set; } = new List<MobileMartResponse>();

    public virtual ICollection<OrderApproval> OrderApprovals { get; set; } = new List<OrderApproval>();

    public virtual ICollection<OrderCommission> OrderCommissions { get; set; } = new List<OrderCommission>();

    public virtual ICollection<OrderFile> OrderFiles { get; set; } = new List<OrderFile>();

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
