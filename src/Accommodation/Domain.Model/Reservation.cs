using System;
using Common.Domain.Model;
using Common.Kernel;
using Events.Accommodation;

namespace Accommodation.Domain.Model
{
    public class Reservation : AggregateRoot
    {
        protected Reservation() { }

        public bool IsCheckedIn { get; private set; }
        public DateTime CheckedInDateTime { get; private set; }

        public Reservation(
            Guid id,
            RoomType roomType, 
            string customerName,
            int quantityOfRoomsTaken,
            DateTime reservationStartDate,
            DateTime reservationEndDate)
        {
            RaiseEvent(new ReservationConfirmed
                       {
                           Id = id,
                           RoomType = roomType,
                           CustomerName = customerName,
                           QuantityOfRoomsTaken = quantityOfRoomsTaken,
                           StartDate = reservationStartDate,
                           EndDate = reservationEndDate,
                           OccurredOn = DateTime.UtcNow
                       });
        }

        public void CheckIn()
        {
            RaiseEvent(new ReservationStartedOnCheckIn
                       {
                           Id = Id,
                           OccurredOn = DateTime.UtcNow
                       });
        }

        public void AssignToRoom(int number)
        {
            RaiseEvent(new ReservationAssignedToRoom
                       {
                           Id = Id,
                           RoomNumber = number
                       });
        }

        public void Apply(ReservationConfirmed @event)
        {
            Id = @event.Id;
        }

        public void Apply(ReservationStartedOnCheckIn @event)
        {
            IsCheckedIn = true;
            CheckedInDateTime = @event.OccurredOn;
        }
    }
}