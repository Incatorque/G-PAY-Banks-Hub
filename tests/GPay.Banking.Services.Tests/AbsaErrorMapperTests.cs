using FluentAssertions;
using GPay.Banking.Services.Absa.Api.Mapping;
using Xunit;

namespace GPay.Banking.Services.Tests;

public class AbsaErrorMapperTests
{
    [Fact]
    public void Map_PrefixesBankCode()
    {
        var mapper = new AbsaErrorMapper();
        var error = mapper.Map("E001", "Rejected");
        error.Code.Should().Be("ABSA_E001");
        error.Message.Should().Be("Rejected");
        error.BankCode.Should().Be("E001");
    }
}

