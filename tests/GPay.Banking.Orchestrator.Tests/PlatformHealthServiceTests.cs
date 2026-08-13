using FluentAssertions;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Health;
using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Infrastructure.Configuration;
using GPay.Banking.Infrastructure.Health;
using GPay.Banking.Infrastructure.Messaging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;

namespace GPay.Banking.Orchestrator.Tests;

public class PlatformHealthServiceTests
{
    [Fact]
    public async Task GetQueueDepthsAsync_ReturnsKnownQueues()
    {
        var bus = new Mock<IMessageBus>();
        bus.Setup(b => b.GetQueueStatsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((12u, 1u));

        var sut = CreateSut(bus.Object);
        var queues = await sut.GetQueueDepthsAsync();

        queues.Should().Contain(q => q.QueueName == QueueNames.OrchestratorResponses);
        queues.Should().Contain(q => q.QueueName == "gpay.banking.absa.requests" && q.Bank == "Absa");
        queues.Should().OnlyContain(q => q.MessageCount == 12 && q.ConsumerCount == 1);
    }

    [Fact]
    public async Task GetPlatformHealthAsync_FlagsCloggedQueueAsUnhealthy()
    {
        var bus = new Mock<IMessageBus>();
        bus.SetupGet(b => b.IsConnected).Returns(true);
        bus.Setup(b => b.GetQueueStatsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((5000u, 1u));

        var sut = CreateSut(bus.Object, clogThreshold: 100);
        var health = await sut.GetPlatformHealthAsync();

        health.Services.Should().Contain(s => s.ServiceName == "Orchestrator" && s.Status == ServiceHealthStatus.Unhealthy);
        health.Services.Should().Contain(s => s.ServiceName == "Absa" && s.Status == ServiceHealthStatus.Unhealthy);
        health.OverallStatus.Should().Be(ServiceHealthStatus.Unhealthy);
    }

    [Fact]
    public async Task GetPlatformHealthAsync_FlagsMissingConsumersAsCritical()
    {
        var bus = new Mock<IMessageBus>();
        bus.SetupGet(b => b.IsConnected).Returns(true);
        bus.Setup(b => b.GetQueueStatsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((0u, 0u));

        var banking = Options.Create(new BankingOptions
        {
            Banks =
            [
                new BankRegistration
                {
                    BankCode = BankCode.Absa,
                    Name = "Absa",
                    RequestQueue = "gpay.banking.absa.requests"
                }
            ]
        });

        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient());

        var sut = new PlatformHealthService(
            bus.Object,
            Options.Create(new RabbitMqOptions { ClogThreshold = 1000 }),
            banking,
            factory.Object,
            NullLogger<PlatformHealthService>.Instance);

        var health = await sut.GetPlatformHealthAsync();
        var absa = health.Services.Single(s => s.ServiceName == "Absa");

        absa.Status.Should().Be(ServiceHealthStatus.Critical);
        absa.IsResponding.Should().BeFalse();
    }

    private static PlatformHealthService CreateSut(IMessageBus bus, int clogThreshold = 1000)
    {
        var banking = Options.Create(new BankingOptions
        {
            Banks =
            [
                new BankRegistration
                {
                    BankCode = BankCode.Absa,
                    Name = "Absa",
                    RequestQueue = "gpay.banking.absa.requests"
                }
            ]
        });

        var factory = new Mock<IHttpClientFactory>();
        factory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(new HttpClient());

        return new PlatformHealthService(
            bus,
            Options.Create(new RabbitMqOptions { ClogThreshold = clogThreshold }),
            banking,
            factory.Object,
            NullLogger<PlatformHealthService>.Instance);
    }
}
