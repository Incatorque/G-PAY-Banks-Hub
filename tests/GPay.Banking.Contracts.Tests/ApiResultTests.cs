using System.Text.Json;
using FluentAssertions;
using GPay.Banking.Contracts.Common;
using GPay.Banking.Contracts.Dtos.AccountVerification;

namespace GPay.Banking.Contracts.Tests;

public class ApiResultTests
{
    [Fact]
    public void Ok_SetsSuccessAndData()
    {
        var result = ApiResult<AccountVerificationResponse>.Ok(
            new AccountVerificationResponse { IsVerified = true, ResultCode = "OK" },
            "corr-1");

        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Error.Should().BeNull();
        result.CorrelationId.Should().Be("corr-1");
    }

    [Fact]
    public void Fail_SetsErrorEnvelope()
    {
        var result = ApiResult<AccountVerificationResponse>.Fail(
            new ApiError { Code = "X", Message = "failed" },
            "corr-2");

        result.Success.Should().BeFalse();
        result.Data.Should().BeNull();
        result.Error!.Code.Should().Be("X");
    }

    [Fact]
    public void ApiResult_RoundTripsThroughJson()
    {
        var original = ApiResult<AccountVerificationResponse>.Ok(
            new AccountVerificationResponse
            {
                IsVerified = true,
                AccountHolderName = "Jane Doe",
                ResultCode = "MATCH"
            },
            "corr-3");

        var json = JsonSerializer.Serialize(original);
        var restored = JsonSerializer.Deserialize<ApiResult<AccountVerificationResponse>>(json);

        restored.Should().NotBeNull();
        restored!.Success.Should().BeTrue();
        restored.Data!.AccountHolderName.Should().Be("Jane Doe");
        restored.CorrelationId.Should().Be("corr-3");
    }
}
