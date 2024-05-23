using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Questrade.FinCrime.Email.Intelligence.Domain.Repository.Models;

[ExcludeFromCodeCoverage]
public class GetLexisNexisAttributeQueryRequest
{
    [JsonPropertyName("account_address_city")]
    public string? AccountAddressCity { get; set; }

    [JsonPropertyName("account_address_country")]
    public string? AccountAddressCountry { get; set; }

    [JsonPropertyName("account_address_state")]
    public string? AccountAddressState { get; set; }

    [JsonPropertyName("account_address_street1")]
    public string? AccountAddressStreet { get; set; }

    [JsonPropertyName("account_address_zip")]
    public string? AccountAddressZip { get; set; }

    [JsonPropertyName("account_email")]
    public string AccountEmail { get; set; } = default!;

    [JsonPropertyName("account_first_name")]
    public string? AccountFirstName { get; set; }

    [JsonPropertyName("account_last_name")]
    public string? AccountLastName { get; set; }

    [JsonPropertyName("local_attrib_2")]
    public string? CrmUserId { get; set; }

    [JsonPropertyName("output_format")]
    public string OutputFormat { get; set; } = default!;

    [JsonPropertyName("account_telephone")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("policy")]
    public string Policy { get; set; } = default!;

    [JsonPropertyName("service_type")]
    public string ServiceType { get; set; } = default!;

    [JsonPropertyName("local_attrib_1")]
    public string? Source { get; set; }
}
