using GPay.Banking;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.Helpers;

internal static class PaymentRuleDiagnostics
{
    public static void LogRailResolutionFailure(
        ILogger logger,
        string bank,
        string? orderNumber,
        decimal amount,
        string? requestedRail,
        BankPaymentConfiguration? configuration,
        Exception exception)
    {
        logger.LogError(
            exception,
            "Unable to resolve a payment rail for bank {Bank}, order {OrderNumber}, amount {Amount}, requestedRail={RequestedRail}. Active configured rules follow.",
            bank,
            orderNumber,
            amount,
            requestedRail);

        foreach (var rail in configuration?.Rails.Where(candidate => candidate.IsActive) ?? [])
        {
            foreach (var rule in rail.Rules)
            {
                logger.LogError(
                    "Active BankTime configuration during rail-resolution failure: bank={Bank}, rail={Rail}/{RailCode}, rule={RuleId}, day={Day}, time={StartTime}-{EndTime}, minAmount={MinAmount}, maxAmount={MaxAmount}, onUs={OnUs}, internal={IsInternal}.",
                    bank,
                    rail.Name,
                    rail.RailCode,
                    rule.RuleId,
                    DayName(rule.DayOfWeek),
                    rule.StartTime,
                    rule.EndTime,
                    rule.MinAmount,
                    rule.MaxAmount,
                    rule.IsOnUs,
                    rule.IsInternal);
            }
        }

        if (configuration is null || configuration.Rails.All(rail => !rail.IsActive))
        {
            logger.LogError("No active payment rails were loaded for bank {Bank}.", bank);
        }
    }

    public static void LogRailSelection(
        ILogger logger,
        string bank,
        string? orderNumber,
        decimal amount,
        string? requestedRail,
        string railCode,
        BankPaymentConfiguration? configuration)
    {
        var rail = configuration?.Rails.FirstOrDefault(candidate =>
            candidate.IsActive &&
            (string.Equals(candidate.RailCode, railCode, StringComparison.OrdinalIgnoreCase) ||
             string.Equals(candidate.Name, railCode, StringComparison.OrdinalIgnoreCase)));

        var matchingRules = rail?.Rules
            .Where(rule =>
                (!rule.MinAmount.HasValue || amount >= rule.MinAmount.Value) &&
                (!rule.MaxAmount.HasValue || amount <= rule.MaxAmount.Value))
            .ToList() ?? [];

        if (matchingRules.Count == 0)
        {
            logger.LogWarning(
                "Payment routing resolved bank {Bank} rail {Rail} for order {OrderNumber}, amount {Amount}, requestedRail={RequestedRail}, but no active database amount rule was available to log. ConfigurationLoaded={ConfigurationLoaded}.",
                bank,
                railCode,
                orderNumber,
                amount,
                requestedRail,
                configuration is not null);
            return;
        }

        logger.LogInformation(
            "Payment routing selected bank {Bank} rail {Rail} for order {OrderNumber}, amount {Amount}, requestedRail={RequestedRail}. {RuleCount} active BankTime rule(s) with IDs {RuleIds} accept this amount for rail selection; bank schedule validation checks day, time, and on-us/off-us before submission.",
            bank,
            railCode,
            orderNumber,
            amount,
            requestedRail,
            matchingRules.Count,
            string.Join(",", matchingRules.Select(rule => rule.RuleId)));
    }

    public static string DayName(int dayOfWeek) => dayOfWeek switch
    {
        0 => "Any",
        1 => "Monday",
        2 => "Tuesday",
        3 => "Wednesday",
        4 => "Thursday",
        5 => "Friday",
        6 => "Saturday",
        7 => "Sunday",
        8 => "Public holiday",
        _ => $"Day {dayOfWeek}"
    };
}
