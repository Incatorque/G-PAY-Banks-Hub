using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Capitec.Api.Models.Payment;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Services.Capitec.Api.Mapping;

/// <summary>Maps one Bank Hub payment to the single-transaction CPS request format.</summary>
public sealed class CapitecPaymentMapper : ICapitecPaymentMapper
{
    private static readonly TimeSpan SouthAfricaOffset = TimeSpan.FromHours(2);
    private static readonly Guid IdNamespace = new("b8d44604-3eb4-4c4d-8b1a-112c32b63266");
    private readonly CapitecOptions _options;

    public CapitecPaymentMapper(IOptions<CapitecOptions> options) => _options = options.Value;

    public CapitecPaymentMapper() : this(Options.Create(new CapitecOptions())) { }

    public string ToStatusLabel(string? status) => CapitecPaymentStatuses.ToLabel(status);

    public CapitecPaymentInitiateRequest ToCapitecInitiateRequest(InstantPaymentRequest request, string correlationId)
    {
        var bankId = request.GpayOrderId?.ToString("N") ?? request.Reference.Trim();
        var messageId = StableGuid($"message:{bankId}");
        var instructionUetr = StableGuid($"instruction:{bankId}");
        var endToEndId = Truncate(request.Reference.Trim(), 35);
        var bicfi = ResolveBicfi(request);
        var paymentDate = DateTimeOffset.UtcNow.ToOffset(SouthAfricaOffset)
            .ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        var mop = MapMethodOfPayment(request.PaymentRail);

        return new CapitecPaymentInitiateRequest
        {
            GpayOrderId = request.GpayOrderId,
            GroupHeader = new CapitecGroupHeader
            {
                MessageIdentification = messageId,
                CreationDateTime = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'", CultureInfo.InvariantCulture),
                NumberOfTransactions = "1",
                ControlSum = request.Amount,
                InitiatingPartyId = Required(_options.InitiatingPartyId, "Capitec:InitiatingPartyId")
            },
            PaymentInstruction =
            [
                new CapitecPaymentInstruction
                {
                    InstructionUetr = instructionUetr,
                    ConsolidatedDebitPosting = false,
                    NumberOfTransactions = 1,
                    ControlSum = request.Amount,
                    InstructedDate = paymentDate,
                    DebtorName = _options.DebtorName,
                    DebtorAccountIdentification = request.FromAccountNumber.Trim(),
                    CreditTransferTransaction =
                    [
                        new CapitecCreditTransferTransaction
                        {
                            EndToEndIdentification = endToEndId,
                            TransactionMop = mop,
                            InstructedAmount = request.Amount,
                            CreditorAgentBicfi = bicfi,
                            CreditorName = Required(request.BeneficiaryName, "BeneficiaryName"),
                            CreditorAccountIdentification = request.ToAccountNumber.Trim(),
                            CreditorReference = Truncate(
                                !string.IsNullOrWhiteSpace(request.ToStatementRef) ? request.ToStatementRef : request.Reference,
                                35)
                        }
                    ]
                }
            ]
        };
    }

    public InstantPaymentResponse ToGpayInitiateResponse(CapitecPaymentInitiateResponse response, InstantPaymentRequest request)
    {
        var label = CapitecPaymentStatuses.ToLabel(response.StatusCode.ToString("000", CultureInfo.InvariantCulture));
        return new InstantPaymentResponse
        {
            BankCode = "Capitec",
            TransactionId = response.EndToEndIdentification,
            TransactionReference = response.EndToEndIdentification,
            ApiReference = response.MessageIdentification,
            Status = label,
            RawStatusLabel = label,
            BankStatusCode = response.StatusCode,
            PaymentRail = request.PaymentRail,
            Reference = request.Reference,
            Amount = request.Amount,
            Currency = request.Currency,
            BankMessageIdentification = response.MessageIdentification,
            BankInstructionUetr = response.InstructionUetr,
            ResultDescription = "Capitec accepted the payment request. Final settlement status will arrive by callback or status enquiry."
        };
    }

