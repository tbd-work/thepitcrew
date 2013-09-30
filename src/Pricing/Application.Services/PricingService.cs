using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pricing.View.Models;
using Raven.Client;

namespace Pricing.Application.Services
{
    public class PricingService
    {
        private readonly IDocumentStore _documentStore;

        public PricingService(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public decimal GetPricingForRoomType(string roomType)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                var pricing = documentSession.Query<PricingView>()
                    .SingleOrDefault(r => r.RoomType.Code == roomType);

                return pricing != null ? pricing.Price : 0;
            }
        }

        public IEnumerable<PricingView> GetRoomPricings()
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                return documentSession.Query<PricingView>();
            }
        }
    }
}