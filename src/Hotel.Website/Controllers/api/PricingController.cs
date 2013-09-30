using System.Web.Http;
using Commands.Pricing;
using NServiceBus;

namespace Hotel.Website.Controllers.api
{
    public class PricingInput
    {
        public string RoomType { get; set; }
        public decimal Pricing { get; set; }
    }

    public class PricingController : ApiController
    {
        private readonly IBus _bus;

        public PricingController(IBus bus)
        {
            _bus = bus;
        }

        public void Post(PricingInput input)
        {
            _bus.Send(new SetPricingForRoom
                      {
                          RoomType = input.RoomType,
                          Pricing = input.Pricing
                      });
        }
    }
}