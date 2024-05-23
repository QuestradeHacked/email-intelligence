using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.Library.PubSubClientHelper.Primitives;
using Questrade.Library.PubSubClientHelper.Publisher.Outbox;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Services.Publisher;

[ExcludeFromCodeCoverage]
public class EmailIntelligencePublisherService : OutboxPubsubPublisherService<EmailIntelligencePublisherConfiguration, PubSubMessage<EmailIntelligenceResultMessage>>
{
    public static string? SpecVersion { get; private set; }

    public static string? Type { get; private set; }

    public EmailIntelligencePublisherService(
        ILoggerFactory loggerFactory,
        EmailIntelligencePublisherConfiguration emailIntelligencePublisherConfiguration,
        IMessageOutboxStore<PubSubMessage<EmailIntelligenceResultMessage>> messageOutboxStore
    )
        : base(loggerFactory, emailIntelligencePublisherConfiguration, messageOutboxStore)
    {
        SpecVersion = string.Join("", emailIntelligencePublisherConfiguration.TopicId?.Split("-").LastOrDefault()?.Take(3) ?? Array.Empty<char>());
        Type = emailIntelligencePublisherConfiguration.TopicId;
    }
}
