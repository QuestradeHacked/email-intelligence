using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Questrade.FinCrime.Email.Intelligence.Infra.Services.Publisher;
using Xunit;
using Moq;
using NSubstitute;
using Questrade.FinCrime.Email.Intelligence.Domain.Utils;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.Library.PubSubClientHelper.Publisher;
using Questrade.Library.PubSubClientHelper.Publisher.Outbox.InMemory;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Infra.Services.Publisher;

public class PublisherHandlerServiceTest
{
    private readonly EmailIntelligencePublisherService _emailIntelligencePublisherService;

    private readonly EmailIntelligencePublisherConfiguration _emailIntelligencePublisherConfiguration = new();

    public PublisherHandlerServiceTest()
    {
        var loggerMoq = new Mock<ILoggerFactory>();

        var outboxConfiguration =
            new InMemoryMessageOutboxStoreConfiguration<PubSubMessage<EmailIntelligenceResultMessage>>();

        var outbox =
            new InMemoryMessageOutboxStore<PubSubMessage<EmailIntelligenceResultMessage>>(
                loggerMoq.Object,
                outboxConfiguration,
                _emailIntelligencePublisherConfiguration
            );

        _emailIntelligencePublisherService = Substitute.For<EmailIntelligencePublisherService>(
            loggerMoq.Object,
            _emailIntelligencePublisherConfiguration,
            outbox
        );
    }

    [Fact]
    public async Task Publish_ShouldPublishMessage_WhenEverythingGoesRight()
    {
        // Arrange
        var publishService = BuildPublisherHandlerService();
        var emailIntelligenceResultMessage = new EmailIntelligenceResultMessage();

        // Act
        await publishService.Publish(emailIntelligenceResultMessage, CancellationToken.None);

        // Assert
        await _emailIntelligencePublisherService
            .Received(1)
            .PublishMessageAsync(
                message: Arg.Any<PubSubMessage<EmailIntelligenceResultMessage>>(),
                options: Arg.Any<PublishingOptions>()
            );
    }

    private static IServiceProvider GetServiceProvider()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<CorrelationContext>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var serviceScopeMock = new Mock<IServiceScope>();
        serviceScopeMock.SetupGet<IServiceProvider>(s => s.ServiceProvider)
            .Returns(serviceProvider);

        var serviceScopeFactoryMock = new Mock<IServiceScopeFactory>();
        serviceScopeFactoryMock.Setup(s => s.CreateScope())
            .Returns(serviceScopeMock.Object);

        return serviceProvider;
    }

    private PublisherHandlerService BuildPublisherHandlerService()
    {
        var loggerService = new Mock<ILogger<PublisherHandlerService>>();

        var publishService = new PublisherHandlerService(
            loggerService.Object,
            _emailIntelligencePublisherService,
            GetServiceProvider()
        );

        return publishService;
    }
}
