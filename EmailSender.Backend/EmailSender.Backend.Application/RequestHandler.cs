using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace EmailSender.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class RequestHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}