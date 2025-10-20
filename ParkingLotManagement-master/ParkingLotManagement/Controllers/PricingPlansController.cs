using Microsoft.AspNetCore.Mvc;
using ParkingLotManagement.Models;
using ParkingLotManagement.Repositories;

namespace ParkingLotManagement.Controllers
{
    [ApiController]
    [Route("api/PricingPlans")]
    public class PricingPlansController : Controller
    {
        private readonly PricingPlansRepository _pricingPlansRepository;
        public PricingPlansController(PricingPlansRepository pricingPlansRepository)
        {
            _pricingPlansRepository = pricingPlansRepository;
        }
        [HttpPost()]
        public IActionResult CreatePricingPlans(PricingPlans pricingPlan)
        {
            _pricingPlansRepository.CreatePricingPlans(pricingPlan);
            return Ok();
        }
        
        //Rrequest qe BEN update ParkingSpot qe merr si parameter ID
        [HttpPut("{Id}")]
        public IActionResult UpdatePricingPlans(int Id, PricingPlans updatedPricingPlans)
        {
            var pricingPlans = new PricingPlans
            {
                Id = Id,
                HourlyPricing = updatedPricingPlans.HourlyPricing,
                DailyPricing = updatedPricingPlans.DailyPricing,
                MinimumHours = updatedPricingPlans.MinimumHours,
                Type = updatedPricingPlans.Type
            };

            try
            {
                _pricingPlansRepository.UpdatePricingPlans(pricingPlans);
                return Ok();

            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
        [HttpGet()]
        public IActionResult GetPricingPlans()
        {

            var pricingPlans = _pricingPlansRepository.GetPricingPlans();
            return Ok(pricingPlans);
        }
        [HttpDelete("{Id}")]
        public IActionResult DeletePricingPlans(int Id)
        {
            try
            {
                _pricingPlansRepository.DeletePricingPlans(Id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
