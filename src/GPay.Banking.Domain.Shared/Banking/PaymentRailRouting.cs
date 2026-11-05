namespace GPay.Banking;

public static class PaymentRailRouting
{
    public const string AbsaRpp = "RPP";
    public const string AbsaIip = "IIP";
    public const string AbsaPaaf = "PAAF";
    public const string Fnb3Pim = "3PIM";

    /// <summary>
    /// Resolves order-specific rail selection where Bank Hub has bank-specific business rules.
    /// Banks without such rules leave selection to their adapter.
    /// </summary>
    public static string ForOrder(BankCode bank, decimal amount, string? serviceType) =>
        bank == BankCode.Absa ? ForAbsaOrder(amount, serviceType) : string.Empty;

    /// <summary>
    /// Resolves Absa's payment rail from the order amount and selected service type.
    /// RPP, IIP, and PAAF are Absa-specific and must not be used for other banks.
    /// </summary>
    public static string ForAbsaOrder(decimal amount, string? serviceType)
    {
        if (amount <= 50000m)
        {
            return AbsaRpp;
        }

        if (amount > 250000m)
        {
            return AbsaPaaf;
        }

        return string.Equals(serviceType, "02", StringComparison.Ordinal) ? AbsaIip : AbsaPaaf;
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
