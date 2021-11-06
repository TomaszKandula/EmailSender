namespace EmailSender.Backend.EmailService.Services.BillingService
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;

    public interface IBillingService
    {
        Task<Guid> AddUserBilling(Guid userId, CancellationToken cancellationToken = default);

        Task<Billing> GetUserBilling(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Billing>> GetAllBillings(CancellationToken cancellationToken = default);
    }
}