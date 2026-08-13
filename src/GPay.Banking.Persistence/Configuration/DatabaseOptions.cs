namespace GPay.Banking.Persistence.Configuration;

/// <summary>
/// Database persistence settings.
/// </summary>
public sealed class DatabaseOptions
{
    /// <summary>
    /// Configuration section name.
    /// </summary>
    public const string SectionName = "Database";

    /// <summary>
    /// Connection string name under <c>ConnectionStrings</c>. Defaults to <c>Banking</c>.
    /// </summary>
    public string ConnectionStringName { get; set; } = "Banking";

    /// <summary>
    /// When true, uses the EF Core in-memory provider (tests only).
    /// </summary>
    public bool UseInMemory { get; set; }

    /// <summary>
    /// In-memory database name when <see cref="UseInMemory"/> is true.
    /// </summary>
    public string InMemoryDatabaseName { get; set; } = "GPayBanking";

    /// <summary>
    /// When true, calls <c>EnsureCreated</c> on startup. Keep false for existing GPayDev.
    /// </summary>
    public bool EnsureCreated { get; set; }
}
