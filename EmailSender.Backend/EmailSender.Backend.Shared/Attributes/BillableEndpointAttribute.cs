namespace EmailSender.Backend.Shared.Attributes;

using System;

[AttributeUsage(AttributeTargets.Method)] 
public class BillableEndpointAttribute : Attribute { }
