using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Faker;

public class LexisNexisFaker
{
    private readonly Bogus.Faker _faker;

    public LexisNexisFaker()
    {
        _faker = new Bogus.Faker();
    }

    public LexisNexisConfiguration GetValidLexisNexisConfiguration()
    {
        return new LexisNexisConfiguration
        {
            OrgId = _faker.Random.Guid().ToString(),
            OutputFormat = _faker.Random.Word(),
            PolicyName = _faker.Random.Word(),
            ServiceType = _faker.Random.Word(),
            ApiKey = _faker.Random.Uuid().ToString(),
            AttributeQuery = _faker.Random.Word(),
            BaseAddress = _faker.Random.Word(),
            Enable = true,

        };
    }
}
