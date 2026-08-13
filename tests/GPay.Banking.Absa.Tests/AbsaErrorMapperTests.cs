using FluentAssertions;
using GPay.Banking.Absa.Mapping;

namespace GPay.Banking.Absa.Tests;

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
        error.BankMessage.Should().Be("Rejected");
    }
}
