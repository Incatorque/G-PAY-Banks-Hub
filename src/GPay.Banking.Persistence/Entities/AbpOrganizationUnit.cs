using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class AbpOrganizationUnit
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public Guid? ParentId { get; set; }

    public string Code { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public int EntityVersion { get; set; }

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public DateTime? LastModificationTime { get; set; }

    public Guid? LastModifierId { get; set; }

    public bool? IsDeleted { get; set; }

    public Guid? DeleterId { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual ICollection<AbpOrganizationUnitRole> AbpOrganizationUnitRoles { get; set; } = new List<AbpOrganizationUnitRole>();

    public virtual ICollection<AbpUserOrganizationUnit> AbpUserOrganizationUnits { get; set; } = new List<AbpUserOrganizationUnit>();

    public virtual ICollection<AbpOrganizationUnit> InverseParent { get; set; } = new List<AbpOrganizationUnit>();

    public virtual AbpOrganizationUnit? Parent { get; set; }
}
