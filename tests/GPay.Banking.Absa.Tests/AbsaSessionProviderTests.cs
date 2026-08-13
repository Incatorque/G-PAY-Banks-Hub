using FluentAssertions;
using GPay.Banking.Absa.Clients;
using GPay.Banking.Absa.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;

namespace GPay.Banking.Absa.Tests;

public class AbsaSessionProviderTests
{
    [Fact]
    public async Task GetSessionAsync_PostsUsernamePassword_AndCachesSession()
    {
        var calls = 0;
        var handler = new Mock<HttpMessageHandler>();
        handler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(() =>
            {
                calls++;
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        """{"Success":true,"CorrelationId":"c1","Session":"SESS-1","ErrorList":[]}""",
                        Encoding.UTF8,
                        "application/json")
                };
            });

        var http = new HttpClient(handler.Object);
        var signer = new Mock<IAbsaRequestSigner>();
        var options = Options.Create(new AbsaCapiOptions
        {
            BaseUrl = "https://mercurius-uat.cib.digital",
            Username = "demo",
            Password = "secret",
            ClientApiKey = "key",
            SkipRequestSignature = true,
            SessionCacheSeconds = 1500
        });

        var sut = new AbsaSessionProvider(http, signer.Object, options, NullLogger<AbsaSessionProvider>.Instance);
        var s1 = await sut.GetSessionAsync();
        var s2 = await sut.GetSessionAsync();

        s1.Should().Be("SESS-1");
        s2.Should().Be("SESS-1");
        calls.Should().Be(1);
        signer.Verify(s => s.ApplyHeaders(It.IsAny<HttpRequestMessage>(), It.IsAny<string?>()), Times.Once);
    }
}
