using Commands.Reservations;
using NServiceBus;
using Reservations.Application.Services;

namespace Reservations.Handlers.Application.Services
{
    public class MakeNewReservationHandler : IHandleMessages<MakeNewReservation>
    {
        private readonly ReservationService _service;

        public MakeNewReservationHandler(ReservationService service)
        {
            _service = service;
        }

        public void Handle(MakeNewReservation message)
        {
            _service.MakeReservation(message.Id, message.CustomerName,
                message.CheckingIn, message.CheckingOut, message.RoomType, message.RoomsRequired);
        }
    }
}