using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityContact
{
    public Guid PkEntityContactId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid? FkUserId { get; set; }

    public string Initials { get; set; } = null!;

    public string LastnName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public Guid FkTitleId { get; set; }

    public string? Telephone { get; set; }

    public string? Cellphone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public Guid? FkEntityAddressId { get; set; }

    public bool? Primaray { get; set; }

    public string? Idnumber { get; set; }

    public bool? EmailNotification { get; set; }

    public bool? Smsnotification { get; set; }

    public bool? Active { get; set; }

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual Title FkTitle { get; set; } = null!;

    public virtual User? FkUser { get; set; }
}
