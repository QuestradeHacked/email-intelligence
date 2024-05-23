using System.Diagnostics.CodeAnalysis;
using Questrade.FinCrime.Email.Intelligence.Domain.Models.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository.Models;
using Questrade.FinCrime.Email.Intelligence.Domain.Utils;

namespace Questrade.FinCrime.Email.Intelligence.Domain.Repository.Mapper;

[ExcludeFromCodeCoverage]
public static class LexisNexisMapper
{
    public static AttributeQuery MapAttributeQueryResponseToAttributeQuery(this GetLexisNexisAttributeQueryResponse response)
    {
        return new AttributeQuery(response.AccountEmail,
            new EmailAge(
                new EmailRiskScore
                {
                    BillAddressToFullNameConfidence = response.EmailAgeEmailRiskScoreBillAddressToFullNameConfidence,
                    BillAddressToLastNameConfidence = response.EmailAgeEmailRiskScoreBillAddressToLastNameConfidence,
                    BillRiskCountry = response.EmailAgeEmailRiskScoreBillRiskCountry,
                    Company = response.EmailAgeEmailRiskScoreCompany,
                    CorrelationId = response.EmailAgeEmailRiskScoreCorrelationId,
                    DomainAge = response.EmailAgeEmailRiskScoreDomainAge?.ToNullableDateTime(DateTime.Now),
                    DomainCategory = response.EmailAgeEmailRiskScoreDomainCategory,
                    DomainCompany = response.EmailAgeEmailRiskScoreDomainCompany,
                    DomainCorporate = response.EmailAgeEmailRiskScoreDomainCorporate,
                    DomainCountry = response.EmailAgeEmailRiskScoreDomainCountry,
                    DomainCountryMatch = response.EmailAgeEmailRiskScoreDomainCountryMatch,
                    DomainCreationDays = response.EmailAgeEmailRiskScoreDomainCreationDays?.ToNullableInt(),
                    DomainExists = response.EmailAgeEmailRiskScoreDomainExists,
                    DomainName = response.EmailAgeEmailRiskScoreDomainName,
                    DomainRelevantInfo = response.EmailAgeEmailRiskScoreDomainRelevantInfo,
                    DomainRelevantInfoId = response.EmailAgeEmailRiskScoreDomainRelevantInfoId?.ToNullableInt(),
                    DomainRiskCountry = response.EmailAgeEmailRiskScoreDomainRiskCountry,
                    DomainRiskLevel = response.EmailAgeEmailRiskScoreDomainRiskLevel,
                    DomainRiskLevelId = response.EmailAgeEmailRiskScoreDomainRiskLevelId?.ToNullableInt(),
                    EaAdvice = response.EmailAgeEmailRiskScoreEAAdvice,
                    EaAdviceId = response.EmailAgeEmailRiskScoreEAAdviceId?.ToNullableInt(),
                    EaReason = response.EmailAgeEmailRiskScoreEAReason,
                    EaReasonId = response.EmailAgeEmailRiskScoreEAReasonId?.ToNullableInt(),
                    EaRiskBand = response.EmailAgeEmailRiskScoreEARiskband,
                    EaRiskBandId = response.EmailAgeEmailRiskScoreEARiskbandId?.ToNullableInt(),
                    EaScore = response.EmailAgeEmailRiskScoreEAScore?.ToNullableInt(),
                    EaStatusId = response.EmailAgeEmailRiskScoreEAStatusId?.ToNullableInt(),
                    EmailExists = response.EmailAgeEmailRiskScoreEmailExists,
                    EmailToBillAddressConfidence = response.EmailAgeEmailRiskScoreEmailToBillAddressConfidence?.ToNullableInt(),
                    EmailToFullNameConfidence = response.EmailAgeEmailRiskScoreEmailToFullNameConfidence?.ToNullableInt(),
                    EmailToLastNameConfidence = response.EmailAgeEmailRiskScoreEmailToLastNameConfidence?.ToNullableInt(),
                    FirstSeenDays = response.EmailAgeEmailRiskScoreFirstSeenDays?.ToNullableInt(),
                    FirstVerificationDate = response.EmailAgeEmailRiskScoreFirstVerificationDate?.ToNullableDateTime(DateTime.Now),
                    FraudRisk = response.EmailAgeEmailRiskScoreFraudRisk,
                    NameMatch = response.EmailAgeEmailRiskScoreNameMatch,
                    OverallDigitalIdentityScore = response.EmailAgeEmailRiskScoreOverallDigitalIdentityScore?.ToNullableInt(),
                    ResponseStatus = new ResponseStatus(
                        response.EmailAgeEmailRiskScoreResponseStatusErrorCode?.ToNullableInt(),
                        response.EmailAgeEmailRiskScoreResponseStatus
                    ),
                    ShipForward = response.EmailAgeEmailRiskScoreShipForward,
                    Status = response.EmailAgeEmailRiskScoreStatus,
                    TotalHits = response.EmailAgeEmailRiskScoreTotalHits?.ToNullableInt(),
                    UniqueHits = response.EmailAgeEmailRiskScoreUniqueHits?.ToNullableInt(),
                }));
    }
}
