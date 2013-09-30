using System;
using Common.Domain.Model;

namespace Events.Accommodation
{
    public class ReservationAssignedToRoom : IDomainEvent
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        public int RoomNumber { get; set; }        
    }
}