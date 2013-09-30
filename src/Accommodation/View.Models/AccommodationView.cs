using System;
using Common.Kernel;

namespace Accommodation.View.Models
{
    public class AccommodationView
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime CheckingIn { get; set; }
        public DateTime CheckingOut { get; set; }
        public int QuantityOfRooms { get; set; }
    }
}