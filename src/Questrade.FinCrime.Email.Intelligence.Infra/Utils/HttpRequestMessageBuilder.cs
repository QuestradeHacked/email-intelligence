using System.Diagnostics.CodeAnalysis;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Utils;

[ExcludeFromCodeCoverage]
public static class HttpRequestMessageBuilder
{
    public static async Task<HttpRequestMessage> WithParamsAsync<TClass>(
        this HttpRequestMessage httpRequestMessage, TClass parameters, CancellationToken cancellationToken) where TClass : class
    {
        var httpContent = await new StreamJsonSerializer().SerializeRequestAsync(parameters, cancellationToken);
        httpRequestMessage.Content = httpContent;
        return httpRequestMessage;
    }

    public static HttpRequestMessage WithMethod(this HttpRequestMessage httpRequestMessage, HttpMethod method)
    {
        httpRequestMessage.Method = method;
        return httpRequestMessage;
    }

    public static HttpRequestMessage WithUri(this HttpRequestMessage httpRequestMessage, string url)
    {
        httpRequestMessage.RequestUri = new Uri(url, UriKind.Absolute);
        return httpRequestMessage;
    }

    public static HttpRequestMessage WithOrgIdAndApiKey(this HttpRequestMessage httpRequestMessage, string orgId, string apiKey)
    {
        httpRequestMessage.Headers.Add("x-org-id", orgId);
        httpRequestMessage.Headers.Add("x-api-key", apiKey);

        return httpRequestMessage;
    }

    public static HttpRequestMessage CreateRequest() => new();
}
