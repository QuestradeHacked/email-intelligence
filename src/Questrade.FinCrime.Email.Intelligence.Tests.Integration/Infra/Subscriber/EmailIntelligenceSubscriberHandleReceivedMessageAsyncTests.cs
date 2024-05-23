using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Questrade.FinCrime.Email.Intelligence.Domain.Services;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.FinCrime.Email.Intelligence.Infra.Services.Publisher;
using Questrade.FinCrime.Email.Intelligence.Infra.Services.Subscriber;
using Questrade.FinCrime.Email.Intelligence.Tests.Integration.Faker;
using Questrade.FinCrime.Email.Intelligence.Tests.Integration.Fixture;
using Questrade.FinCrime.Email.Intelligence.Tests.Integration.Providers;
using Questrade.Library.PubSubClientHelper.Primitives;
using Xunit;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Integration.Infra.Subscriber;

public class EmailIntelligenceSubscriberHandleReceivedMessageAsyncTests : IAssemblyFixture<PubSubEmulatorProcessFixture>
{
    private readonly MockLogger<EmailIntelligenceSubscriberHandleReceivedMessageAsyncTests> _logger;

    private readonly Mock<ILoggerFactory> _loggerFactory;

    private readonly IMediator _mediator;

    private readonly IMetricService _metricService;

    private readonly PubSubEmulatorProcessFixture _pubSubFixture;

    private readonly IPublisherHandlerService _publisherHandlerService;

    private readonly EmailIntelligenceSubscriber _subscriber;

    private readonly int _subscriberTimeout;

    private readonly string _topicId;

    public EmailIntelligenceSubscriberHandleReceivedMessageAsyncTests(PubSubEmulatorProcessFixture pubSubFixture)
    {
        _logger = new MockLogger<EmailIntelligenceSubscriberHandleReceivedMessageAsyncTests>();
        _loggerFactory = new Mock<ILoggerFactory>();
        _loggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(_logger);
        _mediator = Substitute.For<IMediator>();
        _metricService = Substitute.For<IMetricService>();
        _pubSubFixture = pubSubFixture;
        _publisherHandlerService = Substitute.For<IPublisherHandlerService>();
        _subscriberTimeout = _pubSubFixture.SubscriberTimeout;
        _topicId = $"T_{Guid.NewGuid()}";

        var services = new ServiceCollection();

        services.AddSingleton<IDefaultJsonSerializerOptionsProvider, MyDefaultJsonSerializerOptionsProvider>();
        var serviceProvider = services.AddMemoryCache().BuildServiceProvider();
        var subscriptionId = $"{_topicId}.{Guid.NewGuid()}";
        var subscriberConfig = _pubSubFixture.CreateDefaultSubscriberConfig(subscriptionId);

        _subscriber = new EmailIntelligenceSubscriber(
            subscriberConfig,
            _loggerFactory.Object,
            _mediator,
            _metricService,
            _publisherHandlerService,
            serviceProvider
        );

        _pubSubFixture.CreateTopic(_topicId);
        _pubSubFixture.CreateSubscription(_topicId, subscriptionId);
    }

    [Theory]
    [MemberData(nameof(GetPossibleInvalidMessages))]
    public async Task HandleReceivedMessageAsync_ShouldLogWarning_WhenDataProfileIdCrmUserIdOrEmailIsEmptyOrNull(PubSubMessage<EmailIntelligenceMessage> emailIntelligenceMessage)
    {
        // Arrange
        var publisher = await _pubSubFixture.CreatePublisherAsync(_topicId);

        // Act
        await publisher.PublishAsync(JsonConvert.SerializeObject(emailIntelligenceMessage));
        await _subscriber.StartAsync(CancellationToken.None);
        await Task.Delay(_subscriberTimeout);
        await _subscriber.StopAsync(CancellationToken.None);

        var loggedMessages = _logger.GetAllMessages();

        // Assert
        Assert.Contains("A message with empty profile id, crm user id or email was received", loggedMessages);
        _metricService.Received(1).Increment(
            statName: Arg.Any<string>(),
            tags: Arg.Any<List<string>>()
        );
    }

