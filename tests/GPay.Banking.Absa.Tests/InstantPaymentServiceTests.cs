using FluentAssertions;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Services;
using GPay.Banking.Contracts.Dtos.InstantPayment;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace GPay.Banking.Absa.Tests;

public class InstantPaymentServiceTests
{
    [Fact]
    public async Task PayAsync_ReturnsSuccessfulStubEnvelope()
    {
        var capi = new Mock<IAbsaCapiClient>();
        capi.SetupGet(c => c.IsConfigured).Returns(false);

        var sut = new AbsaInstantPaymentService(capi.Object, NullLogger<AbsaInstantPaymentService>.Instance);

        var result = await sut.PayAsync(
            new InstantPaymentRequest
            {
                FromAccountNumber = "111",
                ToAccountNumber = "222",
                ToBranchCode = "632005",
                Amount = 100.50m,
                Reference = "PAY-1"
            },
            "corr-pay");

        result.Success.Should().BeTrue();
        result.Data!.Status.Should().Be("AcceptedStub");
        result.Data.Reference.Should().Be("PAY-1");
        result.Data.Amount.Should().Be(100.50m);
        result.CorrelationId.Should().Be("corr-pay");
    }
}
