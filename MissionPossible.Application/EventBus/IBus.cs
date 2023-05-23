
using MissionPossible.Domain.Common;
using MediatR;
using System.Threading.Tasks;

namespace MissionPossible.Application.EventBus.Bus
{
    public interface IDomainBus
    {
     
        Task<TResponse> ExecuteAsync<T, TResponse>(T command) where T : IRequest<TResponse>;
        Task RaiseEvent<T>(T @event) where T : DomainEvent;
    }
}
