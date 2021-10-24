﻿namespace EmailSenderService.Backend.Shared.Services.Logger
{
    public interface ILogger
    {
        void LogDebug(string message);

        void LogError(string message);

        void LogInformation(string message);

        void LogWarning(string message);

        void LogCriticalError(string message);
    }
}