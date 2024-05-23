using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Services.Publisher;

public interface IPublisherHandlerService
{
    Task Publish(EmailIntelligenceResultMessage message, CancellationToken cancellationToken);
}
