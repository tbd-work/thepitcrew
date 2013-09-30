using System;
using Common.Kernel;
using Common.Persistence;
using Raven.Client;
using Reservations.Domain.Model.Bookings;
using Reservations.View.Model;

namespace Reservations.Application.Services
{
    public class ReservationService
    {
        private readonly IRepository _repository;
        private readonly IDocumentStore _documentStore;

        public ReservationService(IRepository repository, IDocumentStore documentStore)
        {
            _repository = repository;
            _documentStore = documentStore;
        }

        public void MakeReservation(
            Guid id, string customerName, DateTime startReservationDate,
            DateTime endReservationDate, RoomType roomType, int quantityOfRooms)
        {
            Reservation reservation = new Reservation(id, customerName, roomType, quantityOfRooms,
                startReservationDate, endReservationDate);

            _repository.Save(reservation, Guid.NewGuid());
        }

        public ReservationInfo GetReservationByReference(Guid reservationReference)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                ReservationInfo reservation = documentSession.Load<ReservationInfo>(reservationReference);

                return reservation;
            }
        }
    }
}