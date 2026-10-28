using GPay.Banking.Services.Absa;
using GPay.Banking;
using GPay.Banking.Services;
using GPay.Banking.Services.Absa.Api.Models.Payment;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Services.Absa.Api.Mapping;

/// <summary>
/// Default Absa payment mapper. Default Economics.Indicator = RPP (PayShap).
/// </summary>
public sealed class AbsaPaymentMapper : IAbsaPaymentMapper
{
    private static readonly TimeSpan SouthAfricaOffset = TimeSpan.FromHours(2);

    private readonly AbsaCapiOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaPaymentMapper"/> class.
    /// </summary>
    public AbsaPaymentMapper(IOptions<AbsaCapiOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Parameterless constructor for unit tests.
    /// </summary>
    public AbsaPaymentMapper()
        : this(Options.Create(new AbsaCapiOptions
        {
            DefaultSubmittingEntityName = "GPay",
            DefaultSubsidiaryEntityName = "GPay",
            DefaultSourceShortName = "GPAY"
        }))
    {
    }

    /// <inheritdoc />
    public AbsaPaymentInitiateRequest ToAbsaInitiateRequest(InstantPaymentRequest request)
    {
        var rail = NormalizeRail(request.PaymentRail);
        var fromAccount = StringValueResolver.FirstNonEmpty(request.FromAccountNumber, _options.DefaultSourceAccountNumber)
            ?? throw new InvalidOperationException("FromAccountNumber is required.");
        var shortName = StringValueResolver.FirstNonEmpty(request.FromShortName, _options.DefaultSourceShortName) ?? "GPAY";
        var submitting = StringValueResolver.FirstNonEmpty(request.SubmittingEntityName, _options.DefaultSubmittingEntityName) ?? "GPay";
        var subsidiary = StringValueResolver.FirstNonEmpty(request.SubsidiaryEntityName, _options.DefaultSubsidiaryEntityName) ?? submitting;
        var fromStmt = Truncate(StringValueResolver.FirstNonEmpty(request.FromStatementRef, request.Reference) ?? request.Reference, 20);
        var toStmt = Truncate(StringValueResolver.FirstNonEmpty(request.ToStatementRef, request.Reference) ?? request.Reference, 20);
        var paymentDate = string.IsNullOrWhiteSpace(request.PaymentDate)
            ? DateTimeOffset.UtcNow.ToOffset(SouthAfricaOffset).ToString("yyyy-MM-dd")
            : request.PaymentDate.Trim();

        var fromAccountType = request.FromAccountType != 0
            ? request.FromAccountType
            : _options.DefaultSourceAccountType;

        // Absa MG0001: Proof of Payment object is mandatory. Indicator is T/F on the wire.
        var sendPop = request.ProofOfPaymentIndicator switch
        {
            1 => true,
            0 => false,
            _ => !string.IsNullOrWhiteSpace(request.ProofOfPaymentEmail)
                 || !string.IsNullOrWhiteSpace(request.ProofOfPaymentMobile)
        };

        var proof = new AbsaPaymentProofOfPayment
        {
            EmailAddress = StringValueResolver.FirstNonEmpty(request.ProofOfPaymentEmail),
            MobileNumber = StringValueResolver.FirstNonEmpty(request.ProofOfPaymentMobile),
            Indicator = sendPop ? "T" : "F"
        };

        AbsaPaymentCallback? callback = null;
        var callbackUri = StringValueResolver.FirstNonEmpty(request.CallbackUri, _options.PaymentCallbackUri);
        var callbackToken = StringValueResolver.FirstNonEmpty(request.CallbackToken, _options.PaymentCallbackToken);
        if (!string.IsNullOrWhiteSpace(callbackUri) && !string.IsNullOrWhiteSpace(callbackToken))
        {
            callback = new AbsaPaymentCallback
            {
                Uri = callbackUri,
                Token = callbackToken,
                SupportEmail = StringValueResolver.FirstNonEmpty(request.CallbackSupportEmail, _options.PaymentCallbackSupportEmail)
            };
        }

        return new AbsaPaymentInitiateRequest
        {
            GpayOrderId = request.GpayOrderId,
            Authorisation = new AbsaPaymentAuthorisation
            {
                SubmittingEntityName = submitting,
                SubsidiaryEntityName = subsidiary,
                Indicator = request.AuthorisationIndicator
            },
            Source = new AbsaPaymentSource
            {
                StatementRef = fromStmt,
                ShortName = shortName,
                AccountType = fromAccountType,
                AccountNumber = fromAccount.Trim()
            },
            Target = new AbsaPaymentTarget
            {
                StatementRef = toStmt,
                AccountType = request.ToAccountType == 0 ? 10 : request.ToAccountType,
                AccountNumber = request.ToAccountNumber.Trim(),
                BankBranchCode = request.ToBranchCode.Trim(),
                Name = StringValueResolver.FirstNonEmpty(request.BeneficiaryName) ?? "BENEFICIARY",
                IsTrustAccount = StringValueResolver.FirstNonEmpty(request.IsTrustAccount)?.ToUpperInvariant() ?? "N"
            },
            Economics = new AbsaPaymentEconomics
            {
                Indicator = rail,
                CurrencyCode = StringValueResolver.FirstNonEmpty(request.Currency)?.ToUpperInvariant() ?? "ZAR",
                Amount = FormatAmount(request.Amount),
                PaymentDate = paymentDate,
                TransactionRef = Truncate(request.Reference.Trim(), 35)
            },
            ProofOfPayment = proof,
            Callback = callback
        };
    }

    /// <inheritdoc />
    public InstantPaymentResponse ToGpayInitiateResponse(
        AbsaPaymentInitiateResponse response,
        InstantPaymentRequest request)
    {
        var apiRef = response.GetCorrelation(3);
        var txRef = response.GetCorrelation(4) ?? request.Reference;
        var statusCode = response.StatusCode;
        var hasErrors = response.HasErrors;
        var label = ToStatusLabel(statusCode, hasErrors);
        var firstError = response.ErrorList?.FirstOrDefault();

        return new InstantPaymentResponse
        {
            TransactionId = txRef,
            TransactionReference = txRef,
            ApiReference = apiRef,
            SourceStatementRef = response.GetCorrelation(1),
            TargetStatementRef = response.GetCorrelation(2),
            Status = label,
            RawStatusLabel = label,
            BankStatusCode = statusCode,
            PaymentRail = NormalizeRail(request.PaymentRail),
            Reference = request.Reference,
            Amount = request.Amount,
            Currency = request.Currency,
            ResultDescription = firstError?.Description ?? firstError?.Message ?? InitiateStatusDescription(statusCode),
            ErrorCode = firstError?.Code
        };
    }

    /// <inheritdoc />
    public AbsaPaymentStatusRequest ToAbsaStatusRequest(PaymentStatusRequest request)
    {
        var corr = StringValueResolver.FirstNonEmpty(request.TransactionReference, request.ApiReference)
            ?? throw new InvalidOperationException("TransactionReference or ApiReference is required.");

        return new AbsaPaymentStatusRequest
        {
            Correlations = [corr.Trim()],
            GpayOrderId = request.GpayOrderId
        };
    }

    /// <inheritdoc />
    public PaymentStatusResponse ToGpayStatusResponse(
        AbsaPaymentStatusResponse response,
        PaymentStatusRequest request)
    {
        var item = response.StatusList?.FirstOrDefault();
        if (item is null)
        {
            var err = response.ErrorList?.FirstOrDefault();
            return new PaymentStatusResponse
            {
                Status = "Failed",
                RawStatusLabel = "Failed",
                BankStatusCode = 0,
                TransactionReference = request.TransactionReference,
                ApiReference = request.ApiReference,
                TransactionId = request.TransactionReference ?? request.ApiReference,
                ErrorCode = err?.Code ?? "PAYMENT_STATUS_EMPTY",
                ResultDescription = err?.Description ?? err?.Message ?? "No status returned from Absa."
            };
        }

        var apiRef = item.GetCorrelation(3) ?? request.ApiReference;
        var txRef = item.GetCorrelation(4) ?? request.TransactionReference;
        var statusCode = item.StatusCode;
        var hasErrors = item.HasErrors || response.HasErrors;
        var label = ToStatusLabel(statusCode, hasErrors);
        var firstError = item.ErrorList?.FirstOrDefault() ?? response.ErrorList?.FirstOrDefault();

        return new PaymentStatusResponse
        {
            TransactionId = txRef ?? apiRef,
            TransactionReference = txRef,
            ApiReference = apiRef,
            SourceStatementRef = item.GetCorrelation(1),
            TargetStatementRef = item.GetCorrelation(2),
            Status = label,
            RawStatusLabel = label,
            BankStatusCode = statusCode,
            ErrorCode = firstError?.Code,
            ResultDescription = firstError?.Description ?? firstError?.Message ?? AbsaPaymentStatuses.ToBankName(statusCode)
        };
    }

    /// <inheritdoc />
    public string ToStatusLabel(int? statusCode, bool hasErrors)
    {
        if (statusCode is null or 0)
        {
            return "Failed";
        }

        if (hasErrors && statusCode is 4 or 6 or 7)
        {
            return "Failed";
        }

        return AbsaPaymentStatuses.ToLabel(statusCode);
    }

    private static string FormatAmount(decimal amount) =>
        amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

    private static string InitiateStatusDescription(int? statusCode) => statusCode == 8
        ? "The Transaction Reference is a duplicate. Please enter a unique Transaction Reference."
        : AbsaPaymentStatuses.ToBankName(statusCode);

    private static string NormalizeRail(string? rail) =>
        string.IsNullOrWhiteSpace(rail) ? "RPP" : rail.Trim().ToUpperInvariant();

    private static string Truncate(string value, int max) =>
        value.Length <= max ? value : value[..max];
}
