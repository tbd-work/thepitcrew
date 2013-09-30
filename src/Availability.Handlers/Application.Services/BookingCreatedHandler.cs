using Availability.Application.Services;
using Events.Availability;
using Events.Reservations;
using NServiceBus;

namespace Availability.Handlers.Application.Services
{
    public class BookingCreatedHandler : IHandleMessages<ReservationCreated>
    {
        private readonly IBus _bus;
        private readonly AvailabilityService _roomAvailability;

        public BookingCreatedHandler(IBus bus, AvailabilityService roomAvailability)
        {
            _bus = bus;
            _roomAvailability = roomAvailability;
        }

        public void Handle(ReservationCreated reservationCreated)
        {
            _roomAvailability.ReduceAvailabilityForRoom(
                reservationCreated.RoomType,
                reservationCreated.QuantityOfRooms,
                reservationCreated.ReservationStartDate,
                reservationCreated.ReservationEndDate);

            _bus.Publish(new AvailabilityReduced
                         {
                             RoomType = reservationCreated.RoomType
                         });
        }
    }
}