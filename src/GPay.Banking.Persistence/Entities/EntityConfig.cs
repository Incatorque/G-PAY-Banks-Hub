using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityConfig
{
    public Guid PkEntityConfigId { get; set; }

    public Guid FkGroupId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkAccountId { get; set; }

    public Guid FkUserId { get; set; }

    public string ReportHeader { get; set; } = null!;

    public string HomeUrl { get; set; } = null!;

    public string Apiroute { get; set; } = null!;

    public string RegistrationUrl { get; set; } = null!;

    public string? SmsauthToken { get; set; }

    public bool? OpenLoop { get; set; }

    public string EmailPlaceholder { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string VendorDescr { get; set; } = null!;

    public string ConsumerDescr { get; set; } = null!;

    public string RepresentativeDescr { get; set; } = null!;

    public decimal BlanketOver { get; set; }

    public int? StatementTerm { get; set; }

    public bool? AllowUnallocated { get; set; }

    public bool? AddFees { get; set; }

    public bool? DisableDuplicateCheck { get; set; }

    public bool? DisableRegistrationNotification { get; set; }

    public Guid? FkEntityIdFees { get; set; }

    public virtual ICollection<EntityConfigAccount> EntityConfigAccounts { get; set; } = new List<EntityConfigAccount>();

    public virtual Account FkAccount { get; set; } = null!;

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual Entity? FkEntityIdFeesNavigation { get; set; }

    public virtual Group FkGroup { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;

    public virtual ICollection<IntegratedSystem> IntegratedSystems { get; set; } = new List<IntegratedSystem>();
}