    public CapitecPaymentStatusRequest ToCapitecStatusRequest(PaymentStatusRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.BankMessageIdentification) || string.IsNullOrWhiteSpace(request.BankInstructionUetr))
        {
            throw new InvalidOperationException("Capitec status enquiry requires the original Message Identification and Instruction UETR from the payout request.");
        }

        return new CapitecPaymentStatusRequest
        {
            GpayOrderId = request.GpayOrderId,
            GroupHeader = new CapitecStatusGroupHeader
            {
                MessageIdentification = Guid.NewGuid().ToString(),
                CreationDateTime = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'", CultureInfo.InvariantCulture),
                InitiatingPartyId = Required(_options.InitiatingPartyId, "Capitec:InitiatingPartyId")
            },
            StatusEnquiryRequest = new CapitecStatusEnquiry
            {
                OriginalMessageIdentification = request.BankMessageIdentification,
                OriginalInstructionUetr = request.BankInstructionUetr,
                OriginalEndToEndIdentification = request.TransactionReference
            }
        };
    }

    public PaymentStatusResponse ToGpayStatusResponse(CapitecPaymentStatusResponse response, PaymentStatusRequest request)
    {
        var item = response.StatusList?.FirstOrDefault();
        if (item is null)
        {
            var error = response.ErrorList?.FirstOrDefault();
            return new PaymentStatusResponse
            {
                BankCode = "Capitec",
                Status = "Failed",
                RawStatusLabel = "Failed",
                TransactionReference = request.TransactionReference,
                ApiReference = request.ApiReference ?? request.BankMessageIdentification,
                TransactionId = request.TransactionReference ?? request.ApiReference,
                ErrorCode = error?.Code ?? "CAPITEC_STATUS_EMPTY",
                ResultDescription = error?.Description ?? error?.Message ?? "Capitec returned no status result.",
                BankMessageIdentification = request.BankMessageIdentification,
                BankInstructionUetr = request.BankInstructionUetr
            };
        }

        var label = CapitecPaymentStatuses.ToLabel(item.Status);
        var failed = label == "Failed";
        return new PaymentStatusResponse
        {
            BankCode = "Capitec",
            TransactionId = request.TransactionReference ?? item.UniqueIdentifier,
            TransactionReference = request.TransactionReference ?? item.UniqueIdentifier,
            ApiReference = request.ApiReference ?? request.BankMessageIdentification,
            Status = label,
            RawStatusLabel = label,
            BankStatusCode = CapitecPaymentStatuses.ToCode(item.Status),
            PaymentRail = item.TransactionMop,
            ErrorCode = failed ? item.ReasonCode : null,
            ResultDescription = item.ReasonDescription ?? item.Status,
            BankMessageIdentification = request.BankMessageIdentification,
            BankInstructionUetr = request.BankInstructionUetr
        };
    }

    private string ResolveBicfi(InstantPaymentRequest request)
    {
        if (IsBicfi(request.ToBankBicfi)) return request.ToBankBicfi!.Trim().ToUpperInvariant();
        if (IsBicfi(request.ToBranchCode)) return request.ToBranchCode.Trim().ToUpperInvariant();

        var bank = Normalize(request.BeneficiaryBank);
        var configured = _options.BankBicfi.FirstOrDefault(item => Normalize(item.Key) == bank).Value;
        if (IsBicfi(configured)) return configured!.Trim().ToUpperInvariant();

        throw new InvalidOperationException($"No Capitec creditor BICFI is configured for beneficiary bank '{request.BeneficiaryBank}'. Set Capitec:BankBicfi or provide ToBankBicfi.");
    }

    private static string? MapMethodOfPayment(string? rail) => Normalize(rail) switch
    {
        "RPP" or "PAYSHAP" or "PBAC" => "RPP",
        "IIP" or "IPP" or "RTC" or "INSTANTPAYMENT" or "REALTIMECLEARANCE" => "RTC",
        "PAAF" or "AUTO" or "" => null,
        _ => throw new InvalidOperationException($"Payment method '{rail}' is not supported by Capitec Bulk Payments API. Use RPP/PayShap, RTC, or let Capitec derive the method.")
    };

    private static bool IsBicfi(string? value) => value is not null &&
        System.Text.RegularExpressions.Regex.IsMatch(value.Trim(), "^[A-Z0-9]{4}[A-Z]{2}[A-Z0-9]{2}([A-Z0-9]{3})?$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    private static string Normalize(string? value) => string.IsNullOrWhiteSpace(value)
        ? string.Empty
        : new(value.Where(char.IsLetterOrDigit).Select(char.ToUpperInvariant).ToArray());

    private static string Required(string? value, string field) => !string.IsNullOrWhiteSpace(value)
        ? value.Trim()
        : throw new InvalidOperationException($"{field} is required by Capitec Bulk Payments API.");

    private static string Truncate(string value, int max) => value.Length <= max ? value : value[..max];

    private static string StableGuid(string value)
    {
        var namespaceBytes = IdNamespace.ToByteArray();
        SwapGuidByteOrder(namespaceBytes);
        var nameBytes = Encoding.UTF8.GetBytes(value);
        var hash = SHA1.HashData([.. namespaceBytes, .. nameBytes]);
        hash[6] = (byte)((hash[6] & 0x0F) | 0x50);
        hash[8] = (byte)((hash[8] & 0x3F) | 0x80);
        var guidBytes = hash[..16];
        SwapGuidByteOrder(guidBytes);
        return new Guid(guidBytes).ToString();
    }

    private static void SwapGuidByteOrder(byte[] bytes)
    {
        (bytes[0], bytes[3]) = (bytes[3], bytes[0]);
        (bytes[1], bytes[2]) = (bytes[2], bytes[1]);
        (bytes[4], bytes[5]) = (bytes[5], bytes[4]);
        (bytes[6], bytes[7]) = (bytes[7], bytes[6]);
    }
}
