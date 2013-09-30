using System;
using Accommodation.Domain.Model;
using Common.Persistence;

namespace Accommodation.Domain.Services
{
    public class GuestCheckIn
    {
        private readonly IRepository _repository;

        public GuestCheckIn(IRepository repository)
        {
            _repository = repository;
        }

        public void CustomerIsCheckingIn(Guid bookingId)
        {
            Reservation reservation = _repository.GetById<Reservation>(bookingId.ToString());

            reservation.CheckIn();

            _repository.Save(reservation, Guid.NewGuid());
        }
    }
}