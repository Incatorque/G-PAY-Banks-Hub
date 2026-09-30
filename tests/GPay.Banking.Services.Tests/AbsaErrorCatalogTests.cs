using FluentAssertions;
using GPay.Banking;
using GPay.Banking.Services.Absa.Api.Mapping;
using Xunit;

namespace GPay.Banking.Services.Tests;

public class AbsaErrorCatalogTests
{
    [Fact]
    public void Resolve_KnownCode_UsesBankMessage()
    {
        var info = AbsaErrorCatalog.Resolve("BR0007", "Transaction has timed out");
        info.Known.Should().BeTrue();
        info.Code.Should().Be("BR0007");
        info.Category.Should().Be("BusinessRule");
        info.Message.Should().Be("Transaction has timed out");
    }

    [Fact]
    public void Resolve_StripsAbsaPrefix()
    {
        var info = AbsaErrorCatalog.Resolve("ABSA_ME0080", null);
        info.Known.Should().BeTrue();
        info.Message.Should().Contain("not registered");
    }

    [Fact]
    public void Map_UnknownCode_KeepsBankMessage()
    {
        var error = new AbsaErrorMapper().Map("E001", "Rejected");
        error.Code.Should().Be("ABSA_E001");
        error.Message.Should().Be("Rejected");
        error.BankCode.Should().Be("E001");
    }
}
