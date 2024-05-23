using Questrade.FinCrime.Email.Intelligence.Domain.Models.LexisNexis;

namespace Questrade.FinCrime.Email.Intelligence.Domain.Models;

public abstract class EmailIntelligenceResult
{
    public string? AccountNumber { get; set; }

    public int? AccountStatusId { get; set; }

    public string? CrmUserId { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public string? EnterpriseProfileId { get; set; }

    public string? Id { get; set; }

    public string? PhoneNumber { get; set; }

    public string? ProfileId { get; set; }

    public AttributeQuery? ScanResult { get; set; }

    public string? Source { get; set; }
}
