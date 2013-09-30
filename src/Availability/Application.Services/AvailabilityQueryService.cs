using System;
using System.Collections.Generic;
using System.Linq;
using Availability.Domain.Model.Rooms;
using Common.Kernel;
using Raven.Client;

namespace Availability.Application.Services
{
    public class AvailableRoomType
    {
        public string RoomDescription { get; set; }
        public int RoomsAvailable { get; set; }
    }

    public class AvailabilityQueryService
    {
        private readonly IDocumentStore _documentStore;

        public AvailabilityQueryService(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IEnumerable<AvailableRoomType> RoomsOfTypeAvailableForDates(RoomType room, DateTime startingDate, DateTime endingDate)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                List<AvailableRoomType> availableRooms = new List<AvailableRoomType>();

                var generalAvailability =
                    (from r in documentSession.Query<RoomAvailability>()
                        where r.DateOfAvailability.Date >= startingDate.Date &&
                              r.DateOfAvailability <= endingDate.Date
                        select r).ToList();

                if (generalAvailability.Any())
                {
                    availableRooms.Add(new AvailableRoomType
                                       {
                                           RoomDescription = room.Description,
                                           RoomsAvailable =
                                               (int)generalAvailability.Where(r => r.RoomType.Equals(room)).Average(r => r.Availability)
                                       });
                }

                return availableRooms;
            }
        }
    }
}