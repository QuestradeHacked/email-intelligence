using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;
using Context = Polly.Context;

namespace Questrade.FinCrime.Email.Intelligence.Infra.Utils;

[ExcludeFromCodeCoverage]
public class HttpPolicies<T> where T : class
{
    public readonly ILogger<T> _logger;
    public HttpPolicies(IServiceProvider serviceProvider)
    {
        _logger = serviceProvider.GetService<ILogger<T>>()!;
    }

    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(Resilience resilience)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .Or<Exception>()
            .WaitAndRetryAsync(resilience.RetryCount, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy(Resilience resilience)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(
                resilience.ConsecutiveExceptionsAllowedBeforeBreaking,
                TimeSpan.FromSeconds(resilience.DurationOfBreakInSeconds),
                onBreak: (outcome, breakDelay) =>
                    _logger.LogError("API will break in the next {totalSeconds} seconds. | Error: {exception}", breakDelay.TotalSeconds, outcome?.Exception),
                onReset: () => _logger.LogInformation("API Reset."),
                onHalfOpen: () => _logger.LogWarning("API Half Open."));
    }

    public IAsyncPolicy<HttpResponseMessage> GetFallback()
    {
        return Policy<HttpResponseMessage>
            .Handle<BrokenCircuitException>()
            .FallbackAsync(FallbackAction, OnFallbackAsync);
    }

    public IAsyncPolicy<HttpResponseMessage> GetAllResiliencePolicies(Resilience resilience)
    {
        return Policy.WrapAsync(GetRetryPolicy(resilience), GetFallback(), GetCircuitBreakerPolicy(resilience));
    }

    private Task OnFallbackAsync(DelegateResult<HttpResponseMessage> response, Context context)
    {
        _logger.LogError("{policyKey} at {operationKey}: fallback value substituted, due to: {exception}.", context.PolicyKey, context.OperationKey, response.Exception);
        return Task.CompletedTask;
    }

    private Task<HttpResponseMessage> FallbackAction(DelegateResult<HttpResponseMessage> responseToFailedRequest, Context context, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Fallback action is executing");
        var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent)
        {
            Content = null
        };
        return Task.FromResult(httpResponseMessage);
    }
}
