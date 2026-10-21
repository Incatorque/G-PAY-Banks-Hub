using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BankApiCallLog")]
[Index("Channel", "EndpointName", "Succeeded", Name = "IX_BankApiCallLog_Channel_EndpointName_Succeeded")]
public partial class BankApiCallLog : Entity<Guid>
{
    [Key]
    public override Guid Id { get; protected set; }

    public DateTimeOffset CreatedUtc { get; set; }

    [StringLength(80)]
    public string Environment { get; set; } = null!;

    public Guid? QueueId { get; set; }

    [StringLength(64)]
    public string? OrderId { get; set; }

    [StringLength(50)]
    public string EndpointName { get; set; } = null!;

    [StringLength(10)]
    public string HttpMethod { get; set; } = null!;

    [StringLength(500)]
    public string UrlTemplate { get; set; } = null!;

    [Column("XRequestId")]
    [StringLength(120)]
    public string? XrequestId { get; set; }

    [StringLength(80)]
    public string? MsgId { get; set; }

    [StringLength(80)]
    public string? PmtInfId { get; set; }

    [StringLength(140)]
    public string? EndToEndId { get; set; }

    [StringLength(120)]
    public string? InstructionId { get; set; }

    public int? HttpStatusCode { get; set; }

    public long DurationMs { get; set; }

    [StringLength(128)]
    public string? RequestHash { get; set; }

    [StringLength(128)]
    public string? ResponseHash { get; set; }

    [StringLength(2000)]
    public string? MaskedErrorSummary { get; set; }

    public bool Succeeded { get; set; }

    public string? RawRequestPayload { get; set; }

    public string? RawResponsePayload { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Channel { get; set; } = null!;

    [ForeignKey("Channel")]
    [InverseProperty("BankApiCallLogs")]
    public virtual BankChannel ChannelNavigation { get; set; } = null!;
}


