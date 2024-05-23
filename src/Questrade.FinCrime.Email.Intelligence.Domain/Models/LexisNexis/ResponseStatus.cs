using System.Diagnostics.CodeAnalysis;

namespace Questrade.FinCrime.Email.Intelligence.Domain.Models.LexisNexis;

[ExcludeFromCodeCoverage]
public record ResponseStatus(int? ErrorCode, string? Status);
