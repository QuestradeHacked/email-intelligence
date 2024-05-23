using Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;
using Xunit;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Infra.Config.PubSub;

public class EmailIntelligenceSubscriberConfigurationTest
{
    private readonly Bogus.Faker _faker = new();

    [Fact]
    public void Validate_ShouldNotThrow()
    {
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            Enable = false
        };

        //Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenProjectIdIsNull(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = null
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The configuration options for the EmailIntelligenceSubscriber is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenProjectIdIsEmpty(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = string.Empty
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The configuration options for the EmailIntelligenceSubscriber is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenProjectIdIsASpace(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = " "
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The configuration options for the EmailIntelligenceSubscriber is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenSubscriptionIdIsNull(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            SubscriptionId = null
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The configuration options for the EmailIntelligenceSubscriber is not valid", exception.Message);
    }

    [Fact]
     public void Validate_ShouldReturnError_WhenSubscriptionIdIsEmpty(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            SubscriptionId = string.Empty
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The configuration options for the EmailIntelligenceSubscriber is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenSubscriptionIdIsASpace(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            SubscriptionId = " "
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The configuration options for the EmailIntelligenceSubscriber is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmulatorIsTrue_AndEndpointIsEmpty(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            SubscriptionId = _faker.Internet.Random.ToString(),
            UseEmulator = true,
            Endpoint = string.Empty
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The emulator configuration options for EmailIntelligenceSubscriber is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmulatorIsTrue_AndEndpointIsNull(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            SubscriptionId = _faker.Internet.Random.ToString(),
            UseEmulator = true,
            Endpoint = null
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The emulator configuration options for EmailIntelligenceSubscriber is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmulatorIsTrue_AndEndpointASpace(){
        // Arrange
        EmailIntelligenceSubscriberConfiguration  emailIntelligenceSubscriberConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            SubscriptionId = _faker.Internet.Random.ToString(),
            UseEmulator = true,
            Endpoint = " "
        };

        // Act
        var exception = Record.Exception(() => emailIntelligenceSubscriberConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The emulator configuration options for EmailIntelligenceSubscriber is not valid", exception.Message);
    }
}
