# ======================================================================================================================
# 1 - BUILD PROJECTS AND RUN TESTS
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/sdk:6.0.416-alpine3.18 AS PROJECTS

WORKDIR /app
COPY . ./

# RESTORE & BUILD & TEST
RUN dotnet restore --disable-parallel
RUN dotnet build -c Release --force
RUN dotnet test -c Release --no-build --no-restore
# ======================================================================================================================
# 2 - BUILD DOCKER IMAGE
# ======================================================================================================================
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.18
WORKDIR /app

# INSTALL ICU FULL SUPPORT
RUN apk add icu-libs --no-cache
RUN apk add icu-data-full --no-cache

# BACKEND
COPY --from=PROJECTS "/app/EmailSender.Backend/EmailSender.Backend.Application/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/EmailSender.Backend/EmailSender.Backend.Core/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/EmailSender.Backend/EmailSender.Backend.Domain/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/EmailSender.Backend/EmailSender.Backend.Shared/bin/Release/net6.0" .

# PERSISTENCE
COPY --from=PROJECTS "/app/EmailSender.Persistence/EmailSender.Persistence.Database/bin/Release/net6.0" .

# SERVICES
COPY --from=PROJECTS "/app/EmailSender.Services/EmailSender.Services.BehaviourService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/EmailSender.Services/EmailSender.Services.HttpClientService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/EmailSender.Services/EmailSender.Services.SenderService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/EmailSender.Services/EmailSender.Services.SmtpService/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/EmailSender.Services/EmailSender.Services.UserService/bin/Release/net6.0" .

# WEBAPI
COPY --from=PROJECTS "/app/EmailSender.WebApi/bin/Release/net6.0" .
COPY --from=PROJECTS "/app/EmailSender.WebApi.Dto/bin/Release/net6.0" .

# CONFIGURATION
ARG ENV_VALUE
ARG PFX_PASSWORD
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=${PFX_PASSWORD}
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/certs/wildcard-emailsender.dev.pfx
ENV ASPNETCORE_URLS=https://+:443
EXPOSE 443
ENTRYPOINT ["dotnet", "EmailSender.WebApi.dll"]
