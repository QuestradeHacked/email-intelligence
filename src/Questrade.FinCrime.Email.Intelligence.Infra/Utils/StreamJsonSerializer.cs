using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Utils;

[ExcludeFromCodeCoverage]
public class StreamJsonSerializer
{
    private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public JsonSerializerOptions JsonSerializerOptions { get; set; }
    public StreamJsonSerializer(JsonSerializerOptions? jsonSerializerOptions = null)
    {
        JsonSerializerOptions = jsonSerializerOptions ?? DefaultJsonSerializerOptions;
    }

    public async Task<HttpContent> SerializeRequestAsync<TParam>(TParam param, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();

        await JsonSerializer.SerializeAsync(stream, param, JsonSerializerOptions, cancellationToken);

        stream.Seek(0, SeekOrigin.Begin);

        await stream.FlushAsync(cancellationToken);

        return new StreamContent(stream)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };
    }

    public async Task<T> DeserializeAsync<T>(HttpContent content)
    {
        await using var contentStream = await content.ReadAsStreamAsync();

        var response = await JsonSerializer.DeserializeAsync<T>(contentStream, JsonSerializerOptions);

        await contentStream.FlushAsync();

        return response!;
    }
}
