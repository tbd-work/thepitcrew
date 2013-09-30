using System;

namespace Commands.Reservations
{
    public class MakeNewReservation
    {
        public Guid Id { get; set; }
        public int RoomsRequired { get; set; }
        public string RoomType { get; set; }
        public DateTime CheckingOut { get; set; }
        public DateTime CheckingIn { get; set; }
        public string CustomerName { get; set; }        
    }
}