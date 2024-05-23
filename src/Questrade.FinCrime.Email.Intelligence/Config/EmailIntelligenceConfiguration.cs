using Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;

namespace Questrade.FinCrime.Email.Intelligence.Config;

public class EmailIntelligenceConfiguration
{
    public EmailIntelligencePublisherConfiguration EmailIntelligencePublisherConfiguration { get; set; } = new();

    public EmailIntelligenceSubscriberConfiguration EmailIntelligenceSubscriberConfiguration { get; set; } = new();

    public LexisNexisConfiguration LexisNexisConfiguration { get; set; } = new();

    internal void Validate()
    {
        if (EmailIntelligencePublisherConfiguration == null)
        {
            throw new InvalidOperationException(message: "Email Intelligence Publisher configuration is not valid.");
        }

        EmailIntelligencePublisherConfiguration.Validate();

        if (EmailIntelligenceSubscriberConfiguration == null)
        {
            throw new InvalidOperationException(message: "Email Intelligence Subscriber configuration is not valid.");
        }

        EmailIntelligenceSubscriberConfiguration.Validate();

        if (LexisNexisConfiguration == null)
        {
            throw new InvalidOperationException("LexisNexis configuration not found.");
        }

        LexisNexisConfiguration.Validate();
    }
}
