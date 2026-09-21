using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityCompactList
{
    public Guid PkEntityId { get; set; }

    public string? Code { get; set; }

    public string Name { get; set; } = null!;

    public string? Taname { get; set; }

    public string? RegistrationNumber { get; set; }

    public string? Vatnumber { get; set; }

    public DateTime? LastSyncDate { get; set; }

    public string? Comments { get; set; }

    public string? ImageLocation { get; set; }

    public string? Description { get; set; }

    public Guid FkStatusId { get; set; }

    public string EntTypes { get; set; } = null!;

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public string Status { get; set; } = null!;

    public string QuickInfo { get; set; } = null!;

    public Guid? FkEntitySubTypeId { get; set; }

    public string? GroupName { get; set; }

    public int Balance { get; set; }

    public string? Hash { get; set; }

    public DateTime? LastUpdated { get; set; }

    public string? Idnumber { get; set; }

    public bool? Vatexemption { get; set; }

    public int Id { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? EntityReferenceAlias { get; set; }
}
