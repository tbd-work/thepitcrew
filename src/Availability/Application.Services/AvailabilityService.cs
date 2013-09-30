using System;
using Availability.Domain.Model.Rooms;
using Common.Kernel;
using Raven.Client;

namespace Availability.Application.Services
{
    public class AvailabilityService
    {
        private readonly IDocumentStore _documentStore;

        public AvailabilityService(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void MakeRoomAvailabilityForDateRange(RoomType roomType, DateTime startingDate, DateTime endingDate, int availableRooms)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                for (int day = 0; day < endingDate.Subtract(startingDate).Days; day++)
                {
                    RoomAvailability roomAvailability = RoomAvailability.MakeAvailability(roomType, startingDate.AddDays(day), availableRooms);

                    documentSession.Store(roomAvailability);

                    documentSession.SaveChanges();
                }
            }
        }

        public void ReduceAvailabilityForRoom(RoomType roomType, int quantityOfRoomsTaken, DateTime startingDate, DateTime endingDate)
        {
            for (int day = 0; day < endingDate.Subtract(startingDate).Days; day++)
            {
                using (IDocumentSession documentSession = _documentStore.OpenSession())
                {
                    RoomAvailability roomAvailability =
                        documentSession.Load<RoomAvailability>(string.Format("{0}/{1}", roomType,
                            startingDate.AddDays(day).ToString("yyyyMMdd")));

                    if (roomAvailability != null)
                        roomAvailability.RoomsWereBooked(quantityOfRoomsTaken);

                    documentSession.Store(roomAvailability);
                    documentSession.SaveChanges();
                }
            }
        } 
    }
}