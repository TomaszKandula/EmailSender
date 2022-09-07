namespace EmailSender.Backend.Domain.Enums;

public enum UserStatus
{
    NotSpecified,
    Activated,
    Deactivated,
    PendingActivation,
    PendingDeactivation,
    PendingDeletion
}