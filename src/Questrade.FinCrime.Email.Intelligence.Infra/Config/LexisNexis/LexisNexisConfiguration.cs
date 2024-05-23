namespace Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;

public class LexisNexisConfiguration
{
    public string ApiKey { get; set; } = default!;
    public string AttributeQuery { get; set; } = default!;
    public string BaseAddress { get; set; } = default!;
    public bool Enable { get; set; }
    public string OrgId { get; set; } = default!;
    public string OutputFormat { get; set; } = default!;
    public string PolicyName { get; set; } = default!;
    public Resilience Resilience { get; set; } = default!;
    public bool SendPIIData { get; set; } = false;
    public string ServiceType { get; set; } = default!;

    public string GetAllAttributeQueryUrl()
    {
        return string.IsNullOrEmpty(BaseAddress) || string.IsNullOrEmpty(AttributeQuery) ? string.Empty : $"{BaseAddress}/{AttributeQuery}";
    }

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(OrgId)
            || string.IsNullOrWhiteSpace(ApiKey)
            || string.IsNullOrWhiteSpace(GetAllAttributeQueryUrl())
            || string.IsNullOrWhiteSpace(OutputFormat)
            || string.IsNullOrWhiteSpace(PolicyName)
            || string.IsNullOrWhiteSpace(ServiceType))
        {
            throw new InvalidOperationException("The configuration options for the email intelligence LexisNexisConfiguration is not valid. Please check vault secrets");
        }
    }
}
