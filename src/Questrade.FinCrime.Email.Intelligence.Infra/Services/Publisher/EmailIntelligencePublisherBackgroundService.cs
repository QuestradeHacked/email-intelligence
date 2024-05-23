using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.Library.PubSubClientHelper.Primitives;
using Questrade.Library.PubSubClientHelper.Publisher.Outbox;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Services.Publisher;

[ExcludeFromCodeCoverage]
public class EmailIntelligencePublisherBackgroundService : PubsubPublisherBackgroundService<EmailIntelligencePublisherConfiguration, PubSubMessage<EmailIntelligenceResultMessage>>
{
    public EmailIntelligencePublisherBackgroundService(
        ILoggerFactory loggerFactory,
        IMemoryCache memoryCache,
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration,
        IMessageOutboxStore<PubSubMessage<EmailIntelligenceResultMessage>> messageOutboxStore,
        IPublisherService<PubSubMessage<EmailIntelligenceResultMessage>> publisherService,
        IDefaultJsonSerializerOptionsProvider defaultJsonSerializerOptionsProvider,
        IServiceProvider serviceProvider,
        IHostApplicationLifetime hostApplicationLifetime
    )
        : base(
            loggerFactory,
            memoryCache,
            emailIntelligencePublisherConfiguration,
            messageOutboxStore,
            publisherService,
            defaultJsonSerializerOptionsProvider,
            serviceProvider,
            hostApplicationLifetime
        )
    { }
}
