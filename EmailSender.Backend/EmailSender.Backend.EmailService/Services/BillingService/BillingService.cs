namespace EmailSender.Backend.EmailService.Services.BillingService
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Database;
    using Shared.Resources;
    using Shared.Exceptions;
    using Shared.Services.DateTimeService;

    public class BillingService : IBillingService
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IDateTimeService _dateTimeService;

        public BillingService(DatabaseContext databaseContext, IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _dateTimeService = dateTimeService;
        }

        public async Task<Guid> AddUserBilling(Guid userId, CancellationToken cancellationToken = default)
        {
            var userPrice = await _databaseContext.Pricing
                .AsNoTracking()
                .Where(price => price.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (userPrice == null)
                throw new BusinessException(nameof(ErrorCodes.MISSING_PRICING), ErrorCodes.MISSING_PRICING);

            var sentEmails = await _databaseContext.History
                .AsNoTracking()
                .Where(history => history.UserId == userId)
                .Select(history => history.Sent)
                .ToListAsync(cancellationToken);

            var amountBeforeDiscount = userPrice.PerSentEmail * sentEmails.Count;
            var amountAfterDiscount = 0m;

            var hasDiscount = userPrice.DiscountMaturity > _dateTimeService.Now;
            if (hasDiscount && userPrice.Discount != null)
                amountAfterDiscount = amountBeforeDiscount - (decimal)userPrice.Discount * amountBeforeDiscount;

            var totalAmount = amountAfterDiscount == 0 
                ? amountBeforeDiscount 
                : amountAfterDiscount;

            var billing = new Domain.Entities.Billing
            {
                UserId = userId,
                Amount = totalAmount,
                CurrencyIso = userPrice.CurrencyIso,
                ValueDate = _dateTimeService.Now,
                DueDate = _dateTimeService.Now.AddDays(userPrice.Terms),
                IsInvoiceSent = false,
                IssuedInvoice = null
            };

            await _databaseContext.AddAsync(billing, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            return billing.Id;
        }

        public async Task<Billing> GetUserBilling(Guid userId, CancellationToken cancellationToken = default)
        {
            var userBilling = await _databaseContext.Billing
                .AsNoTracking()
                .Where(billing => billing.UserId == userId)
                .Select(billing => new Billing
                {
                    Amount = billing.Amount,
                    CurrencyIso = billing.CurrencyIso,
                    ValueDate = billing.ValueDate,
                    DueDate = billing.DueDate,
                    IsInvoiceSent = billing.IsInvoiceSent,
                    IssuedInvoice = billing.IssuedInvoice
                })
                .FirstOrDefaultAsync(cancellationToken);

            return userBilling;
        }

        public async Task<IEnumerable<Billing>> GetAllBillings(CancellationToken cancellationToken = default)
        {
            var billings = await _databaseContext.Billing
                .AsNoTracking()
                .Select(billing => new Billing
                {
                    Amount = billing.Amount,
                    CurrencyIso = billing.CurrencyIso,
                    ValueDate = billing.ValueDate,
                    DueDate = billing.DueDate,
                    IsInvoiceSent = billing.IsInvoiceSent,
                    IssuedInvoice = billing.IssuedInvoice
                })
                .ToListAsync(cancellationToken);

            return billings;
        }
    }
}