using Common.Kernel;

namespace Commands.Pricing
{
    public class SetPricingForRoom
    {
        public RoomType RoomType { get; set; }
        public decimal Pricing { get; set; }
    }
}