using System;
using Common.Domain.Model;

namespace Events.Accommodation
{
    public class ReservationStartedOnCheckIn : IDomainEvent
    {
        public Guid Id { get; set; }        

        public DateTime OccurredOn { get; set; }
    }
}