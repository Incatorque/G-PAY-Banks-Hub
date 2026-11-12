using GPay.Banking;
using GPay.Banking.Domain;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.EntityFrameworkCore.GPay;

/// <summary>Loads payment rail definitions and their BankTime rows from GPay's shared database.</summary>
internal sealed class BankPaymentConfigurationProvider(BankingDbContext dbContext)
    : IBankPaymentConfigurationProvider
{
    public async Task<BankPaymentConfiguration?> GetAsync(
        string bank,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(bank))
        {
            return null;
        }

        var bankToken = GetBankToken(bank);
        var bankRow = await dbContext.Set<GpayBankRow>()
            .AsNoTracking()
            .FirstOrDefaultAsync(row => row.Active == true &&
                (bankToken == "FNB"
                    ? row.Name.ToUpper().Contains("FNB") || row.Name.ToUpper().Contains("FIRST NATIONAL")
                    : row.Name.ToUpper().Contains(bankToken)), cancellationToken);
        if (bankRow is null)
        {
            return null;
        }

        var railRows = await dbContext.Set<GpayBankRailRow>()
            .AsNoTracking()
            .Where(rail => rail.BankId == bankRow.Id)
            .OrderBy(rail => rail.Name)
            .ToListAsync(cancellationToken);

        var railIds = railRows.Select(rail => rail.Id).ToArray();
        List<GpayBankTimeRuleRow> timeRows;
        if (railIds.Length == 0)
        {
            timeRows = [];
        }
        else
        {
            timeRows = await dbContext.Set<GpayBankTimeRuleRow>()
                .AsNoTracking()
                .Where(rule => rule.BankId == bankRow.Id && rule.Active == true && rule.BankRailId.HasValue && railIds.Contains(rule.BankRailId.Value))
                .ToListAsync(cancellationToken);
        }

        var rails = railRows.Select(rail => new BankPaymentRailConfiguration(
            rail.Id,
            rail.Name,
            rail.RailCode,
            rail.IsActive,
            timeRows.Where(rule => rule.BankRailId == rail.Id)
                .Select(rule => new BankPaymentTimeRule(
                    rule.Id,
                    rule.DayOfWeek,
                    rule.StartTime,
                    rule.EndTime,
                    rule.IsInternal == true,
                    rule.IsOnUs == true,
                    rule.MinAmount,
                    rule.MaxAmount))
                .ToList()))
            .ToList();

        return new BankPaymentConfiguration(bankRow.Id, bankRow.Name, rails);
    }

    private static string GetBankToken(string bank)
    {
        var normalized = bank.Trim().ToUpperInvariant();
        if (normalized.Contains("ABSA")) return "ABSA";
        if (normalized.Contains("FNB") || normalized.Contains("FIRST NATIONAL")) return "FNB";
        if (normalized.Contains("NEDBANK")) return "NEDBANK";
        return normalized;
    }
}
