using Questrade.FinCrime.Email.Intelligence.Config;
using Questrade.FinCrime.Email.Intelligence.Extensions;

var builder = WebApplication.CreateBuilder(args);
var emailIntelligenceConfiguration = new EmailIntelligenceConfiguration();

builder.Configuration.Bind("EmailIntelligence", emailIntelligenceConfiguration);
emailIntelligenceConfiguration.Validate();
builder.RegisterServices(emailIntelligenceConfiguration);

var app = builder.Build().Configure();

app.Run();
