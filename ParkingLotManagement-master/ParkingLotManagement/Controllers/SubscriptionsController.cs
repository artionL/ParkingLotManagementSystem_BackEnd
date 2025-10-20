using Microsoft.AspNetCore.Mvc;
using ParkingLotManagement.Models;
using ParkingLotManagement.Repositories;

namespace ParkingLotManagement.Controllers
{
    [ApiController]
    [Route("api/Subscriptions")]
    public class SubscriptionsController : Controller
    {
        private readonly SubscriptionsRepository _subscriptionsRepository;
        public SubscriptionsController(SubscriptionsRepository subscriptionsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
        }
        [HttpPost()]
        public IActionResult CreateSubscription(Subscriptions subscriptions)
        {
            _subscriptionsRepository.CreateSubscription(subscriptions);
            return Ok();
        }
        [HttpGet]
        public IActionResult GetSubscriptions()
        {
            var subscriptionsList = _subscriptionsRepository.GetSubscriptions();
            return Ok(subscriptionsList);
        }
        [HttpGet("{code}")]
        public List<Subscriptions> GetSubscriptionsByCode(string code)
        {
            var subscriptions = _subscriptionsRepository.GetSubscriptionsByCode(code);
            if (subscriptions == null || subscriptions.Count == 0)
            {
                return new List<Subscriptions>();
            }
            return subscriptions;
        }
        [HttpGet("firstName/{firstName}")]
        public List<Subscriptions> GetSubscriptionsBySubscribersName(string firstName)
        {
            var subscriptions = _subscriptionsRepository.GetSubscriptionsBySubscribersName(firstName);
            if (subscriptions == null || subscriptions.Count == 0)
            {
                return new List<Subscriptions>();

            }
            return subscriptions;
        }
        [HttpPut("{Id}")]
        public IActionResult UpdateSubsciptions(int Id, Subscriptions updatedSubscriptions)
        {
            var subscriptions = new Subscriptions
            {
                Id = Id,
                Code=updatedSubscriptions.Code,
                Price=updatedSubscriptions.Price,
                DiscountValue = updatedSubscriptions.DiscountValue,
                StartDate = updatedSubscriptions.StartDate,
                EndDate = updatedSubscriptions.EndDate
            };
            _subscriptionsRepository.UpdateSubscriptions(subscriptions);
            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteSubscriptions(int Id )
        {
            _subscriptionsRepository.DeleteSubscriptions(Id);
            return Ok();
        }

    }
}
