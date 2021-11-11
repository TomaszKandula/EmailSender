namespace EmailSender.Backend.BillingService
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Services.DateTimeService;

    public class BillingService : IBillingService
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IDateTimeService _dateTimeService;

        public BillingService(DatabaseContext databaseContext, IDateTimeService dateTimeService)
        {
            _databaseContext = databaseContext;
            _dateTimeService = dateTimeService;
        }

        /// <summary>
        /// Adds new billing to given user. Billing takes into account how many requests have been made,
        /// and how many emails have been sent.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Billing database entity ID (Guid).</returns>
        /// <exception cref="BusinessException">Throws HTTP status code 400.</exception>
        public async Task<Guid> AddUserBilling(Guid userId, CancellationToken cancellationToken = default)
        {
            var pricing = await _databaseContext.Pricing
                .AsNoTracking()
                .Where(price => price.UserId == userId)
                .FirstOrDefaultAsync(cancellationToken);

            if (pricing == null)
                throw new BusinessException(nameof(ErrorCodes.MISSING_PRICING), ErrorCodes.MISSING_PRICING);

            var emailHistory = await _databaseContext.EmailHistory
                .AsNoTracking()
                .Where(history => history.UserId == userId)
                .Select(history => history.Sent)
                .ToListAsync(cancellationToken);

            var requestHistory = await _databaseContext.RequestHistory
                .AsNoTracking()
                .Where(history => history.UserId == userId)
                .Select(history => history.Requested)
                .ToListAsync(cancellationToken);

            var sentCost = pricing.PerSentEmail * emailHistory.Count;
            var requestCost = pricing.PerApiRequest * requestHistory.Count;
            var amountBeforeDiscount = sentCost + requestCost;
            var amountAfterDiscount = 0m;

            var hasDiscount = pricing.DiscountMaturity > _dateTimeService.Now;
            if (hasDiscount && pricing.Discount != null)
                amountAfterDiscount = amountBeforeDiscount - (decimal)pricing.Discount * amountBeforeDiscount;

            var totalAmount = amountAfterDiscount == 0 
                ? amountBeforeDiscount 
                : amountAfterDiscount;

            var billing = new Domain.Entities.Billing
            {
                UserId = userId,
                Amount = totalAmount,
                CurrencyIso = pricing.CurrencyIso,
                ValueDate = _dateTimeService.Now,
                DueDate = _dateTimeService.Now.AddDays(pricing.Terms),
                InvoiceSentDate = null,
                IssuedInvoice = null
            };

            await _databaseContext.AddAsync(billing, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            return billing.Id;
        }

        /// <summary>
        /// Returns billing entity from database by billing ID.
        /// </summary>
        /// <param name="id">Billing ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Requested billing entity.</returns>
        public async Task<Billing> GetUserBilling(Guid id, CancellationToken cancellationToken = default)
        {
            var userBilling = await _databaseContext.Billing
                .AsNoTracking()
                .Where(billing => billing.Id == id)
                .Select(billing => new Billing
                {
                    Amount = billing.Amount,
                    CurrencyIso = billing.CurrencyIso,
                    ValueDate = billing.ValueDate,
                    DueDate = billing.DueDate,
                    InvoiceSentDate = billing.InvoiceSentDate,
                    Status = billing.Status
                })
                .FirstOrDefaultAsync(cancellationToken);

            return userBilling;
        }

        /// <summary>
        /// Returns list of billing database entities for given user ID.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Requested list of billing entities.</returns>
        public async Task<IEnumerable<Billing>> GetAllUserBillings(Guid userId, CancellationToken cancellationToken = default)
        {
            var billings = await _databaseContext.Billing
                .AsNoTracking()
                .Where(billing => billing.UserId == userId)
                .Select(billing => new Billing
                {
                    Amount = billing.Amount,
                    CurrencyIso = billing.CurrencyIso,
                    ValueDate = billing.ValueDate,
                    DueDate = billing.DueDate,
                    InvoiceSentDate = billing.InvoiceSentDate,
                    Status = billing.Status
                })
                .ToListAsync(cancellationToken);

            return billings;
        }

        /// <summary>
        /// Returns all registered billings for any user.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Requested list of billing entities.</returns>
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
                    InvoiceSentDate = billing.InvoiceSentDate,
                    Status = billing.Status
                })
                .ToListAsync(cancellationToken);

            return billings;
        }
    }
}