using System;

namespace Common.Domain.Model
{
    public class DomainEventPublisher
    {
        [ThreadStatic]
        private static DomainEventPublisher _instance;

        public static DomainEventPublisher Instance
        {
            get { return _instance ?? (_instance = new DomainEventPublisher()); }
        }

        public void Publish<TEvent>(TEvent @event)
        {

        }
    }
}