using FluentAssertions;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Mapping;
using GPay.Banking.Services.Absa.Api.Models.Payment;
using GPay.Banking.Services.Absa.Api.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace GPay.Banking.Services.Tests;

public class AbsaPaymentInitiationFailureTests
{
    [Fact]
    public async Task DuplicateReferenceStatus_IsReturnedAsFailureWithEmailMessage()
    {
        const string duplicateMessage = "The Transaction Reference is a duplicate. Please enter a unique Transaction Reference.";
        var client = new Mock<IAbsaCapiClient>();
        client.Setup(x => x.InitiatePaymentAsync(It.IsAny<AbsaPaymentInitiateRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new AbsaPaymentInitiateResponse { Status = 8, ErrorList = [] });

        var service = new AbsaInstantPaymentService(
            client.Object,
            new AbsaPaymentMapper(),
            new AbsaErrorMapper(),
            NullLogger<AbsaInstantPaymentService>.Instance);

        var result = await service.PayAsync(new InstantPaymentRequest
        {
            FromAccountNumber = "source",
            ToAccountNumber = "target",
            ToBranchCode = "123456",
            Amount = 1m,
            Reference = "duplicate-reference"
        }, "correlation-id");

        result.Success.Should().BeFalse();
        result.Error!.Code.Should().Be("ABSA_PAYMENT_DUPLICATE");
        result.Error.Message.Should().Be(duplicateMessage);
    }
}
