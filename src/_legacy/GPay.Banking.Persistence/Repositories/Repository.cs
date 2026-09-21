using System.Linq.Expressions;
using GPay.Banking.Contracts.Persistence;
using GPay.Banking.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.Persistence.Repositories;

/// <summary>
/// EF Core generic repository over the scaffolded <see cref="BankingDbContext"/>.
/// </summary>
public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    private readonly BankingDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    public Repository(BankingDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public async Task<TEntity?> FindAsync(params object[] keyValues) =>
        await _dbSet.FindAsync(keyValues);

    /// <inheritdoc />
    public async Task<TEntity?> FindAsync(object[] keyValues, CancellationToken cancellationToken) =>
        await _dbSet.FindAsync(keyValues, cancellationToken);

    /// <inheritdoc />
    public async Task<IReadOnlyList<TEntity>> ListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = _dbSet.AsNoTracking();
        if (predicate is not null)
        {
            query = query.Where(predicate);
        }

        return await query.ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        _dbSet.AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
        _dbSet.AnyAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public Task<int> CountAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        CancellationToken cancellationToken = default) =>
        predicate is null
            ? _dbSet.CountAsync(cancellationToken)
            : _dbSet.CountAsync(predicate, cancellationToken);

    /// <inheritdoc />
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default) =>
        await _dbSet.AddAsync(entity, cancellationToken);

    /// <inheritdoc />
    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) =>
        await _dbSet.AddRangeAsync(entities, cancellationToken);

    /// <inheritdoc />
    public void Update(TEntity entity) =>
        _dbSet.Update(entity);

    /// <inheritdoc />
    public void Remove(TEntity entity) =>
        _dbSet.Remove(entity);

    /// <inheritdoc />
    public void RemoveRange(IEnumerable<TEntity> entities) =>
        _dbSet.RemoveRange(entities);
}
