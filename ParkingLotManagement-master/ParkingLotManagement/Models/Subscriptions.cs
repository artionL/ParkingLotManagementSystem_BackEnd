using System.ComponentModel.DataAnnotations;

namespace ParkingLotManagement.Models
{
    public class Subscriptions
    {
        public int Id { get; set; }
        public required string Code { get; set; } //?????
        public double Price { get; set; }
        public double DiscountValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public int SubscribersId { get; set; }
        public Subscribers? Subscribers { get; set; }


    }
    
}
