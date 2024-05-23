using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Questrade.FinCrime.Email.Intelligence.Domain.Repository.Models;

[ExcludeFromCodeCoverage]
public class GetLexisNexisAttributeQueryResponse
{
    [JsonPropertyName("account_email")]
    public string AccountEmail { get; set; } = default!;

    [JsonPropertyName("emailage.emailriskscore.billaddresstofullnameconfidence")]
    public string? EmailAgeEmailRiskScoreBillAddressToFullNameConfidence { get; set; }

    [JsonPropertyName("emailage.emailriskscore.billaddresstolastnameconfidence")]
    public string? EmailAgeEmailRiskScoreBillAddressToLastNameConfidence { get; set; }

    [JsonPropertyName("emailage.emailriskscore.billriskcountry")]
    public string? EmailAgeEmailRiskScoreBillRiskCountry { get; set; }

    [JsonPropertyName("emailage.emailriskscore.company")]
    public string? EmailAgeEmailRiskScoreCompany { get; set; }

    [JsonPropertyName("emailage.emailriskscore.correlationid")]
    public string? EmailAgeEmailRiskScoreCorrelationId { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domainage")]
    public string? EmailAgeEmailRiskScoreDomainAge { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domaincategory")]
    public string? EmailAgeEmailRiskScoreDomainCategory { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domaincompany")]
    public string? EmailAgeEmailRiskScoreDomainCompany { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domaincorporate")]
    public string? EmailAgeEmailRiskScoreDomainCorporate { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domaincountry")]
    public string? EmailAgeEmailRiskScoreDomainCountry { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domaincountrymatch")]
    public string? EmailAgeEmailRiskScoreDomainCountryMatch { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domain_creation_days")]
    public string? EmailAgeEmailRiskScoreDomainCreationDays { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domainexists")]
    public string? EmailAgeEmailRiskScoreDomainExists { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domainname")]
    public string? EmailAgeEmailRiskScoreDomainName { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domainrelevantinfo")]
    public string? EmailAgeEmailRiskScoreDomainRelevantInfo { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domainrelevantinfoid")]
    public string? EmailAgeEmailRiskScoreDomainRelevantInfoId { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domainriskcountry")]
    public string? EmailAgeEmailRiskScoreDomainRiskCountry { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domainrisklevel")]
    public string? EmailAgeEmailRiskScoreDomainRiskLevel { get; set; }

    [JsonPropertyName("emailage.emailriskscore.domainrisklevelid")]
    public string? EmailAgeEmailRiskScoreDomainRiskLevelId { get; set; }

    [JsonPropertyName("emailage.emailriskscore.eaadvice")]
    public string? EmailAgeEmailRiskScoreEAAdvice { get; set; }

    [JsonPropertyName("emailage.emailriskscore.eaadviceid")]
    public string? EmailAgeEmailRiskScoreEAAdviceId { get; set; }

    [JsonPropertyName("emailage.emailriskscore.eareason")]
    public string? EmailAgeEmailRiskScoreEAReason { get; set; }

    [JsonPropertyName("emailage.emailriskscore.eareasonid")]
    public string? EmailAgeEmailRiskScoreEAReasonId { get; set; }

    [JsonPropertyName("emailage.emailriskscore.eariskband")]
    public string? EmailAgeEmailRiskScoreEARiskband { get; set; }

    [JsonPropertyName("emailage.emailriskscore.eariskbandid")]
    public string? EmailAgeEmailRiskScoreEARiskbandId { get; set; }

    [JsonPropertyName("emailage.emailriskscore.eascore")]
    public string? EmailAgeEmailRiskScoreEAScore { get; set; }

    [JsonPropertyName("emailage.emailriskscore.eastatusid")]
    public string? EmailAgeEmailRiskScoreEAStatusId { get; set; }

    [JsonPropertyName("emailage.emailriskscore.emailexists")]
    public string? EmailAgeEmailRiskScoreEmailExists { get; set; }

    [JsonPropertyName("emailage.emailriskscore.emailtobilladdressconfidence")]
    public string? EmailAgeEmailRiskScoreEmailToBillAddressConfidence { get; set; }

    [JsonPropertyName("emailage.emailriskscore.emailtofullnameconfidence")]
    public string? EmailAgeEmailRiskScoreEmailToFullNameConfidence { get; set; }

    [JsonPropertyName("emailage.emailriskscore.emailtolastnameconfidence")]
    public string? EmailAgeEmailRiskScoreEmailToLastNameConfidence { get; set; }

    [JsonPropertyName("emailage.emailriskscore.first_seen_days")]
    public string? EmailAgeEmailRiskScoreFirstSeenDays { get; set; }

    [JsonPropertyName("emailage.emailriskscore.firstverificationdate")]
    public string? EmailAgeEmailRiskScoreFirstVerificationDate { get; set; }

    [JsonPropertyName("emailage.emailriskscore.fraudrisk")]
    public string? EmailAgeEmailRiskScoreFraudRisk { get; set; }

    [JsonPropertyName("emailage.emailriskscore.namematch")]
    public string? EmailAgeEmailRiskScoreNameMatch { get; set; }

    [JsonPropertyName("emailage.emailriskscore.overalldigitalidentityscore")]
    public string? EmailAgeEmailRiskScoreOverallDigitalIdentityScore { get; set; }

    [JsonPropertyName("emailage.emailriskscore.responsestatus.errorcode")]
    public string? EmailAgeEmailRiskScoreResponseStatusErrorCode { get; set; }

    [JsonPropertyName("emailage.emailriskscore.responsestatus.status")]
    public string? EmailAgeEmailRiskScoreResponseStatus { get; set; }

    [JsonPropertyName("emailage.emailriskscore.shipforward")]
    public string? EmailAgeEmailRiskScoreShipForward { get; set; }

    [JsonPropertyName("emailage.emailriskscore.status")]
    public string? EmailAgeEmailRiskScoreStatus { get; set; }

    [JsonPropertyName("emailage.emailriskscore.totalhits")]
    public string? EmailAgeEmailRiskScoreTotalHits { get; set; }

    [JsonPropertyName("emailage.emailriskscore.uniquehits")]
    public string? EmailAgeEmailRiskScoreUniqueHits { get; set; }

    [JsonPropertyName("emailage.emailriskscore.phone_status")]
    public string? EmailAgeEmailRiskScorePhoneStatus { get; set; }

    [JsonPropertyName("request_duration")]
    public string? RequestDuration { get; set; }

    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }

    [JsonPropertyName("request_result")]
    public string? RequestResult { get; set; }

    [JsonPropertyName("review_status")]
    public string? ReviewStatus { get; set; }

    [JsonPropertyName("risk_rating")]
    public string? RiskRating { get; set; }

    [JsonPropertyName("secondary_industry")]
    public string? SecondaryIndustry { get; set; }

    [JsonPropertyName("service_type")]
    public string? ServiceType { get; set; }

    [JsonPropertyName("tps_error")]
    public string? TpsError { get; set; }

    [JsonPropertyName("tps_was_timeout")]
    public string? TpsWasTimeout { get; set; }
}
