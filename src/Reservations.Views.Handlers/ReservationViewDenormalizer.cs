using Events.Accommodation;
using Events.Reservations;
using NServiceBus;
using Raven.Client;
using Reservations.View.Model;

namespace Reservations.Views.Handlers
{
    public class ReservationViewDenormalizer : IHandleMessages<ReservationCreated>
    {
        private readonly IDocumentStore _documentStore;

        public ReservationViewDenormalizer(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void Handle(ReservationCreated message)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                documentSession.Store(new ReservationInfo
                                      {
                                          Id = message.Id,
                                          CustomerName = message.CustomerName,
                                          StartingDate = message.ReservationStartDate,
                                          EndingDate = message.ReservationEndDate,
                                          RoomType = message.RoomType,
                                          QuantityOfRooms = message.QuantityOfRooms,
                                          ReservationWasMadeOn = message.OccurredOn
                                      });

                documentSession.SaveChanges();
            }
        }
    }
}