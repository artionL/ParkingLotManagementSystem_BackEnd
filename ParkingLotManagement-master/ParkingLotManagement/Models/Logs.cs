namespace ParkingLotManagement.Models
{
    public class Logs
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public double? Price { get; set; }
        public int? SubscriptionsId { get; set; }
        public Subscriptions? Subscriptions { get; set; }
    }
}
