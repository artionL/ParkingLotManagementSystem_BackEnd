using ParkingLotManagement.Models;

namespace ParkingLotManagement.Repositories
{
    public class SubscriptionsRepository
    {
        private readonly ParkingDbContext _context;
        public SubscriptionsRepository(ParkingDbContext context)
        {
            _context = context;
        }
        public List<Subscriptions> GetSubscriptions()
        {
            var allSubscriptions = _context.Subscriptions.ToList();
            return allSubscriptions;
        }
        public void CreateSubscription(Subscriptions subscription)
        {
            if (_context.Subscriptions.Any(x => x.Code == subscription.Code))
            {
                throw new Exception("Subscription with this code already exists");
            }
            else
            {
                if(_context.Subscribers.Any(x => x.Id == subscription.SubscribersId))
                {
                    _context.Subscriptions.Add(subscription);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Subscriber with this ID does not exist.");
                }
            }
        }

        public List<Subscriptions> GetSubscriptionsByCode(string code)
        {
            var subscriptions = _context.Subscriptions.Where(x => x.Code == code).ToList();
            return subscriptions;
        }
        public List<Subscriptions> GetSubscriptionsBySubscribersName(string name)
        {
            var subsribers = _context.Subscribers.Where(y => y.FirstName == name).Select(i => i.Id).ToList();
            var subscriptions = new List<Subscriptions>();
            foreach (var subscriberId in subsribers)
            {
                subscriptions.Add(_context.Subscriptions.Where(i => i.SubscribersId == subscriberId && i.IsDeleted == false).FirstOrDefault());
            }
                return subscriptions;
        }
        public void UpdateSubscriptions(Subscriptions updatedSubscriptions)
        {
            var existingSubscriptions = _context.Subscriptions.FirstOrDefault(p => p.Id == updatedSubscriptions.Id);
            if (existingSubscriptions.Id == updatedSubscriptions.Id)
            {
                existingSubscriptions.Code = updatedSubscriptions.Code;
                //existingSubscriptions.SubscribersId = updatedSubscriptions.SubscribersId;
                existingSubscriptions.Price = updatedSubscriptions.Price;
                existingSubscriptions.DiscountValue = updatedSubscriptions.DiscountValue;
                existingSubscriptions.StartDate = updatedSubscriptions.StartDate;
                existingSubscriptions.EndDate = updatedSubscriptions.EndDate;
                existingSubscriptions.IsDeleted = updatedSubscriptions.IsDeleted;


            }
            _context.SaveChanges();
        }
        public void DeleteSubscriptions(int id )
        {
            var existingSubscriptions = _context.Subscriptions.Find(id);
            if (existingSubscriptions != null)
            {
                existingSubscriptions.IsDeleted = true;
                
                _context.SaveChanges();
            }
        }
    }
}
