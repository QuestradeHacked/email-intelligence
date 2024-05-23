using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Faker;

public class EmailIntelligenceMessageFaker
{
    private readonly Bogus.Faker _faker;

    public EmailIntelligenceMessageFaker()
    {
        _faker = new Bogus.Faker();
    }

    public EmailIntelligenceMessage GetValidEmailIntelligenceMessage()
    {
        return new EmailIntelligenceMessage
        {
            Action = _faker.Random.Word(),
            City = _faker.Person.Address.City,
            Country = _faker.Address.Country(),
            CrmUserId = _faker.Random.Guid().ToString(),
            Email = _faker.Person.Email,
            EnterpriseProfileId = _faker.Random.Guid().ToString(),
            FirstName = _faker.Person.FirstName,
            Id = _faker.Random.Guid().ToString(),
            LastName = _faker.Person.LastName,
            PhoneNumber = _faker.Person.Phone,
            ProfileId = _faker.Random.Guid().ToString(),
            Source = _faker.Person.UserName,
            State = _faker.Address.State(),
            Street = _faker.Person.Address.Street,
            ZipCode = _faker.Person.Address.ZipCode
        };
    }
}
