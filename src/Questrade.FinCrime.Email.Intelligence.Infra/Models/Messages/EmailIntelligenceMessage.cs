using MediatR;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository.Models;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;

public class EmailIntelligenceMessage : IRequest<EmailIntelligenceResultMessage>
{
    public string? AccountNumber { get; set; }

    public int? AccountStatusId { get; set; }

    public string? Action { get; set; } = default!;

    public string? City { get; set; } = default!;

    public string? Country { get; set; } = default!;

    public string? CrmUserId { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public string? Email { get; set; } = default!;

    public string? EnterpriseProfileId { get; set; }

    public string? FirstName { get; set; } = default!;

    public string? Id { get; set; } = default!;

    public string? LastName { get; set; } = default!;

    public string? PhoneNumber { get; set; } = default!;

    public string? ProfileId { get; set; }

    public string? Source { get; set; }

    public string? State { get; set; } = default!;

    public string? Street { get; set; } = default!;

    public string? ZipCode { get; set; } = default!;

    public GetLexisNexisAttributeQueryRequest MapToLexisNexisAttributeQueryRequest(LexisNexisConfiguration lexisNexisConfig)
    {
        var request = new GetLexisNexisAttributeQueryRequest
        {
            AccountEmail = Email!,
            CrmUserId = CrmUserId,
            OutputFormat = lexisNexisConfig.OutputFormat,
            PhoneNumber = PhoneNumber,
            Policy = lexisNexisConfig.PolicyName,
            ServiceType = lexisNexisConfig.ServiceType,
            Source = Source
        };

        if (!lexisNexisConfig.SendPIIData)
        {
            return request;
        }

        request.AccountAddressCity = City;
        request.AccountAddressCountry = Country;
        request.AccountAddressState = State;
        request.AccountAddressStreet = Street;
        request.AccountAddressZip = ZipCode;
        request.AccountFirstName = FirstName;
        request.AccountLastName = LastName;
        return request;
    }
}
