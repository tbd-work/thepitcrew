using Events.Pricing;
using NServiceBus;
using Pricing.View.Models;
using Raven.Client;

namespace Pricing.Views.Handlers
{
    public class PricingViewDenormalizer :
        IHandleMessages<RoomPricingCreated>,
        IHandleMessages<RoomPricingUpdated>
    {
        private readonly IDocumentStore _documentStore;

        public PricingViewDenormalizer(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public void Handle(RoomPricingCreated message)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                PricingView pricingView = documentSession.Load<PricingView>(string.Format("pricingviews/{0}",message.RoomType.Code)) ??
                                          new PricingView
                                          {
                                              Id = message.RoomType.Code,
                                              RoomType = message.RoomType,
                                              Price = message.Price
                                          };

                documentSession.Store(pricingView);

                documentSession.SaveChanges();
            }
        }

        public void Handle(RoomPricingUpdated message)
        {
            using (IDocumentSession documentSession = _documentStore.OpenSession())
            {
                PricingView pricingView = documentSession.Load<PricingView>(string.Format("pricingviews/{0}",message.RoomType.Code)) ??
                                          new PricingView
                                          {
                                              Id = message.RoomType.Code,
                                              Price = message.Price
                                          };

                documentSession.Store(pricingView);
                documentSession.SaveChanges();
            }
        }
    }
}