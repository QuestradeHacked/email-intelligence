using System.Diagnostics.CodeAnalysis;
using Questrade.FinCrime.Email.Intelligence.Domain.Constants;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;

[ExcludeFromCodeCoverage]
public class PubSubMetadataMessage<TData>
{
    public TData? Data { get; set; }

    public string? DataContentType { get; set; }

    public string? Id { get; set; }

    public string? Source { get; set; }

    public string? SpecVersion { get; set; }

    public DateTime? Time { get; set; }

    public string? Type { get; set; }

    public PubSubMetadataMessage()
    {
        DataContentType = PubSubMetadata.DataContentType;
        Id = Guid.NewGuid().ToString();
        Time = DateTime.UtcNow;
    }
}
