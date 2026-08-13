using FluentAssertions;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Mapping;
using GPay.Banking.Absa.Models.Avs;
using GPay.Banking.Absa.Services;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Persistence;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace GPay.Banking.Absa.Tests;

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

        var sut = CreateSut(capi.Object);

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
        result.Data.ResultCode.Should().Be("VERIFIED");
        result.Data.IdentityMatch.Should().Be("Y");
        result.Data.BankReference.Should().Be("SIM001");
    }

    [Fact]
    public async Task VerifyAsync_MissingAccount_ReturnsValidationError()
    {
        var capi = new Mock<IAbsaCapiClient>();
        var sut = CreateSut(capi.Object);

        var result = await sut.VerifyAsync(
            new AccountVerificationRequest { AccountNumber = " ", BranchCode = "632005" },
            "corr-val");

        result.Success.Should().BeFalse();
        result.Error!.Code.Should().Be("GPAY_AVS_ACCOUNT_REQUIRED");
    }

    [Fact]
    public async Task VerifyAsync_BankError_MapsToApiError()
    {
        var capi = new Mock<IAbsaCapiClient>();
        capi.SetupGet(c => c.UseSimulator).Returns(false);
        capi.Setup(c => c.VerifyAccountAsync(It.IsAny<AbsaAvsRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AbsaAvsResponse
            {
                Status = "033",
                ErrorList =
                [
                    new AbsaAvsErrorItem
                    {
                        Code = "UNAUTHORIZED",
                        Description = "Invalid credentials"
                    }
                ]
            });

        var sut = CreateSut(capi.Object);
        var result = await sut.VerifyAsync(
            new AccountVerificationRequest
            {
                AccountNumber = "1234567890",
                BranchCode = "632005",
                IdentityNumber = "9001015009087",
                AccountHolderName = "Jane Doe"
            },
            "corr-err");

        result.Success.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error!.Code.Should().Be("ABSA_UNAUTHORIZED", because: $"got {result.Error.Code}: {result.Error.Message}");
        result.Error.BankMessage.Should().Be("Invalid credentials");
    }

    [Fact]
    public void Mapper_MapsToAbsaCapiFieldNames()
    {
        var mapper = new AbsaAvsMapper();
        var absa = mapper.ToAbsaRequest(
            new AccountVerificationRequest
            {
                AccountNumber = "4081234567",
                BranchCode = "632005",
                IssuingBankCode = "000016",
                IdentityNumber = "9001015009087",
                AccountHolderName = "Jane Mary Smith",
                AccountType = "savings",
                Email = "jane@example.com",
                PhoneNumber = "0821234567"
            },
            "corr-map");

        absa.AccountNumber.Should().Be("4081234567");
        absa.BranchCode.Should().Be("632005");
        absa.BankCode.Should().Be("000016");
        absa.AccountType.Should().Be(2);
        absa.ClientIdType.Should().Be(2);
        absa.ClientIdNumber.Should().Be("9001015009087");
        absa.ClientInitials.Should().Be("JM");
        absa.ClientName.Should().Be("Smith");
        absa.EmailAddress.Should().Be("jane@example.com");
        absa.CellNumber.Should().Be("+27821234567");
        absa.CorrelationId.Should().Be("corr-map");
    }

    private static AbsaAccountVerificationService CreateSut(IAbsaCapiClient capi) =>
        new(
            capi,
            new AbsaAvsMapper(),
            new AbsaErrorMapper(),
            new Mock<IUnitOfWork>().Object,
            NullLogger<AbsaAccountVerificationService>.Instance);
}
