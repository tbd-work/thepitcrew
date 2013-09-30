using System;
using Common.Domain.Model;
using Common.Kernel;

namespace Events.Reservations
{
    public class ReservationCreated : IDomainEvent
    {
        public Guid Id { get; set; } 
        public RoomType RoomType { get; set; }
        public int QuantityOfRooms { get; set; }
        public string CustomerName { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}