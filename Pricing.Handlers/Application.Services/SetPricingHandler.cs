using System;
using Commands.Pricing;
using Events.Pricing;
using NServiceBus;

namespace Pricing.Handlers.Application.Services
{
    public class SetPricingHandler : IHandleMessages<SetPricingForRoom>
    {
        private readonly IBus _bus;

        public SetPricingHandler(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(SetPricingForRoom message)
        {
             _bus.Publish(new RoomPricingCreated
                          {
                              Price = message.Pricing,
                              RoomType = message.RoomType,
                              OccurredOn = DateTime.UtcNow
                          });
        }
    }
}