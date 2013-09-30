using System;
using Common.Kernel;

namespace Reservations.View.Model
{
    public class ReservationInfo
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public RoomType RoomType { get; set; }
        public int QuantityOfRooms { get; set; }
        public DateTime ReservationWasMadeOn { get; set; }
    }
}