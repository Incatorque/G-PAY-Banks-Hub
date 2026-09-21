using FluentAssertions;
using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Mapping;
using GPay.Banking.Services.Absa.Api.Models.Avs;
using GPay.Banking.Services.Absa.Api.Services;
using GPay.Banking.Domain.Dtos;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace GPay.Banking.Services.Tests;

public class AccountVerificationServiceTests
{
    [Fact]
    public async Task VerifyAsync_Simulator_ReturnsVerifiedEnvelope()
    {
        var capi = new Mock<IAbsaCapiClient>();
        capi.SetupGet(c => c.UseSimulator).Returns(true);
        capi.Setup(c => c.VerifyAccountAsync(It.IsAny<AbsaAvsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AbsaAvsRequest req, CancellationToken _) => new AbsaAvsResponse
            {
                Status = 5,
                ReferenceNumber = "SIM001",
                CorrelationId = req.CorrelationId,
                ValueList =
                [
                    new AbsaAvsValueItem { Key = "Account Open", Value = "Yes" },
                    new AbsaAvsValueItem { Key = "Account Found", Value = "Yes" },
                    new AbsaAvsValueItem { Key = "ID Matched", Value = "Yes" },
                    new AbsaAvsValueItem { Key = "Name Matched", Value = "Yes" }
                ]
            });

        var sut = new AbsaAccountVerificationService(
            capi.Object,
            new AbsaAvsMapper(),
            new AbsaErrorMapper(),
            NullLogger<AbsaAccountVerificationService>.Instance);

        var result = await sut.VerifyAsync(
            new AccountVerificationRequest
            {
                AccountNumber = "1234567890",
                BranchCode = "632005",
                IdentityNumber = "9001015009087",
                AccountHolderName = "John Doe",
                Reference = "AVS-1"
            },
            "corr-avs");

        result.Success.Should().BeTrue();
        result.Data!.IsVerified.Should().BeTrue();
        result.Data.BankReference.Should().Be("SIM001");
    }

    [Fact]
    public async Task VerifyAsync_MissingAccount_ReturnsValidationError()
    {
        var sut = new AbsaAccountVerificationService(
            new Mock<IAbsaCapiClient>().Object,
            new AbsaAvsMapper(),
            new AbsaErrorMapper(),
            NullLogger<AbsaAccountVerificationService>.Instance);

        var result = await sut.VerifyAsync(
            new AccountVerificationRequest { AccountNumber = " ", BranchCode = "632005" },
            "corr-val");

        result.Success.Should().BeFalse();
        result.Error!.Code.Should().Be("GPAY_AVS_ACCOUNT_REQUIRED");
    }
}

