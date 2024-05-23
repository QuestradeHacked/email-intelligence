using Microsoft.Extensions.Logging;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.Library.PubSubClientHelper.Primitives;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;

public class EmailIntelligenceSubscriberConfiguration : BaseSubscriberConfiguration<PubSubMessage<EmailIntelligenceMessage>>
{
    public override Task HandleMessageLogAsync(ILogger logger, LogLevel logLevel,PubSubMessage<EmailIntelligenceMessage> message, string logMessage, Exception? error = null, CancellationToken cancellationToken = default)
    {
        logger.Log(logLevel, error, "{LogMessage} - Message with: {MessageId}", logMessage, message.Id);

        return Task.CompletedTask;
    }

    public void Validate()
    {
        if (!Enable) return;

        if (string.IsNullOrWhiteSpace(ProjectId) || string.IsNullOrWhiteSpace(SubscriptionId))
        {
            throw new InvalidOperationException("The configuration options for the EmailIntelligenceSubscriber is not valid");
        }

        if (UseEmulator && string.IsNullOrWhiteSpace(Endpoint))
        {
            throw new InvalidOperationException("The emulator configuration options for EmailIntelligenceSubscriber is not valid");
        }
    }
}
