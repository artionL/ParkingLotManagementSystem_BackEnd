using Microsoft.AspNetCore.Mvc;
using ParkingLotManagement.Models;
using ParkingLotManagement.Repositories;

namespace ParkingLotManagement.Controllers
{
    [ApiController]
    [Route("api/Logs")]
    public class LogsController : Controller
    {
        private readonly LogsRepository _logsRepository;
        public LogsController(LogsRepository logsRepository)
        {
            _logsRepository = logsRepository;
        }


        [HttpPost()]
        public IActionResult CreateLog(Logs log)
        {

            try { _logsRepository.CreateLog(log); }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok();

        }

        [HttpGet("AllLogs")]
        public IActionResult GetAllLogs()
        {
            var logsList = _logsRepository.GetAllLogs();
            return Ok(logsList);
        }

        [HttpGet("LogsByDay")]
        public IActionResult GetLogsByDay(DateTime day)
        {
            var logsList = _logsRepository.GetLogsByDay(day);
            return Ok(logsList);
        }


        [HttpGet("LogsByCode")]
        public IActionResult GetLogsByCode(string code)
        {
            var logsList = _logsRepository.GetLogsByCode(code);
            return Ok(logsList);
        }


        [HttpGet("LogsBySubscriberName")]
        public IActionResult GetLogsBySubscriberName(string subscriberName)
        {
            var logsList = _logsRepository.GetLogsBySubscriberName(subscriberName);
            return Ok(logsList);
        }


        [HttpDelete("{Id}")]
        public IActionResult DeleteLogs(int logId)
        {
            try { _logsRepository.DeleteLogs(logId); }
            catch (Exception e) { 
                return BadRequest(e.Message);
            }
            return Ok();

        }
       
    }
}
