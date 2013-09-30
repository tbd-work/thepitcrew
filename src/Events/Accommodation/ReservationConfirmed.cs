using System;
using Common.Domain.Model;
using Common.Kernel;

namespace Events.Accommodation
{
    public class ReservationConfirmed : IDomainEvent
    {
        public Guid Id { get; set; }
        public RoomType RoomType { get; set; }
        public string CustomerName { get; set; }
        public int QuantityOfRoomsTaken { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}