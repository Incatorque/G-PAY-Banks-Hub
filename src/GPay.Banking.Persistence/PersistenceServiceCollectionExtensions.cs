using GPay.Banking.Contracts.Persistence;
using GPay.Banking.Persistence.Configuration;
using GPay.Banking.Persistence.Data;
using GPay.Banking.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Persistence;

/// <summary>
/// DI registration for the GPayDev EF Core persistence layer.
/// </summary>
public static class PersistenceServiceCollectionExtensions
{
    /// <summary>
    /// Registers <see cref="BankingDbContext"/>, repositories, and <see cref="IUnitOfWork"/>.
    /// </summary>
    public static IServiceCollection AddGpayBankingPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.SectionName));

        services.AddDbContext<BankingDbContext>((sp, options) =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;

            if (dbOptions.UseInMemory)
            {
                options.UseInMemoryDatabase(dbOptions.InMemoryDatabaseName);
                return;
            }

            var connectionString = configuration.GetConnectionString(dbOptions.ConnectionStringName)
                ?? throw new InvalidOperationException(
                    $"Connection string '{dbOptions.ConnectionStringName}' was not found. " +
                    "Add it to appsettings.Local.json or user secrets.");

            options.UseSqlServer(connectionString, sql =>
            {
                sql.CommandTimeout(60);
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    /// <summary>
    /// Optionally creates the schema when <see cref="DatabaseOptions.EnsureCreated"/> is enabled.
    /// Do not enable against the shared GPayDev database.
    /// </summary>
    public static async Task EnsureGpayBankingDatabaseAsync(
        this IServiceProvider services,
        CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
        if (!options.EnsureCreated)
        {
            return;
        }

        var db = scope.ServiceProvider.GetRequiredService<BankingDbContext>();
        await db.Database.EnsureCreatedAsync(cancellationToken);
    }
}
