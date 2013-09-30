using Accommodation.View.Models;
using Events.Accommodation;
using NServiceBus;
using Raven.Client;

namespace Accommodation.Views.Handlers
{
    public class AccommodationViewDenormalizer : IHandleMessages<ReservationConfirmed>
    {
        private readonly IDocumentStore _documentStore;

        public AccommodationViewDenormalizer(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void Handle(ReservationConfirmed message)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                documentSession.Store(new AccommodationView
                                      {
                                          Id = message.Id,
                                          CustomerName = message.CustomerName,
                                          RoomType = message.RoomType,
                                          QuantityOfRooms = message.QuantityOfRoomsTaken,
                                          CheckingIn = message.StartDate,
                                          CheckingOut = message.EndDate
                                      });

                documentSession.SaveChanges();
            }
        }
    }
}