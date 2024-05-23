using System.Diagnostics.CodeAnalysis;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;

[ExcludeFromCodeCoverage]
public class Resilience
{
    public int ConsecutiveExceptionsAllowedBeforeBreaking { get; set; } = 5;
    public int DurationOfBreakInSeconds { get; set; } = 15;
    public int RetryCount { get; set; } = 3;
}
