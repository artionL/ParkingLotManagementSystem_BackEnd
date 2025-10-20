namespace ParkingLotManagement.Models
{
    public class PricingPlans
    {
        public int Id { get; set; }
        public double HourlyPricing { get; set; }
        public double DailyPricing { get; set; }
        public int MinimumHours { get; set; }
        public PricingPlansType Type { get; set; }

    }


    public enum PricingPlansType
    {
        Weekday,
        Weekend
    }
}
