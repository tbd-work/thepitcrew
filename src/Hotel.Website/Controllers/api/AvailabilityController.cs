using System;
using System.Web.Http;
using Availability.Application.Services;

namespace Hotel.Website.Controllers.api
{
    public class AvailabilityRequest
    {
        public string RoomType { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public int Availability { get; set; }
    }

    public class AvailabilityController : ApiController
    {
        private readonly AvailabilityService _service;

        public AvailabilityController(AvailabilityService service)
        {
            _service = service;
        }

        public void Post(AvailabilityRequest input)
        {
            _service.MakeRoomAvailabilityForDateRange(
                input.RoomType, 
                input.StartingDate, 
                input.EndingDate,
                input.Availability);
        }
    }
}