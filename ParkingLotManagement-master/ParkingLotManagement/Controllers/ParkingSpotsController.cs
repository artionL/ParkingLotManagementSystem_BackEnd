using Microsoft.AspNetCore.Mvc;
using ParkingLotManagement.Models;
using ParkingLotManagement.Repositories;

namespace ParkingLotManagement.Controllers
{
    [ApiController]
    [Route("api/ParkingSpots")]
    public class ParkingSpotsController : Controller
    {
        private readonly ParkingSpotsRepository _parkingSpotRepository;
        public ParkingSpotsController(ParkingSpotsRepository parkingSpotRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
        }
        [HttpPost()]
        public IActionResult CreateParkingSpot(ParkingSpots parkingSpot)
        {
            _parkingSpotRepository.CreateParkingSpot(parkingSpot);
            return Ok();
        }
        // request qe kthen numrin e subsciberave ose vendet e rezervuara
        [HttpGet("Reserved")]
        public IActionResult GetReservedSpots()
        {
            int activeSubscriberCount = _parkingSpotRepository.GetReservedSpots();
            return Ok(activeSubscriberCount);
        }
        [HttpGet("Total")]
        public IActionResult GetTotalSpots()
        {
            int totalSpots = _parkingSpotRepository.GetTotalSpots();
            return Ok(totalSpots);
        }
        //request qe kthen vendet e lira = total - reserved
        [HttpGet("Regular")]
        public IActionResult GetRegularSpots()
        {
            int freeSpots = _parkingSpotRepository.GetRegularSpots();
            return Ok(freeSpots);
        }
        //Rrequest qe BEN update ParkingSpot qe merr si parameter ID
        [HttpPut("{Id}")]
        public IActionResult UpdateParkingSpot(int Id, ParkingSpots updatedParkingSpot)
        {
            var parkingSpot = new ParkingSpots
            {
                Id = Id,
                TotalSpots = updatedParkingSpot.TotalSpots
            };
            _parkingSpotRepository.UpdateParkingSpot(parkingSpot);
            return Ok();
        }
        [HttpGet("Occupied/Reserved")]
        public IActionResult GetOccupiedReservedSpots()
        {
            int occupiedReservedSpots = _parkingSpotRepository.GetOccupiedReservedSpots();
            return Ok(occupiedReservedSpots);
        }
        [HttpGet("Occupied/Regular")]
        public IActionResult GetOccupiedRegularSpots()
        {
            int occupiedRegularSpots = _parkingSpotRepository.GetOccupiedRegularSpots();
            return Ok(occupiedRegularSpots);
        }
        [HttpGet("Available/Regular")]
        public IActionResult GetAvailableRegularSpots()
        {
            int availableRegularSpots = _parkingSpotRepository.GetAvailableRegularSpots();
            return Ok(availableRegularSpots);
        }
        [HttpGet("Available/Reserved")]
        public IActionResult GetAvailableReservedSpots()
        {
            int availableReservedSpots = _parkingSpotRepository.GetAvailableReservedSpots();
            return Ok(availableReservedSpots);
        }
    }
}
