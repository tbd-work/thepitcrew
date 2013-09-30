using System;
using Common.Domain.Model;
using Common.Kernel;

namespace Events.Pricing
{
    public class RoomPricingUpdated : IDomainEvent
    {
        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}