using System.Text.Json;
using FluentAssertions;
using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using GPay.Banking.Contracts.Enums;
using GPay.Banking.Contracts.Messaging;
using GPay.Banking.Infrastructure.Configuration;
using GPay.Banking.Infrastructure.Correlation;
using GPay.Banking.Infrastructure.Messaging;
using GPay.Banking.Orchestrator.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;

namespace GPay.Banking.Orchestrator.Tests;

public class BankRoutingServiceTests
{
    [Fact]
    public async Task SendAsync_PublishesToBankQueue_AndReturnsCorrelatedResult()
    {
        var bus = new Mock<IMessageBus>();
        BankRequestMessage? published = null;

        bus.SetupGet(b => b.IsDirectReplyReady).Returns(true);
        bus.Setup(b => b.GetQueueStatsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((0u, 1u));

        bus.Setup(b => b.PublishAsync(
                "gpay.banking.absa.requests",
                It.IsAny<BankRequestMessage>(),
                It.IsAny<MessagePublishOptions>(),
                It.IsAny<CancellationToken>()))
            .Callback<string, BankRequestMessage, MessagePublishOptions?, CancellationToken>((_, msg, options, _) =>
            {
                published = msg;
                options.Should().NotBeNull();
                options!.ReplyTo.Should().Be(QueueNames.DirectReplyTo);
                options.CorrelationId.Should().Be("corr-route");
            })
            .Returns(Task.CompletedTask);

        var correlator = new InMemoryResponseCorrelator();
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

        var sut = new BankRoutingService(
            bus.Object,
            correlator,
            banking,
            NullLogger<BankRoutingService>.Instance);

        var sendTask = sut.SendAsync<AccountVerificationRequest, AccountVerificationResponse>(
            BankCode.Absa,
            BankOperation.AccountVerification,
            new AccountVerificationRequest { AccountNumber = "1", BranchCode = "632005" },
            "corr-route");

        await Task.Delay(50);

        published.Should().NotBeNull();
        published!.BankCode.Should().Be(BankCode.Absa);
        published.Operation.Should().Be(BankOperation.AccountVerification);
        published.CorrelationId.Should().Be("corr-route");

        var apiResult = ApiResult<AccountVerificationResponse>.Ok(
            new AccountVerificationResponse { IsVerified = true, ResultCode = "OK" },
            "corr-route");

        correlator.TryComplete("corr-route", JsonSerializer.Serialize(apiResult)).Should().BeTrue();

        var result = await sendTask;
        result.Success.Should().BeTrue();
        result.Data!.IsVerified.Should().BeTrue();
    }
}
