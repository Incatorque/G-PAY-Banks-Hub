namespace GPay.Banking.Contracts.Entities;

/// <summary>
/// Base type for persisted entities.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Primary key.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// UTC timestamp when the entity was created.
    /// </summary>
    public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// UTC timestamp when the entity was last updated, if any.
    /// </summary>
    public DateTimeOffset? UpdatedAtUtc { get; set; }
}
