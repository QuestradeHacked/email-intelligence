using Questrade.FinCrime.Email.Intelligence.Tests.Unit.Faker;
using Xunit;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Infra.Config.LexisNexis;

public class LexisNexisConfigurationTest
{
    private readonly LexisNexisFaker _faker;

    public LexisNexisConfigurationTest()
    {
        _faker = new LexisNexisFaker();
    }


    [Fact]
    public void Validate_ShouldNotThrowException_WhenConfigurationIsValid()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();

        // Act
        configuration.Validate();

        // Assert
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenOrgIdIsMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.OrgId = string.Empty;

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => configuration.Validate());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenApiKeyIsMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.ApiKey = string.Empty;

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => configuration.Validate());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenOutputFormatIsMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.OutputFormat = string.Empty;

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => configuration.Validate());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenPolicyNameIsMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.PolicyName = string.Empty;

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => configuration.Validate());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenServiceTypeIsMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.ServiceType = string.Empty;

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => configuration.Validate());
    }

    [Fact]
    public void Validate_ShouldThrowException_WhenAttributeQueryIsMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.AttributeQuery = string.Empty;

        // Act
        // Assert
        Assert.Throws<InvalidOperationException>(() => configuration.Validate());
    }

    [Fact]
    public void GetAllAttributeQueryUrl_ShouldReturnEmpty_WhenAttributeQueryAndBaseUrlAreMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.AttributeQuery = string.Empty;
        configuration.BaseAddress = string.Empty;

        // Act
        var result = configuration.GetAllAttributeQueryUrl();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetAllAttributeQueryUrl_ShouldReturnEmpty_WhenAttributeQueryAreMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.AttributeQuery = string.Empty;

        // Act
        var result = configuration.GetAllAttributeQueryUrl();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetAllAttributeQueryUrl_ShouldReturnEmpty_WhenBaseUrlAreMissing()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();
        configuration.BaseAddress = string.Empty;

        // Act
        var result = configuration.GetAllAttributeQueryUrl();

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void GetAllAttributeQueryUrl_ShouldReturnValidUrl_WhenBaseUrlAndAttributeQueryAreFilled()
    {
        // Arrange
        var configuration = _faker.GetValidLexisNexisConfiguration();

        // Act
        var result = configuration.GetAllAttributeQueryUrl();

        // Assert
        Assert.Equal($"{configuration.BaseAddress}/{configuration.AttributeQuery}", result);
    }
}
