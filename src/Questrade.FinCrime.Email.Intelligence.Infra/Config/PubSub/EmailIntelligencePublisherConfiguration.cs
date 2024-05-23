using Microsoft.Extensions.Logging;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.Library.PubSubClientHelper.Primitives;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;

public class EmailIntelligencePublisherConfiguration : BasePublisherConfiguration<PubSubMessage<EmailIntelligenceResultMessage>>
{
    public override Task HandleMessageLogAsync(ILogger logger, LogLevel logLevel, PubSubMessage<EmailIntelligenceResultMessage> message, string logMessage, CancellationToken cancellationToken = default)
    {
        logger.Log(logLevel, "{LogMessage} - Message with: {MessageId}", logMessage, message.Id);

        return Task.CompletedTask;
    }

    public virtual void Validate()
    {
        if (!Enable) return;

        if (string.IsNullOrWhiteSpace(ProjectId))
        {
            throw new InvalidOperationException($"The ProjectId : {ProjectId} configuration options for the EmailIntelligencePublisher is not valid");
        }

        if (string.IsNullOrWhiteSpace(TopicId))
        {
            throw new InvalidOperationException($"The TopicId : {TopicId} configuration option for the EmailIntelligencePublisher is not valid");
        }

        if (UseEmulator && string.IsNullOrWhiteSpace(Endpoint))
        {
            throw new InvalidOperationException($"The Endpoint: {Endpoint} emulator configuration options for EmailIntelligencePublisher is not valid");
        }
    }
}
