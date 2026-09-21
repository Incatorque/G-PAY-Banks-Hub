namespace GPay.Banking.Contracts.Persistence;

/// <summary>
/// Unit of Work coordinating repositories and a single database transaction boundary.
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
    /// <summary>
    /// Returns a typed repository for any mapped entity.
    /// </summary>
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;

    /// <summary>
    /// Persists all staged changes in a single transaction.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins an explicit database transaction.
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current explicit transaction.
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current explicit transaction.
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
