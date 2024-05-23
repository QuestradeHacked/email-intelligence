using MediatR;
using Microsoft.Extensions.Logging;
using Questrade.FinCrime.Email.Intelligence.Domain.Constants;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository;
using Questrade.FinCrime.Email.Intelligence.Domain.Services;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;

namespace Questrade.FinCrime.Email.Intelligence.Application.Handlers;

public class LexisNexisRequestHandler : IRequestHandler<EmailIntelligenceMessage, EmailIntelligenceResultMessage>
{
    private readonly ILogger<LexisNexisRequestHandler> _logger;

    private readonly IMetricService _metricService;

    private readonly LexisNexisConfiguration _lexisNexisConfiguration;

    private readonly ILexisNexisRepository _lexisNexisRepository;

    public LexisNexisRequestHandler(
        ILogger<LexisNexisRequestHandler> logger,
        IMetricService metricService,
        LexisNexisConfiguration lexisNexisConfiguration,
        ILexisNexisRepository lexisNexisRepository
    )
    {
        _logger = logger;
        _metricService = metricService;
        _lexisNexisConfiguration = lexisNexisConfiguration;
        _lexisNexisRepository = lexisNexisRepository;
    }

    public async Task<EmailIntelligenceResultMessage> Handle(EmailIntelligenceMessage emailIntelligenceMessage, CancellationToken cancellationToken)
    {
        if (!_lexisNexisConfiguration.Enable)
        {
            _logLexisNexisDisabledWarning(_logger, nameof(LexisNexisRequestHandler), emailIntelligenceMessage.Id, null);

            return null!;
        }

        _logLexisNexisStartHandler(_logger, nameof(LexisNexisRequestHandler), emailIntelligenceMessage.Id, null);

        try
        {
            _metricService.Increment(
                statName: MetricNames.LexisNexisApiCallRequestMessageCount,
                tags: new List<string> { MetricTags.StatusReceived }
            );

            var lexisNexisResult = await _lexisNexisRepository.GetLexisNexisAttributeQueryAsync(
                                        emailIntelligenceMessage.MapToLexisNexisAttributeQueryRequest(_lexisNexisConfiguration),
                                        cancellationToken
                                    );

            _metricService.Increment(
                statName: MetricNames.LexisNexisApiCallRequestMessageCount,
                tags: new List<string> { MetricTags.StatusSuccess }
            );

            return new EmailIntelligenceResultMessage
            {
                AccountNumber = emailIntelligenceMessage.AccountNumber,
                AccountStatusId = emailIntelligenceMessage.AccountStatusId,
                CrmUserId = emailIntelligenceMessage.CrmUserId,
                EffectiveDate = emailIntelligenceMessage.EffectiveDate,
                EnterpriseProfileId = emailIntelligenceMessage.EnterpriseProfileId,
                Id = emailIntelligenceMessage.Id,
                PhoneNumber = emailIntelligenceMessage.PhoneNumber,
                ProfileId = emailIntelligenceMessage.ProfileId,
                ScanResult = lexisNexisResult,
                Source = emailIntelligenceMessage.Source
            };
        }
        catch (Exception error)
        {
            _logLexisNexisError(_logger, emailIntelligenceMessage.Id, error);
            _metricService.Increment(
                statName: MetricNames.LexisNexisApiCallRequestMessageCount,
                tags: new List<string> { MetricTags.StatusPermanentError }
            );

            return null!;
        }
    }

    private readonly Action<ILogger, string?, string?, Exception?> _logLexisNexisDisabledWarning =
    LoggerMessage.Define<string?, string?>(
        eventId: new EventId(1, nameof(LexisNexisRequestHandler)),
        formatString: "LexisNexis not enabled for {LexisNexisRequestHandlerName} with {MessageId}",
        logLevel: LogLevel.Warning
    );

    private readonly Action<ILogger, string?, string?, Exception?> _logLexisNexisStartHandler =
            LoggerMessage.Define<string?, string?>(
                eventId: new EventId(2, nameof(LexisNexisRequestHandler)),
                formatString: "{repository} start for {messageId}",
                logLevel: LogLevel.Debug
            );

    private readonly Action<ILogger, string?, Exception?> _logLexisNexisError =
        LoggerMessage.Define<string?>(
            eventId: new EventId(3, nameof(LexisNexisRequestHandler)),
            formatString: "Failed on fetching phone number details from LexisNexis with {MessageId}",
            logLevel: LogLevel.Error
        );
}
