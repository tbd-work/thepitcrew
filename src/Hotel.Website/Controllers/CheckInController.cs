using System;
using System.Collections;
using System.Web.Mvc;
using Accommodation.Application.Services;

namespace Hotel.Website.Controllers
{
    public class ReservationInput
    {
        public string CustomerName { get; set; }
        public string Reference { get; set; }
    }

    public class CheckInInput
    {
        public string ReservationReference { get; set; }
        public int RoomNumber { get; set; }
    }

    public class CheckInController : Controller
    {
        private readonly AccommodationQueryService _query;

        public CheckInController(AccommodationQueryService query)
        {
            _query = query;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Retrieve(ReservationInput input)
        {
            if (!string.IsNullOrEmpty(input.CustomerName))
                return View(_query.FindAccommodationViewByCustomerName(input.CustomerName));

            return View(new[] { _query.FindAccommodationByReservationReference(new Guid(input.Reference)) });
        }

        public ActionResult Do(string id)
        {
            return View(_query.FindAccommodationByReservationReference(new Guid(id)));
        }

        public ActionResult Complete(CheckInInput input)
        {
            return Redirect("/");
        }
    }
}
