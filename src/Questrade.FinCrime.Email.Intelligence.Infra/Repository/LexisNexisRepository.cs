using System.Net;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Questrade.FinCrime.Email.Intelligence.Domain.Constants;
using Questrade.FinCrime.Email.Intelligence.Domain.Exceptions;
using Questrade.FinCrime.Email.Intelligence.Domain.Models.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository.Mapper;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository.Models;
using Questrade.FinCrime.Email.Intelligence.Domain.Services;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Infra.Utils;
using SerilogTimings;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Repository;

public class LexisNexisRepository : ILexisNexisRepository
{
    private readonly LexisNexisConfiguration _configuration;
    private readonly ILogger<LexisNexisRepository> _logger;
    private readonly HttpClient _httpClient;
    private readonly IMetricService _metricService;

    public LexisNexisRepository(IOptions<LexisNexisConfiguration> configuration,
        ILogger<LexisNexisRepository> logger,
        HttpClient httpClient,
        IMetricService metricService)
    {
        _httpClient = httpClient;
        _configuration = configuration.Value;
        _logger = logger;
        _metricService = metricService;
    }
    public async Task<AttributeQuery?> GetLexisNexisAttributeQueryAsync(GetLexisNexisAttributeQueryRequest request, CancellationToken cancellationToken)
    {
        var url = _configuration.GetAllAttributeQueryUrl();
        var httpRequestMessage = await HttpRequestMessageBuilder
            .CreateRequest()
            .WithMethod(HttpMethod.Post)
            .WithOrgIdAndApiKey(_configuration.OrgId, _configuration.ApiKey)
            .WithUri(url)
            .WithParamsAsync(request, cancellationToken);

        _logLexisNexisStartRepository(_logger, nameof(LexisNexisRepository), nameof(GetLexisNexisAttributeQueryAsync), request.AccountEmail, null);

        using var timing = Operation.Begin("Timing for {query} - {accountEmail}", nameof(GetLexisNexisAttributeQueryAsync), request.AccountEmail);

        var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage, cancellationToken);

        if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
        {
            _logLexisNexisIntegrationError(_logger, httpResponseMessage.StatusCode.ToString(), null);

            throw new LexisNexisException();
        }

        var response = await httpResponseMessage
            .GetSuccessResponseAsync<GetLexisNexisAttributeQueryResponse>(_logger, cancellationToken);

        if (response?.RequestResult != "success")
        {
            _logLexisNexisResultError(_logger, response?.RequestResult, response, null);

            throw new LexisNexisException();
        }

        timing.Complete();

        _metricService.Distribution(
            MetricNames.LexisNexisApiCallLatency,
            timing.Elapsed.TotalMilliseconds,
            new List<string>{ MetricTags.StatusSuccess }
        );

        return response.MapAttributeQueryResponseToAttributeQuery();
    }

    private readonly Action<ILogger, string?, string?, string?, Exception?> _logLexisNexisStartRepository =
        LoggerMessage.Define<string?, string?, string?>(
            eventId: new EventId(1, nameof(LexisNexisRepository)),
            formatString: "{repository} start method {method} for {request}",
            logLevel: LogLevel.Debug
        );

    private readonly Action<ILogger, string?, Exception?> _logLexisNexisIntegrationError =
        LoggerMessage.Define<string?>(
            eventId: new EventId(2, nameof(LexisNexisRepository)),
            formatString: "Error encountered during integration with LexisNexis Attribute-Query API. StatusCode: {statusCode}",
            logLevel: LogLevel.Error
        );

    private readonly Action<ILogger, string?, GetLexisNexisAttributeQueryResponse?, Exception?> _logLexisNexisResultError =
        LoggerMessage.Define<string?, GetLexisNexisAttributeQueryResponse?>(
            eventId: new EventId(3, nameof(LexisNexisRepository)),
            formatString: "The response from the LexisNexis Attribute-Query API returned an error status {requestResult}. Response: {response}",
            logLevel: LogLevel.Error
        );
}
