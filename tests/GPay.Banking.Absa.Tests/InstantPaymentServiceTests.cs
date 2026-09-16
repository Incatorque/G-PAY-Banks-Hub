using FluentAssertions;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Mapping;
using GPay.Banking.Absa.Models.Payment;
using GPay.Banking.Absa.Services;
using GPay.Banking.Contracts.Dtos.InstantPayment;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace GPay.Banking.Absa.Tests;

public class InstantPaymentServiceTests
{
    [Fact]
    public async Task PayAsync_Simulator_ReturnsMappedSubmittedEnvelope()
    {
        var capi = new Mock<IAbsaCapiClient>();
        capi.SetupGet(c => c.UseSimulator).Returns(true);
        capi.Setup(c => c.InitiatePaymentAsync(It.IsAny<AbsaPaymentInitiateRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AbsaPaymentInitiateRequest req, CancellationToken _) => new AbsaPaymentInitiateResponse
            {
                Status = 2,
                Correlations =
                [
                    new AbsaPaymentCorrelation { Type = 3, Value = "API-REF-1" },
                    new AbsaPaymentCorrelation { Type = 4, Value = req.Economics.TransactionRef }
                ],
                ErrorList = []
            });

        var sut = CreateSut(capi.Object);

        var result = await sut.PayAsync(
            new InstantPaymentRequest
            {
                FromAccountNumber = "111",
                ToAccountNumber = "222",
                ToBranchCode = "632005",
                Amount = 100.50m,
                Reference = "PAY-1",
                BeneficiaryName = "Jane Doe",
                PaymentRail = "RPP"
            },
            "corr-pay");

        result.Success.Should().BeTrue();
        result.Data!.Status.Should().Be("Submitted");
        result.Data.RawStatusLabel.Should().Be("Submitted");
        result.Data.BankStatusCode.Should().Be(2);
        result.Data.ApiReference.Should().Be("API-REF-1");
        result.Data.TransactionReference.Should().Be("PAY-1");
        result.Data.Reference.Should().Be("PAY-1");
        result.Data.Amount.Should().Be(100.50m);
        result.Data.PaymentRail.Should().Be("RPP");
        result.CorrelationId.Should().Be("corr-pay");
    }

    [Fact]
    public async Task GetStatusAsync_Simulator_ReturnsCompletedEnvelope()
    {
        var capi = new Mock<IAbsaCapiClient>();
        capi.SetupGet(c => c.UseSimulator).Returns(true);
        capi.Setup(c => c.GetPaymentStatusAsync(It.IsAny<AbsaPaymentStatusRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AbsaPaymentStatusResponse
            {
                StatusList =
                [
                    new AbsaPaymentStatusListItem
                    {
                        Status = 3,
                        Correlations =
                        [
                            new AbsaPaymentCorrelation { Type = 3, Value = "API-REF-1" },
                            new AbsaPaymentCorrelation { Type = 4, Value = "PAY-1" }
                        ],
                        ErrorList = []
                    }
                ],
                ErrorList = []
            });

        var sut = CreateSut(capi.Object);

        var result = await sut.GetStatusAsync(
            new PaymentStatusRequest { TransactionReference = "PAY-1" },
            "corr-status");

        result.Success.Should().BeTrue();
        result.Data!.Status.Should().Be("Completed");
        result.Data.RawStatusLabel.Should().Be("Completed");
        result.Data.BankStatusCode.Should().Be(3);
        result.Data.ApiReference.Should().Be("API-REF-1");
        result.Data.TransactionReference.Should().Be("PAY-1");
        result.CorrelationId.Should().Be("corr-status");
    }

    [Fact]
    public async Task PayAsync_MissingFromAccount_ReturnsValidationError()
    {
        var sut = CreateSut(new Mock<IAbsaCapiClient>().Object);

        var result = await sut.PayAsync(
            new InstantPaymentRequest
            {
                FromAccountNumber = " ",
                ToAccountNumber = "222",
                ToBranchCode = "632005",
                Amount = 10m,
                Reference = "PAY-2"
            },
            "corr-val");

        result.Success.Should().BeFalse();
        result.Error!.Code.Should().Be("GPAY_PAY_FROM_REQUIRED");
    }

    private static AbsaInstantPaymentService CreateSut(IAbsaCapiClient capi) =>
        new(
            capi,
            new AbsaPaymentMapper(),
            new AbsaErrorMapper(),
            NullLogger<AbsaInstantPaymentService>.Instance);
}
