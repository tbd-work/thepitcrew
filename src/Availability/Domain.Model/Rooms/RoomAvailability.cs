using System;
using Common.Kernel;

namespace Availability.Domain.Model.Rooms
{
    public class RoomAvailability
    {
        public string Id { get; private set; }
        public RoomType RoomType { get; private set; }
        public DateTime DateOfAvailability { get; private set; }
        public int Availability { get; private set; }

        protected RoomAvailability() { }

        public void RoomsWereBooked(int numberOfRooms)
        {
            Availability -= numberOfRooms;
        }

        public static RoomAvailability MakeAvailability(RoomType roomType, DateTime date, int availableRooms)
        {
            return new RoomAvailability
                   {
                       Id = string.Format("{0}/{1}", roomType, date.ToString("yyyyMMdd")),
                       DateOfAvailability = date,
                       Availability = availableRooms,
                       RoomType = roomType
                   };
        }
    }
}