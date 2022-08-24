namespace EmailSender.Backend.Shared.Attributes;

using System;

[AttributeUsage(AttributeTargets.Method)]
public class RequireAdministratorAttribute : Attribute { }
