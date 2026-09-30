using System.Text.Json;
using FluentAssertions;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Absa.Api.Mapping;
using GPay.Banking.Services.Absa.Api.Models.Payment;
using Xunit;

namespace GPay.Banking.Services.Tests;

public class AbsaPaymentStatusMappingTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    [Fact]
    public void StatusResponse_ReadsStatementRefsAndAcceptedStatus()
    {
        const string body = """
            {
              "StatusList": [
                {
                  "Status": 3,
                  "Correlations": [
                    { "Correlation": 1, "CorrelationId": "Creditor", "Id": "bdfa6b4e-8b27-4be8-92eb-e137f1ba3096" },
                    { "Correlation": 2, "CorrelationId": "Debtor", "Id": "70339a1b-186f-49d4-9608-5ca8ec50b530" },
                    { "Correlation": 4, "CorrelationId": "Tran1518598972958", "Id": "7416001e-03fc-47be-80c4-5f66b7fee8d0" },
                    { "Correlation": 3, "CorrelationId": "5255275a-83e5-4b13-9961-75b1e78f18db", "Id": "09a1c11b-a6ad-4610-a1d5-bea6b1e23ee9" }
                  ],
                  "ErrorList": []
                }
              ],
              "ErrorList": []
            }
            """;

        var response = JsonSerializer.Deserialize<AbsaPaymentStatusResponse>(body, JsonOptions);
        var mapped = new AbsaPaymentMapper().ToGpayStatusResponse(
            response!,
            new PaymentStatusRequest { TransactionReference = "Tran1518598972958" });

        mapped.Status.Should().Be("Completed");
        mapped.BankStatusCode.Should().Be(3);
        mapped.ResultDescription.Should().Be("Accepted");
        mapped.SourceStatementRef.Should().Be("Creditor");
        mapped.TargetStatementRef.Should().Be("Debtor");
        mapped.TransactionReference.Should().Be("Tran1518598972958");
        mapped.ApiReference.Should().Be("5255275a-83e5-4b13-9961-75b1e78f18db");
    }

    [Theory]
    [InlineData(0, "Failed")]
    [InlineData(1, "Pending")]
    [InlineData(2, "Submitted")]
    [InlineData(3, "Completed")]
    [InlineData(4, "Failed")]
    [InlineData(5, "Pending")]
    [InlineData(6, "Failed")]
    [InlineData(7, "Failed")]
    [InlineData(8, "Duplicate")]
    public void ToStatusLabel_MapsMigCodes(int code, string expected)
    {
        new AbsaPaymentMapper().ToStatusLabel(code, hasErrors: false).Should().Be(expected);
    }

    [Fact]
    public void Callback_Accepted_ReadsDataEnvelope()
    {
        const string body = """
            {
              "Data": {
                "PaymentStatus": { "Status": 3, "ErrorList": [] },
                "TransactionRef": "TRAN1603265393666",
                "UniqueEFTnumber": "",
                "Type": "RPP",
                "ApiRef": "7652b7f1-e6b4-4ae6-b946-455979ed21a3",
                "Token": "ClientToken",
                "Date": "2020-10-21T07:30:03.4402136Z"
              }
            }
            """;

        var parsed = AbsaPaymentCallbackParser.Parse(body);

        parsed.Should().NotBeNull();
        parsed!.StatusCode.Should().Be(3);
        parsed.StatusLabel.Should().Be("Completed");
        parsed.TransactionReference.Should().Be("TRAN1603265393666");
        parsed.ApiReference.Should().Be("7652b7f1-e6b4-4ae6-b946-455979ed21a3");
        parsed.PaymentRail.Should().Be("RPP");
        parsed.Token.Should().Be("ClientToken");
        parsed.ErrorMessage.Should().BeNull();
    }

    [Fact]
    public void Callback_Rejected_ReadsErrorList()
    {
        const string body = """
            {
              "Data": {
                "PaymentStatus": {
                  "Status": 4,
                  "ErrorList": [ { "Code": "BR0007", "Description": "Transaction has timed out" } ]
                },
                "TransactionRef": "TRAN1603265393666",
                "Type": "RPP",
                "ApiRef": "7652b7f1-e6b4-4ae6-b946-455979ed21a3",
                "Token": "ClientToken"
              }
            }
            """;

        var parsed = AbsaPaymentCallbackParser.Parse(body);

        parsed.Should().NotBeNull();
        parsed!.StatusLabel.Should().Be("Failed");
        parsed.ErrorCode.Should().Be("BR0007");
        parsed.ErrorMessage.Should().Be("Transaction has timed out");
    }
}
