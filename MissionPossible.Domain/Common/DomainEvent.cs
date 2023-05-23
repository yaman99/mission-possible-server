using MediatR;
using System;

namespace MissionPossible.Domain.Common
{
    //Base event as base notification
    public abstract class DomainEvent : INotification
    {
        protected DomainEvent()
        {
            Timestamp = DateTime.Now;
            Id = Guid.NewGuid();
        }

        //Stamp the current fired event with its equivalent execution time
        public DateTime Timestamp { get; }
        public Guid Id { get; set; }
    }
}
