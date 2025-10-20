using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ParkingLotManagement.Models;

namespace ParkingLotManagement.Repositories
{
    public class PricingPlansRepository
    {
        private readonly ParkingDbContext _context;
        public PricingPlansRepository(ParkingDbContext context)
        {
            _context = context;
        }
        public void CreatePricingPlans(PricingPlans pricingPlan)
        {
            _context.PricingPlans.Add(pricingPlan);
            _context.SaveChanges();
        }
        public void UpdatePricingPlans(PricingPlans updatedPricingPlans)
        {
            var existingPricingPlans = _context.PricingPlans.FirstOrDefault(p => p.Id == updatedPricingPlans.Id);
            if (existingPricingPlans != null)
            {
                existingPricingPlans.HourlyPricing = updatedPricingPlans.HourlyPricing;
                existingPricingPlans.DailyPricing = updatedPricingPlans.DailyPricing;
                existingPricingPlans.MinimumHours = updatedPricingPlans.MinimumHours;
                existingPricingPlans.Type = updatedPricingPlans.Type;
                _context.SaveChanges();

            }
            else
            {
                throw new Exception("Pricing plan with this ID does not exist");
            }
        }
        
        public List<PricingPlans> GetPricingPlans()
        {
            var totalPricingPlans = _context.PricingPlans.ToList();
            return totalPricingPlans;
        }

        public void DeletePricingPlans(int id)
        {
            var pricingPlan = _context.PricingPlans.Find(id);
            if (id != null)
            {
                _context.PricingPlans.Remove(pricingPlan);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Pricing plan with this ID does not exist");
            }
            
        }

    }
}