    [Fact]
    public async Task HandleReceivedMessageAsync_ShouldLogError_WhenFailToHandleMessage()
    {
        // Arrange
        var publisher = await _pubSubFixture.CreatePublisherAsync(_topicId);
        var pubSubEmailIntelligenceMessage = EmailIntelligenceFaker.GetPubSubEmailIntelligenceMessageFake();

        _mediator.Send(pubSubEmailIntelligenceMessage.Data, CancellationToken.None).ThrowsForAnyArgs(new Exception());

        // Act
        await publisher.PublishAsync(JsonConvert.SerializeObject(pubSubEmailIntelligenceMessage));
        await _subscriber.StartAsync(CancellationToken.None);
        await Task.Delay(_subscriberTimeout);
        await _subscriber.StopAsync(CancellationToken.None);

        var loggedMessages = _logger.GetAllMessages();

        // Assert
        Assert.Contains(
            expectedSubstring: "Failed on handling the received message from",
            actualString: loggedMessages
        );
    }

    [Fact]
    public async Task HandleReceivedMessageAsync_ShouldCallMediator_WithPubSubEmailIntelligenceMessage()
    {
        // Arrange
        var publisher = await _pubSubFixture.CreatePublisherAsync(_topicId);
        var pubSubEmailIntelligenceMessage = EmailIntelligenceFaker.GetPubSubEmailIntelligenceMessageFake();
        
        _mediator.Send(pubSubEmailIntelligenceMessage.Data, CancellationToken.None).ReturnsForAnyArgs(new EmailIntelligenceResultMessage());

        // Act
        await publisher.PublishAsync(JsonConvert.SerializeObject(pubSubEmailIntelligenceMessage));
        await _subscriber.StartAsync(CancellationToken.None);
        await Task.Delay(_subscriberTimeout);
        await _subscriber.StopAsync(CancellationToken.None);

        // Assert
        await _mediator.Received(1).Send(
            request: Arg.Any<EmailIntelligenceMessage>(),
            cancellationToken: Arg.Any<CancellationToken>()
        );
        _metricService.Received(2).Increment(
            statName: Arg.Any<string>(), tags: Arg.Any<List<string>>()
        );
    }

    public static IEnumerable<object[]> GetPossibleInvalidMessages()
    {
        var faker = new Bogus.Faker();

        yield return new object[]
        {
            new PubSubMessage<EmailIntelligenceMessage>
            {
                Data = default!
            }
        };

        yield return new object[]
        {
            new PubSubMessage<EmailIntelligenceMessage>
            {
                Data = new EmailIntelligenceMessage{
                    Email = faker.Internet.Email(faker.Person.FirstName).ToLower(),
                    ProfileId = null
                }
            }
        };

        yield return new object[]
        {
            new PubSubMessage<EmailIntelligenceMessage>
            {
                Data = new EmailIntelligenceMessage{
                    Email = faker.Internet.Email(faker.Person.FirstName).ToLower(),
                    CrmUserId = null
                }
            }
        };

        yield return new object[]
        {
            new PubSubMessage<EmailIntelligenceMessage>
            {
                Data = new EmailIntelligenceMessage{
                    Email = faker.Internet.Email(faker.Person.FirstName).ToLower(),
                    ProfileId = string.Empty
                }
            }
        };

        yield return new object[]
        {
            new PubSubMessage<EmailIntelligenceMessage>
            {
                Data = new EmailIntelligenceMessage{
                    Email = null!,
                    ProfileId = faker.Random.Guid().ToString()
                }
            }
        };

        yield return new object[]
        {
            new PubSubMessage<EmailIntelligenceMessage>
            {
                Data = new EmailIntelligenceMessage{
                    Email = string.Empty,
                    ProfileId = faker.Random.Guid().ToString()
                }
            }
        };
    }
}
