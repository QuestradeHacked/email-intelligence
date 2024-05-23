FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
EXPOSE 80
EXPOSE 443

# Copy project resources
COPY src src
COPY nuget.config ./
COPY .editorconfig ./
COPY Questrade.FinCrime.Email.Intelligence.sln Questrade.FinCrime.Email.Intelligence.sln
RUN dotnet restore --locked-mode ./src/Questrade.FinCrime.Email.Intelligence/Questrade.FinCrime.Email.Intelligence.csproj --configfile nuget.config

# Test steps
FROM build as test
RUN dotnet restore --locked-mode src/Questrade.FinCrime.Email.Intelligence.Tests.Unit/Questrade.FinCrime.Email.Intelligence.Tests.Unit.csproj --configfile nuget.config
ENTRYPOINT ["dotnet", "test" ]

# Publishing the application
FROM build AS publish
RUN dotnet publish src/Questrade.FinCrime.Email.Intelligence/Questrade.FinCrime.Email.Intelligence.csproj -c Release -o /app/Questrade.FinCrime.Email.Intelligence --no-restore

# Final image wrap-up
FROM gcr.io/qt-shared-services-3w/dotnet:6.0 as runtime
WORKDIR /app
COPY --from=publish /app/Questrade.FinCrime.Email.Intelligence .
USER dotnet
CMD [ "dotnet", "Questrade.FinCrime.Email.Intelligence.dll" ]
