using System;
using System.Collections.Generic;

namespace Common.Domain.Model
{
    public class AggregateRoot
    {
        public Guid Id { get; protected set; }
        public Queue<IDomainEvent> UnpublishedEvents { get; private set; }
        public int Version { get; private set; }

        protected AggregateRoot()
        {
            UnpublishedEvents = new Queue<IDomainEvent>();
            Version = 1;
        }

        protected void RaiseEvent(IDomainEvent @event)
        {
            UnpublishedEvents.Enqueue(@event);

            ApplyEvent(@event);
        }

        protected void ApplyEvent(IDomainEvent @event)
        {
            (this as dynamic).Apply(@event as dynamic);
        }

        public void ClearUnpublishedEvents()
        {
            UnpublishedEvents = new Queue<IDomainEvent>();
        }

        public void GetUncommittedEvents()
        {
            throw new System.NotImplementedException();
        }
    }
}