using Common.Domain.Model;
using Common.Publishers;
using NServiceBus;

namespace Common.NServiceBus
{
    public class NServiceBusPublisher : IPublisher
    {
        private readonly IBus _bus;

        public NServiceBusPublisher(IBus bus)
        {
            _bus = bus;
        }

        public void Publish(IDomainEvent domainEvent)
        {
            _bus.Publish(domainEvent);
        }
    }
}