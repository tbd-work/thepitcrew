using System;
using Accommodation.Domain.Model;
using Common.Persistence;
using Events.Reservations;
using NServiceBus;

namespace Accommodation.Handlers.Application.Services
{
    public class ReservationCreatedHandler : IHandleMessages<ReservationCreated>
    {
        private readonly IRepository _repository;

        public ReservationCreatedHandler(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(ReservationCreated message)
        {
            Reservation reservation = new Reservation(message.Id,
                message.RoomType,
                message.CustomerName, message.QuantityOfRooms, message.ReservationStartDate,
                message.ReservationEndDate);

            _repository.Save(reservation, Guid.NewGuid());
        }
    }
}