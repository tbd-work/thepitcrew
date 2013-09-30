using System;
using System.Collections.Generic;
using System.Linq;
using Accommodation.View.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace Accommodation.Application.Services
{
    public class AccommodationQueryService
    {
        private readonly IDocumentStore _documentStore;

        public AccommodationQueryService(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IEnumerable<AccommodationView> FindAccommodationViewByCustomerName(string customerName, bool searchAllDates = false)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                var reservations = from r in documentSession.Query<AccommodationView>()
                                   where (searchAllDates || r.CheckingIn >= DateTime.Now) &&
                                         (searchAllDates || r.CheckingOut > DateTime.Now) 
                                   orderby r.CheckingIn
                                   select r;

                return reservations.ToList().Where(r => r.CustomerName.Contains(customerName));
            }
        }

        public AccommodationView FindAccommodationByReservationReference(Guid reservationReference)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                return documentSession.Load<AccommodationView>(reservationReference);
            }
        }
    }
}