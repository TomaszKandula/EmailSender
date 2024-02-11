# ======================================================================================================================
# 1 - BUILD PROJECTS AND RUN TESTS
# ======================================================================================================================

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS PROJECTS

WORKDIR /app
COPY . ./

# RESTORE & BUILD
RUN dotnet restore --disable-parallel
RUN dotnet build -c Release --force
RUN dotnet test -c Release --no-build --no-restore

# ======================================================================================================================
# 2 - BUILD DOCKER IMAGE
# ======================================================================================================================

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /app

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
ENV ASPNETCORE_ENVIRONMENT=${ENV_VALUE}
ENV ASPNETCORE_URLS=http://+:80  

EXPOSE 80
ENTRYPOINT ["dotnet", "EmailSender.WebApi.dll"]
