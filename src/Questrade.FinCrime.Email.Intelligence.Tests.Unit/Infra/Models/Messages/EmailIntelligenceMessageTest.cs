using FluentAssertions;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.FinCrime.Email.Intelligence.Tests.Unit.Faker;
using Xunit;

namespace Questrade.FinCrime.Email.Intelligence.Tests.Unit.Infra.Models.Messages;

public class EmailIntelligenceMessageTest
{
    private readonly LexisNexisConfiguration _lexisNexisConfiguration;
    private readonly EmailIntelligenceMessage _emailIntelligenceMessage;

    public EmailIntelligenceMessageTest()
    {
        var lexisNexisFaker = new LexisNexisFaker();
        var emailIntelligenceMessageFaker = new EmailIntelligenceMessageFaker();

        _lexisNexisConfiguration = lexisNexisFaker.GetValidLexisNexisConfiguration();
        _emailIntelligenceMessage = emailIntelligenceMessageFaker.GetValidEmailIntelligenceMessage();
    }

    [Fact]
    public void MapToLexisNexisAttributeQueryRequest_ShouldNotReturnAddressInfo_WhenSendPIIDataIsFalse()
    {
        // Arrange
        _lexisNexisConfiguration.SendPIIData = false;

        // Act
        var queryRequest = _emailIntelligenceMessage.MapToLexisNexisAttributeQueryRequest(_lexisNexisConfiguration);

        // Assert
        queryRequest.AccountEmail.Should().Be(_emailIntelligenceMessage.Email);
        queryRequest.CrmUserId.Should().Be(_emailIntelligenceMessage.CrmUserId);
        queryRequest.OutputFormat.Should().Be(_lexisNexisConfiguration.OutputFormat);
        queryRequest.Policy.Should().Be(_lexisNexisConfiguration.PolicyName);
        queryRequest.ServiceType.Should().Be(_lexisNexisConfiguration.ServiceType);
        queryRequest.Source.Should().Be(_emailIntelligenceMessage.Source);

        queryRequest.AccountAddressCity.Should().BeNull();
        queryRequest.AccountAddressCountry.Should().BeNull();
        queryRequest.AccountAddressState.Should().BeNull();
        queryRequest.AccountAddressStreet.Should().BeNull();
        queryRequest.AccountAddressZip.Should().BeNull();
        queryRequest.AccountFirstName.Should().BeNull();
        queryRequest.AccountLastName.Should().BeNull();
    }

    [Fact]
    public void MapToLexisNexisAttributeQueryRequest_ShouldReturnAddressInfo_WhenSendPIIDataIsTrue()
    {
        // Arrange
        _lexisNexisConfiguration.SendPIIData = true;

        // Act
        var queryRequest = _emailIntelligenceMessage.MapToLexisNexisAttributeQueryRequest(_lexisNexisConfiguration);

        // Assert
        queryRequest.AccountEmail.Should().Be(_emailIntelligenceMessage.Email);
        queryRequest.CrmUserId.Should().Be(_emailIntelligenceMessage.CrmUserId);
        queryRequest.OutputFormat.Should().Be(_lexisNexisConfiguration.OutputFormat);
        queryRequest.Policy.Should().Be(_lexisNexisConfiguration.PolicyName);
        queryRequest.ServiceType.Should().Be(_lexisNexisConfiguration.ServiceType);
        queryRequest.Source.Should().Be(_emailIntelligenceMessage.Source);

        queryRequest.AccountAddressCity.Should().Be(_emailIntelligenceMessage.City);
        queryRequest.AccountAddressCountry.Should().Be(_emailIntelligenceMessage.Country);
        queryRequest.AccountAddressState.Should().Be(_emailIntelligenceMessage.State);
        queryRequest.AccountAddressStreet.Should().Be(_emailIntelligenceMessage.Street);
        queryRequest.AccountAddressZip.Should().Be(_emailIntelligenceMessage.ZipCode);
        queryRequest.AccountFirstName.Should().Be(_emailIntelligenceMessage.FirstName);
        queryRequest.AccountLastName.Should().Be(_emailIntelligenceMessage.LastName);
    }
}
