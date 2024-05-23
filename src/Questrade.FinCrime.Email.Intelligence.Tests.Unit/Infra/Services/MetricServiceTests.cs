using NSubstitute;
using Questrade.FinCrime.Email.Intelligence.Domain.Constants;
using Questrade.FinCrime.Email.Intelligence.Infra.Services;
using StatsdClient;
using Xunit;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Infra.Services;

public class MetricServiceTests
{
    private readonly IDogStatsd _dogStatsd;

    private readonly MetricService _metricService;

    public MetricServiceTests()
    {
        _dogStatsd = Substitute.For<IDogStatsd>();
        _metricService = new MetricService(_dogStatsd);
    }

    [Fact]
    public void Distribution_ShouldCallDogStatsdClient()
    {
        // Arrange
        const double latency = 101;

        // Act
        _metricService.Distribution(
            statName: MetricNames.EmailIntelligenceHandleMessageCount,
            latency,
            tags: new List<string>()
        );

        // Assert
        _dogStatsd.Received(1).Distribution(
            statName: Arg.Is(MetricNames.EmailIntelligenceHandleMessageCount),
            value: Arg.Is(latency),
            tags: Arg.Any<string[]>()
        );
    }

    [Fact]
    public void Increment_ShouldCallDogStatsdClient()
    {
        // Act
        _metricService.Increment(
            statName: MetricNames.EmailIntelligenceHandleMessageCount,
            tags: new List<string>()
        );

        // Assert
        _dogStatsd.Received(1).Increment(
            statName: Arg.Is(MetricNames.EmailIntelligenceHandleMessageCount),
            tags: Arg.Any<string[]>()
        );
    }

    [Fact]
    public void StartTimer_ShouldCallDogStatsdClient()
    {
        // Act
        _metricService.StartTimer(
            statName: MetricNames.EmailIntelligenceHandleMessageCount,
            tags: new List<string>()
        );

        // Assert
        _dogStatsd.Received(1).StartTimer(
            name: Arg.Is(MetricNames.EmailIntelligenceHandleMessageCount),
            tags: Arg.Any<string[]>()
        );
    }
}
