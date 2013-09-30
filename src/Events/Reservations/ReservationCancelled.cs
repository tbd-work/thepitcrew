using System;
using Common.Domain.Model;

namespace Events.Reservations
{
    public class ReservationCancelled : IDomainEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}