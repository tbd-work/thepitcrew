using Common.Kernel;

namespace Pricing.View.Models
{
    public class PricingView
    {
        public string Id { get; set; }
        public RoomType RoomType { get; set; }
        public decimal Price { get; set; }
    }
}