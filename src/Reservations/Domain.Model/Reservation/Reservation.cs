using System;
using Common.Domain.Model;
using Common.Kernel;
using Events.Reservations;

namespace Reservations.Domain.Model.Bookings
{
    public class Reservation : AggregateRoot
    {
        public string CustomerName { get; private set; }
        public DateTime ReservationStartDate { get; private set; }
        public DateTime ReservationEndDate { get; private set; }
        public DateTime CancellationDate { get; private set; }

        protected Reservation() { }

        public Reservation(Guid id, string customerName, RoomType roomType, int quantityOfRooms, DateTime reservationStartDate, DateTime reservationEndDate)
        {
            Id = id;
            CustomerName = customerName;
            ReservationStartDate = reservationStartDate;
            ReservationEndDate = reservationEndDate;

            RaiseEvent(new ReservationCreated
                       {
                           Id = Id,
                           CustomerName = CustomerName,
                           RoomType = roomType.Code,
                           QuantityOfRooms = quantityOfRooms,
                           ReservationStartDate = ReservationStartDate,
                           ReservationEndDate = ReservationEndDate,
                           OccurredOn = DateTime.UtcNow
                       });
        }

        public int Days
        {
            get
            {
                return ReservationEndDate.Subtract(ReservationStartDate).Days;
            }
        }

        public void CancelReservation()
        {
            RaiseEvent(new ReservationCancelled
                       {
                           Id = Id,
                           OccurredOn = DateTime.Now
                       });
        }

        public void Apply(ReservationCreated @event)
        {
            Id = @event.Id;
            ReservationStartDate = @event.ReservationStartDate;
            ReservationEndDate = @event.ReservationEndDate;
        }

        public void Apply(ReservationCancelled @event)
        {
            CancellationDate = @event.OccurredOn;
        }
    }
}