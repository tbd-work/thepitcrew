using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Availability.Application.Services;
using Pricing.Application.Services;

namespace Customer.Website.Controllers
{
    public class RoomAvailablility
    {
        public string RoomDescription { get; set; }
        public int RoomsAvailable { get; set; }
        public decimal Pricing { get; set; }
    }

    public class AvailabilityRequest
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomType { get; set; }
    }

    public class AvailabilityController : ApiController
    {
        private readonly AvailabilityQueryService _service;
        private readonly PricingService _pricing;

        public AvailabilityController(AvailabilityQueryService service, PricingService pricing)
        {
            _service = service;
            _pricing = pricing;
        }

        public IEnumerable<RoomAvailablility> Get([FromUri]AvailabilityRequest request)
        {
            return
                _service.RoomsOfTypeAvailableForDates(request.RoomType, request.CheckInDate, request.CheckOutDate)
                    .Select(s => new RoomAvailablility
                                 {
                                     RoomDescription = s.RoomDescription,
                                     RoomsAvailable = s.RoomsAvailable,
                                     Pricing = _pricing.GetPricingForRoomType(request.RoomType)
                                 });
        }
    }
}