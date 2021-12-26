namespace EmailSender.Backend.Cqrs
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using MediatR;

    [ExcludeFromCodeCoverage]
    public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
    {
        public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
    }
}