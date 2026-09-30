using GPay.Banking.Domain;
using GPay.Banking.EntityFrameworkCore.GPay;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;

namespace GPay.Banking.EntityFrameworkCore;

[DependsOn(
    typeof(BankingDomainModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule)
)]
public class BankingEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<BankingDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        context.Services.AddDbContextFactory<GPayLegacyDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var connection = configuration.GetConnectionString("GPay");
            options.UseSqlServer(string.IsNullOrWhiteSpace(connection)
                ? "Server=localhost;Database=GPay;Trusted_Connection=True;TrustServerCertificate=True"
                : connection);
        });
        context.Services.AddScoped<IGpayOrderSync, GpayOrderSync>();
    }
}
