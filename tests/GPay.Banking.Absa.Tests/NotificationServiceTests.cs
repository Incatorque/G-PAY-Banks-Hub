using FluentAssertions;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Services;
using GPay.Banking.Contracts.Dtos.Notifications;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace GPay.Banking.Absa.Tests;

public class NotificationServiceTests
{
    [Fact]
    public async Task ProcessAsync_ReturnsSuccessfulStubEnvelope()
    {
        var capi = new Mock<IAbsaCapiClient>();
        capi.SetupGet(c => c.IsConfigured).Returns(false);

        var sut = new AbsaNotificationService(capi.Object, NullLogger<AbsaNotificationService>.Instance);

        var result = await sut.ProcessAsync(
            new NotificationRequest
            {
                AccountNumber = "123",
                Action = "Subscribe"
            },
            "corr-notify");

        result.Success.Should().BeTrue();
        result.Data!.Status.Should().Be("StubAccepted");
        result.CorrelationId.Should().Be("corr-notify");
    }
}
