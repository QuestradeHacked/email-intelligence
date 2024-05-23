using Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;
using Xunit;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Infra.Config.PubSub;

public class EmailIntelligencePublisherConfigurationTest
{
    private readonly Bogus.Faker _faker = new();

    [Fact]
    public void Validate_ShouldNotThrow()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            Enable = false
        };

        //Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.Null(exception);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenProjectIdIsNull()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = null
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The ProjectId :  configuration options for the EmailIntelligencePublisher is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenProjectIdIsEmpty()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = string.Empty
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The ProjectId :  configuration options for the EmailIntelligencePublisher is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenProjectIdIsASpace()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = " "
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The ProjectId :   configuration options for the EmailIntelligencePublisher is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenTopicIdIsNull()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            TopicId = null
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The TopicId :  configuration option for the EmailIntelligencePublisher is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenTopicIdIsEmpty()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            TopicId = string.Empty
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The TopicId :  configuration option for the EmailIntelligencePublisher is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenTopicIdIsASpace()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            TopicId = " "
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The TopicId :   configuration option for the EmailIntelligencePublisher is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmulatorIsTrue_AndEndpointIsEmpty()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            TopicId = _faker.Internet.Random.ToString(),
            UseEmulator = true,
            Endpoint = string.Empty
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The Endpoint:  emulator configuration options for EmailIntelligencePublisher is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmulatorIsTrue_AndEndpointIsNull()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            TopicId = _faker.Internet.Random.ToString(),
            UseEmulator = true,
            Endpoint = null
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The Endpoint:  emulator configuration options for EmailIntelligencePublisher is not valid", exception.Message);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenEmulatorIsTrue_AndEndpointASpace()
    {
        // Arrange
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration = new()
        {
            ProjectId = _faker.Internet.Random.ToString(),
            TopicId = _faker.Internet.Random.ToString(),
            UseEmulator = true,
            Endpoint = " "
        };

        // Act
        var exception = Record.Exception(() => emailIntelligencePublisherConfiguration.Validate());

        // Assert
        Assert.IsType<InvalidOperationException>(exception);
        Assert.Equal("The Endpoint:   emulator configuration options for EmailIntelligencePublisher is not valid", exception.Message);
    }
}
