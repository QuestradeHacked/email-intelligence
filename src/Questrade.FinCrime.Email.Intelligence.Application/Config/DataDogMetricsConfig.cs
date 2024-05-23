using System.Diagnostics.CodeAnalysis;

namespace Questrade.FinCrime.Email.Intelligence.Application.Config;

[ExcludeFromCodeCoverage]
public class DataDogMetricsConfig
{
    public string HostName { get; set; } = default!;

    public string Prefix { get; set; } = default!;
}
