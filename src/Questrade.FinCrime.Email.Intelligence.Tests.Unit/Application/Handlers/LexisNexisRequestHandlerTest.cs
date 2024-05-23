using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Questrade.FinCrime.Email.Intelligence.Application.Handlers;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository.Models;
using Questrade.FinCrime.Email.Intelligence.Domain.Services;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Xunit;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Application.Handlers;

public class LexisNexisRequestHandlerTest
{
    private readonly ILogger<LexisNexisRequestHandler> _logger;
    private readonly IMetricService _metricService;
    private readonly ILexisNexisRepository _lexisNexisRepository;

    public LexisNexisRequestHandlerTest()
    {
        _logger = Substitute.For<ILogger<LexisNexisRequestHandler>>();
        _metricService = Substitute.For<IMetricService>();
        _lexisNexisRepository = Substitute.For<ILexisNexisRepository>();
    }

    [Fact]
    public async Task Handle_ShouldReturnLexisNexisResult_WhenLexisNexisIsEnabled()
    {
        // Arrange
        var lexisNexisConfiguration = new LexisNexisConfiguration { Enable = true };
        var model = new EmailIntelligenceMessage();
        var handler = new LexisNexisRequestHandler(_logger, _metricService, lexisNexisConfiguration, _lexisNexisRepository);

        // Act
        var result = await handler.Handle(model, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<EmailIntelligenceResultMessage>();
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenLexisNexisIsNotEnabled()
    {
        // Arrange
        var lexisNexisConfiguration = new LexisNexisConfiguration { Enable = false };
        var model = new EmailIntelligenceMessage();
        var handler = new LexisNexisRequestHandler(_logger, _metricService, lexisNexisConfiguration, _lexisNexisRepository);

        // Act
        var result = await handler.Handle(model, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenExceptionOccurs()
    {
        // Arrange
        var lexisNexisConfiguration = new LexisNexisConfiguration { Enable = true };
        var model = new EmailIntelligenceMessage();
        var handler = new LexisNexisRequestHandler(_logger, _metricService, lexisNexisConfiguration, _lexisNexisRepository);
        _lexisNexisRepository
            .GetLexisNexisAttributeQueryAsync(Arg.Any<GetLexisNexisAttributeQueryRequest>(), Arg.Any<CancellationToken>())
            .Throws(new Exception());

        // Act
        var result = await handler.Handle(model, CancellationToken.None);

        // Assert
        result.Should().BeNull();
        _metricService.Received(2).Increment(
            statName: Arg.Any<string>(),
            tags: Arg.Any<List<string>>()
        );
    }
}
