using Microsoft.Extensions.Logging;
using NSubstitute;
using Questrade.FinCrime.Email.Intelligence.Domain.Utils;
using Xunit;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Domain.Utils;

public class CorrelationContextTests
{
    private readonly CorrelationContext _correlationContext; 

    private readonly ILogger _logger;

    public CorrelationContextTests()
    {
        _correlationContext = Substitute.For<CorrelationContext>();
        _logger = Substitute.For<ILogger>();
    }

    [Fact]
    public void AddLogScopeForSubscriber_ShouldSetCorrelationId_WhenHasOne()
    {
        // Arrange
        var correlationId = Guid.NewGuid().ToString();

        // Act
        _correlationContext.AddLogScopeForSubscriber(_logger, correlationId, "test");

        // Assert
        Assert.Equal(correlationId, _correlationContext.CorrelationId);
    }

    [Fact]
    public void AddLogScopeForSubscriber_ShouldUseADefaultCorrelationId_WhenDoesNotProvideOne()
    {
        // Act
        _correlationContext.AddLogScopeForSubscriber(_logger, null, "test");

        // Assert
        Assert.NotNull(_correlationContext.CorrelationId);
    }
}
