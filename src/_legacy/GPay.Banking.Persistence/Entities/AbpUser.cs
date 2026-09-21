using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpUser
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string UserName { get; set; } = null!;

    public string NormalizedUserName { get; set; } = null!;

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string Email { get; set; } = null!;

    public string NormalizedEmail { get; set; } = null!;

    public bool? EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string SecurityStamp { get; set; } = null!;

    public bool? IsExternal { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? PhoneNumberConfirmed { get; set; }

    public bool IsActive { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool? LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public bool ShouldChangePasswordOnNextLogin { get; set; }

    public int EntityVersion { get; set; }

    public DateTimeOffset? LastPasswordChangeTime { get; set; }

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public Guid? LastModifierId { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? DeleterId { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual ICollection<AbpUserClaim> AbpUserClaims { get; set; } = new List<AbpUserClaim>();

    public virtual ICollection<AbpUserLogin> AbpUserLogins { get; set; } = new List<AbpUserLogin>();

    public virtual ICollection<AbpUserOrganizationUnit> AbpUserOrganizationUnits { get; set; } = new List<AbpUserOrganizationUnit>();

    public virtual ICollection<AbpUserRole> AbpUserRoles { get; set; } = new List<AbpUserRole>();

    public virtual ICollection<AbpUserToken> AbpUserTokens { get; set; } = new List<AbpUserToken>();
}
