using FluentAssertions;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Services;
using GPay.Banking.Contracts.Dtos.Statements;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace GPay.Banking.Absa.Tests;

public class StatementServiceTests
{
    [Fact]
    public async Task GetStatementAsync_ReturnsSuccessfulStubEnvelope()
    {
        var capi = new Mock<IAbsaCapiClient>();
        capi.SetupGet(c => c.IsConfigured).Returns(false);

        var sut = new AbsaStatementService(capi.Object, NullLogger<AbsaStatementService>.Instance);

        var result = await sut.GetStatementAsync(
            new StatementRequest
            {
                AccountNumber = "555",
                FromDate = new DateOnly(2026, 1, 1),
                ToDate = new DateOnly(2026, 1, 31)
            },
            "corr-stmt");

        result.Success.Should().BeTrue();
        result.Data!.AccountNumber.Should().Be("555");
        result.Data.Transactions.Should().BeEmpty();
        result.CorrelationId.Should().Be("corr-stmt");
    }
}
