using System;
using System.Web.Mvc;
using Commands.Reservations;
using NServiceBus;

namespace Customer.Website.Controllers
{
    public class ReservationParameters
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime CheckingIn { get; set; }
        public DateTime CheckingOut { get; set; }
        public string RoomType { get; set; }
    }

    public class ReservationController : Controller
    {
        private readonly IBus _bus;

        public ReservationController(IBus bus)
        {
            _bus = bus;
        }

        public ActionResult Index(DateTime checkInDate, DateTime checkOutDate, string roomType)
        {
            return View(new ReservationParameters
                        {
                            CheckingIn = checkInDate,
                            CheckingOut = checkOutDate,
                            RoomType = roomType
                        });
        }

        public ActionResult Make(ReservationParameters input)
        {
            Guid reservationId = Guid.NewGuid();

            _bus.Send(new MakeNewReservation
                      {
                          Id = reservationId,
                          CustomerName = input.CustomerName,
                          CheckingIn = input.CheckingIn,
                          CheckingOut = input.CheckingOut,
                          RoomType = input.RoomType,
                          RoomsRequired = 1
                      });

            return View("complete", reservationId);
        }

        public ActionResult Retrieve(Guid reservationReference)
        {
            return View("Retrieve");
        }
    }
}