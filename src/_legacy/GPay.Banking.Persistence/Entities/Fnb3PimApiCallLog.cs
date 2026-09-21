using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Fnb3PimApiCallLog
{
    public Guid Id { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public string Environment { get; set; } = null!;

    public Guid? QueueId { get; set; }

    public string? OrderId { get; set; }

    public string EndpointName { get; set; } = null!;

    public string HttpMethod { get; set; } = null!;

    public string UrlTemplate { get; set; } = null!;

    public string? XrequestId { get; set; }

    public string? MsgId { get; set; }

    public string? PmtInfId { get; set; }

    public string? EndToEndId { get; set; }

    public string? InstructionId { get; set; }

    public int? HttpStatusCode { get; set; }

    public long DurationMs { get; set; }

    public string? RequestHash { get; set; }

    public string? ResponseHash { get; set; }

    public string? MaskedErrorSummary { get; set; }

    public bool Succeeded { get; set; }

    public string? RawRequestPayload { get; set; }

    public string? RawResponsePayload { get; set; }
}
