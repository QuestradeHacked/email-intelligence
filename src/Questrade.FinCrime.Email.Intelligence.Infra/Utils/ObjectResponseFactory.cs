using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Utils;

[ExcludeFromCodeCoverage]
public static class ObjectResponseFactory
{
    public static async Task<T> GetSuccessResponseAsync<T>(this HttpResponseMessage message, ILogger logger, CancellationToken cancellationToken)
    {
        try
        {
            await using var content = await message.Content.ReadAsStreamAsync(cancellationToken);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var response = await JsonSerializer.DeserializeAsync<T>(content, options, cancellationToken);

            await content.FlushAsync(cancellationToken);

            return response!;
        }
        catch (Exception exception)
        {
            _logCriticalError(logger, typeof(T), exception);
            return default!;
        }
    }

    private static readonly Action<ILogger, Type, Exception?> _logCriticalError =
        LoggerMessage.Define<Type>(
            eventId: new EventId(1, nameof(ObjectResponseFactory)),
            formatString: "Error encountered while attempting to convert JSON response to {type}.",
            logLevel: LogLevel.Critical
        );
}
