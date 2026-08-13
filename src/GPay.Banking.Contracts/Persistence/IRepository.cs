using System.Linq.Expressions;

namespace GPay.Banking.Contracts.Persistence;

/// <summary>
/// Generic repository for an entity type.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Finds an entity by primary key values.
    /// </summary>
    Task<TEntity?> FindAsync(params object[] keyValues);

    /// <summary>
    /// Finds an entity by primary key values.
    /// </summary>
    Task<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken);

    /// <summary>
    /// Returns all entities matching an optional predicate.
    /// </summary>
    Task<IReadOnlyList<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the first entity matching the predicate, or <c>null</c>.
    /// </summary>
    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns whether any entity matches the predicate.
    /// </summary>
    Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the number of entities matching an optional predicate.
    /// </summary>
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stages an entity for insert.
    /// </summary>
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stages multiple entities for insert.
    /// </summary>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stages an entity for update.
    /// </summary>
    void Update(TEntity entity);

    /// <summary>
    /// Stages an entity for delete.
    /// </summary>
    void Remove(TEntity entity);

    /// <summary>
    /// Stages multiple entities for delete.
    /// </summary>
    void RemoveRange(IEnumerable<TEntity> entities);
}
