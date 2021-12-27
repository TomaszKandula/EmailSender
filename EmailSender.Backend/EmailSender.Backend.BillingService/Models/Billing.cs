namespace EmailSender.Backend.BillingService.Models;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public class Billing
{
    public decimal Amount { get; set; }

    public string CurrencyIso { get; set; }

    public DateTime ValueDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? InvoiceSentDate { get; set; }

    public PaymentStatuses Status { get; set; }
}