using System.Diagnostics.CodeAnalysis;

namespace Questrade.FinCrime.Email.Intelligence.Domain.Models.LexisNexis;

[ExcludeFromCodeCoverage]
public class EmailRiskScore
{
    public string? BillAddressToFullNameConfidence { get; set; }

    public string? BillAddressToLastNameConfidence { get; set; }

    public string? BillRiskCountry { get; set; }

    public string? Company { get; set; }

    public string? CorrelationId { get; set; }

    public DateTime? DomainAge { get; set; }

    public string? DomainCategory { get; set; }

    public string? DomainCompany { get; set; }

    public string? DomainCorporate { get; set; }

    public string? DomainCountry { get; set; }

    public string? DomainCountryMatch { get; set; }

    public int? DomainCreationDays { get; set; }

    public string? DomainExists { get; set; }

    public string? DomainName { get; set; }

    public string? DomainRelevantInfo { get; set; }

    public int? DomainRelevantInfoId { get; set; }

    public string? DomainRiskCountry { get; set; }

    public string? DomainRiskLevel { get; set; }

    public int? DomainRiskLevelId { get; set; }

    public string? EaAdvice { get; set; }

    public int? EaAdviceId { get; set; }

    public string? EaReason { get; set; }

    public int? EaReasonId { get; set; }

    public string? EaRiskBand { get; set; }

    public int? EaRiskBandId { get; set; }

    public int? EaScore { get; set; }

    public int? EaStatusId { get; set; }

    public string? EmailExists { get; set; }

    public int? EmailToBillAddressConfidence { get; set; }

    public int? EmailToFullNameConfidence { get; set; }

    public int? EmailToLastNameConfidence { get; set; }

    public int? FirstSeenDays { get; set; }

    public DateTime? FirstVerificationDate { get; set; }

    public string? FraudRisk { get; set; }

    public string? NameMatch { get; set; }

    public int? OverallDigitalIdentityScore { get; set; }

    public ResponseStatus? ResponseStatus { get; set; }

    public string? ShipForward { get; set; }

    public string? Status { get; set; }

    public int? TotalHits { get; set; }

    public int? UniqueHits { get; set; }
}
