using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VUserGroup
{
    public Guid PkUserId { get; set; }

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public bool? Active { get; set; }

    public string? Uipersistance { get; set; }

    public string? Comment { get; set; }

    public bool? Status { get; set; }

    public Guid? FkGroupId { get; set; }

    public string? GroupName { get; set; }

    public string? Name { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public string AccountStatus { get; set; } = null!;

    public string? ImageLocation { get; set; }
}
