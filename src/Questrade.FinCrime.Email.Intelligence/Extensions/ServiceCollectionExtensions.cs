using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Questrade.FinCrime.Email.Intelligence.Application.Config;
using Questrade.FinCrime.Email.Intelligence.Config;
using Questrade.FinCrime.Email.Intelligence.Domain.Repository;
using Questrade.FinCrime.Email.Intelligence.Domain.Services;
using Questrade.FinCrime.Email.Intelligence.Domain.Utils;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.LexisNexis;
using Questrade.FinCrime.Email.Intelligence.Infra.Config.PubSub;
using Questrade.FinCrime.Email.Intelligence.Infra.Models.Messages;
using Questrade.FinCrime.Email.Intelligence.Infra.Repository;
using Questrade.FinCrime.Email.Intelligence.Infra.Services;
using Questrade.FinCrime.Email.Intelligence.Infra.Services.Publisher;
using Questrade.FinCrime.Email.Intelligence.Infra.Services.Subscriber;
using Questrade.FinCrime.Email.Intelligence.Infra.Utils;
using Questrade.Library.HealthCheck.AspNetCore.Extensions;
using Questrade.Library.PubSubClientHelper.Extensions;
using Questrade.Library.PubSubClientHelper.HealthCheck;
using Serilog;
using StatsdClient;

namespace Questrade.FinCrime.Email.Intelligence.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    internal static WebApplicationBuilder RegisterServices(
        this WebApplicationBuilder builder,
        EmailIntelligenceConfiguration emailIntelligenceConfiguration
    )
    {
        builder.AddQuestradeHealthCheck();
        builder.Host.UseSerilog((context, logConfiguration) => logConfiguration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddAppServices(emailIntelligenceConfiguration);
        builder.Services.AddControllers();
        builder.Services.AddCorrelationContext();
        builder.Services.AddDataDogMetrics(builder.Configuration);
        builder.Services.AddMediatR(
            AppDomain.CurrentDomain.Load("Questrade.FinCrime.Email.Intelligence"),
            AppDomain.CurrentDomain.Load("Questrade.FinCrime.Email.Intelligence.Application"),
            AppDomain.CurrentDomain.Load("Questrade.FinCrime.Email.Intelligence.Infra")
        );
        builder.Services.AddLexisNexisIntegration(builder.Configuration);
        builder.Services.AddPublisher(emailIntelligenceConfiguration);
        builder.Services.AddSubscribers(emailIntelligenceConfiguration);

        return builder;
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services, EmailIntelligenceConfiguration configuration)
    {
        services.AddTransient<IMetricService, MetricService>();
        services.AddTransient<IPublisherHandlerService, PublisherHandlerService>();
        services.TryAddSingleton(configuration.LexisNexisConfiguration);

        return services;
    }

    private static void AddDataDogMetrics(this IServiceCollection services, IConfiguration configuration)
    {
        var dDMetricsConfig = configuration.GetSection("DataDog:StatsD").Get<DataDogMetricsConfig>();

        services.AddSingleton<IDogStatsd>(_ =>
        {
            var statsdConfig = new StatsdConfig
            {
                Prefix = dDMetricsConfig.Prefix,
                StatsdServerName = dDMetricsConfig.HostName
            };

            var dogStatsdService = new DogStatsdService();
            dogStatsdService.Configure(statsdConfig);

            return dogStatsdService;
        });
    }

    private static IServiceCollection AddPublisher(this IServiceCollection services, EmailIntelligenceConfiguration configuration)
    {
        if (configuration.EmailIntelligencePublisherConfiguration.Enable)
        {
            services.RegisterOutboxPublisherWithInMemoryOutbox<
                EmailIntelligencePublisherConfiguration,
                PubSubMessage<EmailIntelligenceResultMessage>,
                EmailIntelligencePublisherService,
                EmailIntelligencePublisherBackgroundService,
                PublisherHealthCheck<
                    EmailIntelligencePublisherConfiguration,
                    PubSubMessage<EmailIntelligenceResultMessage>
                >
            >(configuration.EmailIntelligencePublisherConfiguration);
        }

        return services;
    }

    private static IServiceCollection AddSubscribers(this IServiceCollection services, EmailIntelligenceConfiguration configuration)
    {
        if (configuration.EmailIntelligenceSubscriberConfiguration.Enable)
        {
            services.RegisterSubscriber<
                EmailIntelligenceSubscriberConfiguration,
                PubSubMessage<EmailIntelligenceMessage>,
                EmailIntelligenceSubscriber,
                SubscriberHealthCheck<
                    EmailIntelligenceSubscriberConfiguration,
                    PubSubMessage<EmailIntelligenceMessage>
                >
                >(configuration.EmailIntelligenceSubscriberConfiguration);
        }

        return services;
    }

    private static IServiceCollection AddCorrelationContext(this IServiceCollection services)
    {
        services.AddScoped<CorrelationContext>();

        return services;
    }

    private static IServiceCollection AddLexisNexisIntegration(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationLexisNexis = configuration.GetSection($"EmailIntelligence:{nameof(LexisNexisConfiguration)}").Get<LexisNexisConfiguration>();
        services.AddOptions().Configure<LexisNexisConfiguration>(configuration.GetSection($"EmailIntelligence:{nameof(LexisNexisConfiguration)}"));
        services.AddHttpClient<ILexisNexisRepository, LexisNexisRepository>(nameof(LexisNexisRepository))
            .AddPolicyHandler(
                    new HttpPolicies<ILexisNexisRepository>(services.BuildServiceProvider()).GetAllResiliencePolicies(configurationLexisNexis.Resilience)
                );

        return services;
    }
}
