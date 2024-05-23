using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Questrade.FinCrime.Email.Intelligence.Domain.Constants;
using Questrade.FinCrime.Email.Intelligence.Domain.Services;
using Questrade.FinCrime.Email.Intelligence.Domain.Utils;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.FinCrime.Email.Intelligence.Infra.Services.Publisher;
using Questrade.Library.PubSubClientHelper.Subscriber;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Services.Subscriber;

public class EmailIntelligenceSubscriber : PubsubSubscriberBackgroundService<EmailIntelligenceSubscriberConfiguration, PubSubMessage<EmailIntelligenceMessage>>
{
    private readonly IMediator _mediator;

    private readonly IMetricService _metricService;

    private readonly IPublisherHandlerService _publisherService;

    private readonly IServiceProvider _serviceProvider;

    public EmailIntelligenceSubscriber(
        EmailIntelligenceSubscriberConfiguration subscriberConfiguration,
        ILoggerFactory loggerFactory,
        IMediator mediator,
        IMetricService metricService,
        IPublisherHandlerService publisherService,
        IServiceProvider serviceProvider)
        : base(loggerFactory, subscriberConfiguration, serviceProvider)
    {
        _mediator = mediator;
        _metricService = metricService;
        _publisherService = publisherService;
        _serviceProvider = serviceProvider;

        ReportMessageReceived += OnReportMessageReceived;
    }

    protected override async Task<bool> HandleReceivedMessageAsync(PubSubMessage<EmailIntelligenceMessage> message, CancellationToken cancellationToken)
    {
        _logDefineScope(Logger, nameof(EmailIntelligenceSubscriber), nameof(HandleReceivedMessageAsync));
        _logMessageReceivedInformation(Logger, message.Id, null);

        _metricService.Increment(MetricNames.EmailIntelligenceReceivedMessageCount, new List<string> { MetricTags.StatusSuccess });

        if ((string.IsNullOrEmpty(message.Data?.ProfileId) && string.IsNullOrEmpty(message.Data?.CrmUserId)) || string.IsNullOrEmpty(message.Data.Email))
        {
            _logMissingRequiredInformationWarning(Logger, null);

            return true;
        }

        FillSourceIntoData(message);

        try
        {
            var emailIntelligenceScanResult = await _mediator.Send<EmailIntelligenceResultMessage?>(message.Data, cancellationToken);

            if (emailIntelligenceScanResult is null)
            {
                return false;
            }

            await _publisherService.Publish(emailIntelligenceScanResult, cancellationToken);

            _metricService.Increment(MetricNames.EmailIntelligenceHandleMessageCount, new List<string> { MetricTags.StatusSuccess });

            _logMessageProcessedMessage(Logger, message.Id, null);

            return true;
        }
        catch (Exception error)
        {
            _logHandlingMessageError(Logger, nameof(EmailIntelligenceSubscriber), message.Id, error);

            _metricService.Increment(MetricNames.EmailIntelligenceHandleMessageCount, new List<string> { MetricTags.StatusPermanentError });

            return false;
        }
    }

    private static void FillSourceIntoData(PubSubMessage<EmailIntelligenceMessage> message)
    {
        message.Data.Source = message.Source;
    }

    private void OnReportMessageReceived(object? sender, Library.PubSubClientHelper.Events.MessageReceivedEventArgs e)
    {
        if (e.Message?.Attributes.ContainsKey("tr-id") == false || e.Message?.Attributes["tr-id"] is null)
        {
            return;
        }

        using var scope = _serviceProvider.CreateScope();
        var correlationContext = scope.ServiceProvider.GetRequiredService<CorrelationContext>();

        correlationContext.AddLogScopeForSubscriber(Logger, e.Message.Attributes["tr-id"], e.Message.MessageId);
    }

    private readonly Func<ILogger, string?, string?, IDisposable> _logDefineScope =
        LoggerMessage.DefineScope<string?, string?>(
            formatString: "{EmailIntelligenceSubscriberName}:{HandleReceivedMessageAsyncName}"
        );

    private readonly Action<ILogger, string?, Exception?> _logMessageReceivedInformation =
        LoggerMessage.Define<string?>(
            eventId: new EventId(1, nameof(EmailIntelligenceSubscriber)),
            formatString: "Message Received: {MessageId}",
            logLevel: LogLevel.Information
        );

    private readonly Action<ILogger, string?, Exception?> _logMessageProcessedMessage =
    LoggerMessage.Define<string?>(
        eventId: new EventId(2, nameof(EmailIntelligenceSubscriber)),
        formatString: "Message Processed: {MessageId}",
        logLevel: LogLevel.Information
    );

    private readonly Action<ILogger, Exception?> _logMissingRequiredInformationWarning =
        LoggerMessage.Define(
            eventId: new EventId(3, nameof(EmailIntelligenceSubscriber)),
            formatString: "A message with empty profile id, crm user id or email was received",
            logLevel: LogLevel.Warning
        );

    private readonly Action<ILogger, string?, string?, Exception?> _logHandlingMessageError =
        LoggerMessage.Define<string?, string?>(
            eventId: new EventId(4, nameof(EmailIntelligenceSubscriber)),
            formatString: "Failed on handling the received message from {EmailIntelligenceSubscriberName}: {MessageId}",
            logLevel: LogLevel.Error
        );
}
