using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Integration.Faker;

public static class EmailIntelligenceFaker
{
    public static LexisNexisConfiguration GetLexisNexisConfigurationFake()
    {
        var faker = new Bogus.Faker();

        var generatedLexisNexisConfiguration = new LexisNexisConfiguration
        {
            OrgId = faker.Random.Guid().ToString(),
            ApiKey = faker.Random.Uuid().ToString(),
            Enable = true,

        };

        return generatedLexisNexisConfiguration;
    }

    public static EmailIntelligenceMessage GetEmailIntelligenceMessageFake()
    {
        var faker = new Bogus.Faker();

        var generatedEmailIntelligenceMessage = new EmailIntelligenceMessage
        {
            AccountNumber = faker.Random.Guid().ToString(),
            AccountStatusId = faker.Random.Int(),
            CrmUserId = faker.Random.Number().ToString(),
            EffectiveDate = faker.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now),
            Email = faker.Internet.Email(faker.Person.FirstName).ToLower(),
            EnterpriseProfileId = faker.Random.Guid().ToString(),
            ProfileId = faker.Random.Guid().ToString()
        };

        return generatedEmailIntelligenceMessage;
    }

    public static PubSubMessage<EmailIntelligenceMessage> GetPubSubEmailIntelligenceMessageFake()
    {
        var faker = new Bogus.Faker();

        var generatedPubSubEmailIntelligenceMessage = new PubSubMessage<EmailIntelligenceMessage>
        {
            Data = GetEmailIntelligenceMessageFake(),
            DataContentType = faker.System.MimeType(),
            Id = faker.Random.Uuid().ToString(),
            Source = faker.System.DirectoryPath(),
            SpecVersion = faker.System.Version().ToString(),
            Time = DateTime.Now,
            Type = faker.Internet.DomainName()
        };

        return generatedPubSubEmailIntelligenceMessage;
    }
}
