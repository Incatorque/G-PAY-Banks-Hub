using FluentAssertions;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Services;
using GPay.Banking.Contracts.Dtos.Balances;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace GPay.Banking.Absa.Tests;

public class BalanceServiceTests
{
    [Fact]
    public async Task GetBalanceAsync_ReturnsSuccessfulStubEnvelope()
    {
        var capi = new Mock<IAbsaCapiClient>();
        capi.SetupGet(c => c.IsConfigured).Returns(false);

        var sut = new AbsaBalanceService(capi.Object, NullLogger<AbsaBalanceService>.Instance);

        var result = await sut.GetBalanceAsync(
            new BalanceRequest { AccountNumber = "999" },
            "corr-bal");

        result.Success.Should().BeTrue();
        result.Data!.AccountNumber.Should().Be("999");
        result.Data.Currency.Should().Be("ZAR");
        result.CorrelationId.Should().Be("corr-bal");
    }
}
