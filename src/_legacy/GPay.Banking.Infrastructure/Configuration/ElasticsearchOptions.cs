namespace GPay.Banking.Infrastructure.Configuration;

/// <summary>
/// Elasticsearch / Kibana logging settings.
/// </summary>
public sealed class ElasticsearchOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "Elasticsearch";

    /// <summary>
    /// Elasticsearch node URI.
    /// </summary>
    public string Uri { get; set; } = "http://localhost:9200";

    /// <summary>
    /// Index format pattern.
    /// </summary>
    public string IndexFormat { get; set; } = "gpay-banking-{0:yyyy.MM.dd}";

    /// <summary>
    /// When false, Elasticsearch sink is skipped (console still used).
    /// </summary>
    public bool Enabled { get; set; } = false;
}
