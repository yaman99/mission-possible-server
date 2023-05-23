
using MissionPossible.Domain.Common;
using MissionPossible.Application.EventBus.Bus;
using MediatR;
using System;
using System.Threading.Tasks;

namespace MissionPossible.Application.EventBus
{
    public class MediatrBus : IDomainBus
    {
        private readonly IMediator _mediator;

        public MediatrBus(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<TResponse> ExecuteAsync<T, TResponse>(T command) where T : IRequest<TResponse>
        {
           return  await _mediator.Send(command);
        }



        public async Task RaiseEvent<T>(T @event) where T : DomainEvent
        {
            await _mediator.Publish(@event);
        }

        /*private INotification GetNotificationCorrespondingToDomainEvent(Event domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }*/
    }
}
