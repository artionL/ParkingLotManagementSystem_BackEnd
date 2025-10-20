using Microsoft.AspNetCore.Mvc;
using ParkingLotManagement.Models;

namespace ParkingLotManagement.Repositories
{
    public class ParkingSpotsRepository
    {
        private readonly ParkingDbContext _context;
        public ParkingSpotsRepository(ParkingDbContext context)
        {
            _context = context;
        }
        public void CreateParkingSpot(ParkingSpots parkingSpot)
        {
            _context.ParkingSpots.Add(parkingSpot);
            _context.SaveChanges();
        }
        public void UpdateParkingSpot(ParkingSpots updatedParkingSpot)
        {
            var existingParkingSpot = _context.ParkingSpots.FirstOrDefault(p => p.Id == updatedParkingSpot.Id);
            if (existingParkingSpot != null)
            {
                existingParkingSpot.TotalSpots = updatedParkingSpot.TotalSpots;
            }
            _context.SaveChanges();
        }
        public int GetReservedSpots()
        {
            var activeSubscriberCount = _context.Subscriptions.Count(subscriber => !subscriber.IsDeleted);
            return activeSubscriberCount;
        }
        public int GetTotalSpots()
        {
            var exists = _context.ParkingSpots.Count();
            if (exists == 0)
            {
                throw new Exception("You havent added total spots in database");
            }
            else
            {
                var totalSpots = _context.ParkingSpots.FirstOrDefault().TotalSpots;
                return totalSpots;
            }
        }
        public int GetRegularSpots()
        {
            int totalSpots = GetTotalSpots();
            int reservedSpots = GetReservedSpots();
            int freeSpots = totalSpots - reservedSpots;
            return freeSpots;
        }
        public int GetOccupiedRegularSpots()
        {
            var checkedInRegularSpots = _context.Logs.Count(log => log.CheckOutTime == null && log.SubscriptionsId == null);
            return checkedInRegularSpots;
        }
        public int GetOccupiedReservedSpots()
        {
            var checkedInReservedSpots = _context.Logs.Count(log => log.CheckOutTime == null && log.SubscriptionsId != null);
            return checkedInReservedSpots;
        }
        public int GetAvailableRegularSpots()
        {
            var availableRegularSpots = GetRegularSpots() - GetOccupiedRegularSpots();
            return availableRegularSpots;
        }
        public int GetAvailableReservedSpots()
        {
            var availableReservedSpots = GetReservedSpots() - GetOccupiedReservedSpots();
            return availableReservedSpots;
        }
    }
}
