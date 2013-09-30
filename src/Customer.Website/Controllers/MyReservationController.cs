using System;
using System.Web.Mvc;
using Reservations.Application.Services;
using Reservations.View.Model;

namespace Customer.Website.Controllers
{
    public class MyReservationController : Controller
    {
        private readonly ReservationService _service;

        public MyReservationController(ReservationService service)
        {
            _service = service;
        }

        public ActionResult Index(string reservationReference)
        {
            ReservationInfo reservation = _service.GetReservationByReference(new Guid(reservationReference));

            return View(reservation);
        }

    }
}
