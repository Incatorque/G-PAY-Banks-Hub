using FluentAssertions;
using GPay.Banking.Contracts.Persistence;
using GPay.Banking.Persistence.Data;
using GPay.Banking.Persistence.Entities;
using GPay.Banking.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.Contracts.Tests;

public class UnitOfWorkTests
{
    [Fact]
    public async Task DbContext_ConnectsToGPayDev()
    {
        await using var context = CreateContext();
        var canConnect = await context.Database.CanConnectAsync();
        canConnect.Should().BeTrue();
    }

    [Fact]
    public async Task Repository_CanQueryGPayDev_AbpSettings()
    {
        await using var context = CreateContext();
        await using IUnitOfWork uow = new UnitOfWork(context);

        var count = await uow.Repository<AbpSetting>().CountAsync();
        count.Should().BeGreaterThanOrEqualTo(0);
    }

    private static BankingDbContext CreateContext()
    {
        var cs = Environment.GetEnvironmentVariable("GPAY_BANKING_CS");
        if (string.IsNullOrWhiteSpace(cs))
        {
            // Local default for GPayDev (override with GPAY_BANKING_CS when needed).
            cs = "Data Source=10.10.1.100;Initial Catalog=GPayDev;Persist Security Info=True;User ID=gpay_secure;Password=]56[N)pbfmd};Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=GPay.Banking.Tests;Command Timeout=30";
        }

        var options = new DbContextOptionsBuilder<BankingDbContext>()
            .UseSqlServer(cs)
            .Options;

        return new BankingDbContext(options);
    }
}
