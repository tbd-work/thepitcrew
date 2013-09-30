using System.Web.Mvc;
using Pricing.Application.Services;

namespace Hotel.Website.Controllers
{
    public class PricingController : Controller
    {
        private readonly PricingService _pricing;

        public PricingController(PricingService pricing)
        {
            _pricing = pricing;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Prices()
        {
            return PartialView(_pricing.GetRoomPricings());
        }
    }
}
