using FluentAssertions;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Configuration;
using GPay.Banking.Absa.Models.Avs;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;

namespace GPay.Banking.Absa.Tests;

public class AbsaCapiClientTests
{
    [Fact]
    public async Task VerifyAccountAsync_LiveMode_PostsValidateBankDetailsWithSession()
    {
        string? capturedBody = null;
        string? capturedUri = null;
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Returns(async (HttpRequestMessage req, CancellationToken ct) =>
            {
                capturedUri = req.RequestUri?.AbsolutePath;
                if (req.Content is not null)
                {
                    capturedBody = await req.Content.ReadAsStringAsync(ct);
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        """
                        {
                          "Status": 5,
                          "ReferenceNumber": "17492985B1",
                          "ValueList": [
                            { "Key": "Account Open", "Value": "Yes" },
                            { "Key": "Account Found", "Value": "Yes" },
                            { "Key": "ID Matched", "Value": "Yes" },
                            { "Key": "Name Matched", "Value": "Yes" }
                          ],
                          "CorrelationId": "09ab8ca5-02c0-4475-a614-f5f14be84075",
                          "ErrorList": [],
                          "ResponseUtcTime": "2019-09-20T06:26:24.7057762Z",
                          "UserFriendlyDisplayList": []
                        }
                        """,
                        Encoding.UTF8,
                        "application/json")
                };
            });

        var http = new HttpClient(handler.Object) { BaseAddress = new Uri("https://capi-uat.absa.co.za/") };
        var sessions = new Mock<IAbsaSessionProvider>();
        sessions.Setup(s => s.GetSessionAsync(It.IsAny<CancellationToken>())).ReturnsAsync("SESSION-ABC");
        var signer = new Mock<IAbsaRequestSigner>();

        var options = Options.Create(new AbsaCapiOptions
        {
            BaseUrl = "https://capi-uat.absa.co.za",
            Username = "user",
            Password = "pass",
            CapiCode = "10068",
            ClientApiKey = "key",
            UseSimulator = false,
            SkipRequestSignature = true
        });

        var sut = new AbsaCapiClient(http, sessions.Object, signer.Object, options, NullLogger<AbsaCapiClient>.Instance);
        var response = await sut.VerifyAccountAsync(new AbsaAvsRequest
        {
            CorrelationId = "r1",
            CapiCode = "10068",
            BankCode = "000016",
            BranchCode = "632005",
            AccountNumber = "9050861780",
            AccountType = 1,
            ClientIdType = 2,
            ClientIdNumber = "3011075124085",
            ClientName = "GONZALEZ",
            ClientInitials = "S"
        });

        response.IsSuccessStatus.Should().BeTrue();
        response.GetValue("ID Matched").Should().Be("Yes");
        sessions.Verify(s => s.GetSessionAsync(It.IsAny<CancellationToken>()), Times.Once);
        signer.Verify(s => s.ApplyHeaders(It.IsAny<HttpRequestMessage>(), It.IsAny<string?>()), Times.Once);

        capturedUri.Should().Be("/api/Account/ValidateBankDetails");
        capturedBody.Should().Contain("SESSION-ABC");
        capturedBody.Should().Contain("CapiCode");
        capturedBody.Should().Contain("BankCode");
        capturedBody.Should().Contain("ClientIdNumber");
    }

    [Fact]
    public async Task VerifyAccountAsync_NonAbsaPending_CallsValidateBankReference()
    {
        var calls = new List<string>();
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync((HttpRequestMessage req, CancellationToken _) =>
            {
                var path = req.RequestUri!.AbsolutePath;
                calls.Add(path);

                if (path.Contains("ValidateBankDetails", StringComparison.OrdinalIgnoreCase))
                {
                    return new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(
                            """{"Status":1,"ReferenceNumber":"17492985B2","ValueList":[],"ErrorList":[]}""",
                            Encoding.UTF8,
                            "application/json")
                    };
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        """
                        {
                          "Status": 2,
                          "ValueList": [
                            { "Key": "Account Open", "Value": "Yes" },
                            { "Key": "Account Found", "Value": "Yes" },
                            { "Key": "ID Matched", "Value": "Yes" },
                            { "Key": "Name Matched", "Value": "Yes" }
                          ],
                          "ErrorList": []
                        }
                        """,
                        Encoding.UTF8,
                        "application/json")
                };
            });

        var http = new HttpClient(handler.Object) { BaseAddress = new Uri("https://capi-uat.absa.co.za/") };
        var sessions = new Mock<IAbsaSessionProvider>();
        sessions.Setup(s => s.GetSessionAsync(It.IsAny<CancellationToken>())).ReturnsAsync("SESSION-ABC");
        var signer = new Mock<IAbsaRequestSigner>();
        var options = Options.Create(new AbsaCapiOptions
        {
            BaseUrl = "https://capi-uat.absa.co.za",
            Username = "user",
            Password = "pass",
            CapiCode = "10068",
            ClientApiKey = "key",
            UseSimulator = false,
            ReferencePollIntervalMs = 1,
            ReferencePollMaxAttempts = 3
        });

        var sut = new AbsaCapiClient(http, sessions.Object, signer.Object, options, NullLogger<AbsaCapiClient>.Instance);
        var response = await sut.VerifyAccountAsync(new AbsaAvsRequest
        {
            CapiCode = "10068",
            BankCode = "000005",
            BranchCode = "250655",
            AccountNumber = "1234567890",
            AccountType = 1,
            ClientIdType = 2,
            ClientIdNumber = "8408215438505",
            ClientName = "DOE",
            ClientInitials = "J"
        });

        calls.Should().Contain(p => p.Contains("ValidateBankDetails"));
        calls.Should().Contain(p => p.Contains("ValidateBankReference"));
        response.IsSuccessStatus.Should().BeTrue();
        response.GetValue("Account Found").Should().Be("Yes");
    }

    [Fact]
    public async Task VerifyAccountAsync_Simulator_DoesNotCallHttp()
    {
        var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        var http = new HttpClient(handler.Object);
        var sessions = new Mock<IAbsaSessionProvider>(MockBehavior.Strict);
        var signer = new Mock<IAbsaRequestSigner>(MockBehavior.Strict);
        var options = Options.Create(new AbsaCapiOptions { UseSimulator = true });

        var sut = new AbsaCapiClient(http, sessions.Object, signer.Object, options, NullLogger<AbsaCapiClient>.Instance);
        var response = await sut.VerifyAccountAsync(new AbsaAvsRequest
        {
            CorrelationId = "sim-1",
            CapiCode = "10068",
            BankCode = "000016",
            AccountNumber = "1",
            BranchCode = "632005",
            AccountType = 1,
            ClientIdType = 2,
            ClientIdNumber = "9001015009087",
            ClientName = "Doe",
            ClientInitials = "J"
        });

        response.StatusCode.Should().Be("5");
        response.GetValue("Account Found").Should().Be("Yes");
    }
}
