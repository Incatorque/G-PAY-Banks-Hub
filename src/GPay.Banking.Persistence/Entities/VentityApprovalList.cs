using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VentityApprovalList
{
    public Guid PkEntityId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Taname { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Vatnumber { get; set; }

    public DateTime? LastSyncDate { get; set; }

    public string? Comments { get; set; }

    public string? Description { get; set; }

    public Guid FkStatusId { get; set; }

    public DateTime DateCreated { get; set; }

    public bool? Featured { get; set; }

    public Guid? FkEntitySubTypeId { get; set; }

    public string? Idnumber { get; set; }

    public bool? Vatexemption { get; set; }

    public Guid? FkRequestedUserId { get; set; }

    public bool? Vatregistered { get; set; }

    public Guid? FkIdentificationTypeId { get; set; }

    public string? EntityReferenceAlias { get; set; }

    public string? ImageLocation { get; set; }

    public string? RejectReason { get; set; }

    public string? WbulkPaymentType { get; set; }

    public string? WbulkLimitSgn { get; set; }

    public bool? IsNew { get; set; }

    public string Status { get; set; } = null!;

    public Guid UserToApprove { get; set; }

    public bool? PreviousLevelIsApproved { get; set; }
}
