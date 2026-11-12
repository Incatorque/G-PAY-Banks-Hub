namespace GPay.Banking;

public static class PaymentRailRouting
{
    public const string AbsaRpp = "RPP";
    public const string AbsaIip = "IIP";
    public const string AbsaPaaf = "PAAF";
    public const string Fnb3Pim = "3PIM";

    /// <summary>Resolves the bank rail using active rail rules configured in GPay.</summary>
    public static string ForOrder(
        BankCode bank,
        decimal amount,
        string? serviceType,
        BankPaymentConfiguration? configuration)
    {
        var rails = configuration?.Rails.Where(rail => rail.IsActive).ToList() ?? [];
        if (bank != BankCode.Absa)
        {
            var configuredRail = rails.FirstOrDefault(rail => HasAmountRule(rail, amount));
            if (configuredRail is not null)
            {
                return configuredRail.RailCode;
            }

            if (rails.Count > 0)
            {
                throw new InvalidOperationException(
                    $"No active GPay rail rule covers {amount:C} for {bank}. Configure an active payment rail and amount range.");
            }

            return string.Empty;
        }

        var rpp = FindRail(rails, AbsaRpp);
        if (rpp is not null && HasAmountRule(rpp, amount))
        {
            return AbsaRpp;
        }

        var iip = FindRail(rails, AbsaIip);
        if (string.Equals(serviceType, "02", StringComparison.Ordinal) && iip is not null && HasAmountRule(iip, amount))
        {
            return AbsaIip;
        }

        var paaf = FindRail(rails, AbsaPaaf);
        if (paaf is not null && HasAmountRule(paaf, amount))
        {
            return AbsaPaaf;
        }

        throw new InvalidOperationException(
            $"No active GPay rail rule covers {amount:C} for {bank}. Configure an active payment rail and amount range.");
    }

    private static bool HasAmountRule(BankPaymentRailConfiguration rail, decimal amount) =>
        rail.Rules.Any(rule =>
            (!rule.MinAmount.HasValue || amount >= rule.MinAmount.Value) &&
            (!rule.MaxAmount.HasValue || amount <= rule.MaxAmount.Value));

    private static BankPaymentRailConfiguration? FindRail(
        IEnumerable<BankPaymentRailConfiguration> rails,
        string railCode) => rails.FirstOrDefault(rail =>
            MatchesRailName(rail.RailCode, railCode) || MatchesRailName(rail.Name, railCode));

    private static bool MatchesRailName(string name, string railCode)
    {
        var normalized = string.Join("", name.Where(char.IsLetterOrDigit)).ToUpperInvariant();
        return railCode switch
        {
            AbsaRpp => normalized == "RPP" || normalized.Contains("PAYSHAP"),
            AbsaIip => normalized is "IIP" or "IPP" or "RTC" or "IIPRTC" or "REALTIMECLEARANCE" or "INSTANTPAYMENT",
            AbsaPaaf => normalized == "PAAF",
            _ => false
        };
    }

    public static string ForBankRequest(BankCode bank, string? requestedRail)
    {
        return bank switch
        {
            BankCode.Absa => ForAbsaRequest(requestedRail),
            BankCode.Fnb => Fnb3Pim,
            _ => requestedRail?.Trim() ?? string.Empty
        };
    }

    /// <summary>Validates an Absa API rail and amount against the bank's configured limits.</summary>
    public static string ForBankRequest(
        BankCode bank,
        string? requestedRail,
        decimal amount,
        BankPaymentConfiguration? configuration)
    {
        if (bank != BankCode.Absa)
        {
            var configuredRails = configuration?.Rails.Where(rail => rail.IsActive).ToList() ?? [];
            if (configuredRails.Count > 0)
            {
                var selectedRail = string.IsNullOrWhiteSpace(requestedRail)
                    ? configuredRails.FirstOrDefault(rail => HasAmountRule(rail, amount))
                    : configuredRails.FirstOrDefault(rail =>
                        string.Equals(rail.Name, requestedRail.Trim(), StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(rail.RailCode, requestedRail.Trim(), StringComparison.OrdinalIgnoreCase));

                if (selectedRail is null || !HasAmountRule(selectedRail, amount))
                {
                    throw new InvalidOperationException(
                        $"The requested payment rail is not configured for {amount:C} at {bank} in GPay.");
                }

                return selectedRail.RailCode;
            }

            return ForBankRequest(bank, requestedRail);
        }

        if (string.IsNullOrWhiteSpace(requestedRail))
        {
            return ForOrder(bank, amount, null, configuration);
        }

        var normalizedRail = ForAbsaRequest(requestedRail);
        var configuredRail = FindRail(configuration?.Rails.Where(rail => rail.IsActive) ?? [], normalizedRail);
        if (configuredRail is null || !HasAmountRule(configuredRail, amount))
        {
            throw new InvalidOperationException(
                $"The requested Absa rail {normalizedRail} is not configured for {amount:C} in GPay.");
        }

        return normalizedRail;
    }

    private static string ForAbsaRequest(string? requestedRail)
    {
        if (string.IsNullOrWhiteSpace(requestedRail))
        {
            return AbsaRpp;
        }

        var normalizedRail = requestedRail.Trim().ToUpperInvariant();
        return normalizedRail is AbsaRpp or AbsaIip or AbsaPaaf
            ? normalizedRail
            : throw new NotSupportedException($"Absa payment rail '{requestedRail}' is not supported.");
    }
}
