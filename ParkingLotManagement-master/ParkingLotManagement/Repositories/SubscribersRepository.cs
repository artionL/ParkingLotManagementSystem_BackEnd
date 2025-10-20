using Microsoft.AspNetCore.Http.HttpResults;
using ParkingLotManagement.Models;

namespace ParkingLotManagement.Repositories
{
    public class SubscribersRepository
    {
        private readonly ParkingDbContext _context;
        public SubscribersRepository(ParkingDbContext context)
        {
            _context = context;
        }
        public List<Subscribers> GetSubscribers()
        {
            var allSubscribers = _context.Subscribers.ToList();
            return allSubscribers;
        }
        public void CreateSubscribers(Subscribers subscribers)
        {
            if(_context.Subscribers.Any(x => x.IdCardNumber == subscribers.IdCardNumber))
            {
                throw new Exception("User already exists.");
            }
            _context.Subscribers.Add(subscribers);
            _context.SaveChanges();
        }
        public void UpdateSubscribers(Subscribers updatedSubscribers)
        {
            var existingSubscribers = _context.Subscribers.FirstOrDefault(p => p.Id == updatedSubscribers.Id);
            if (existingSubscribers.Id == updatedSubscribers.Id)
            {
                existingSubscribers.FirstName = updatedSubscribers.FirstName;
                existingSubscribers.LastName = updatedSubscribers.LastName;
                existingSubscribers.IdCardNumber = updatedSubscribers.IdCardNumber;
                existingSubscribers.Email = updatedSubscribers.Email;
                existingSubscribers.PhoneNumber = updatedSubscribers.PhoneNumber;
                existingSubscribers.Birthday = updatedSubscribers.Birthday;
                existingSubscribers.PlateNumber = updatedSubscribers.PlateNumber;
            }
            _context.SaveChanges();
        }

        public List<Subscribers> GetSubscribersByFirstName(string firstName)
        {
            var firstNameList= _context.Subscribers.Where(x => x.FirstName==firstName).ToList();
            return firstNameList;
        }
        public List<Subscribers> GetSubscribersByLastName(string lastName)
        {
            var lastNameList = _context.Subscribers.Where(x => x.LastName == lastName).ToList();
            return lastNameList;
        }
        public List<Subscribers> GetSubscribersByIdCardNumber(string idCardNumber)
        {
            var idCardNumberList = _context.Subscribers.Where(x => x.IdCardNumber == idCardNumber).ToList();
            return idCardNumberList;
        }
        public List<Subscribers> GetSubscribersByEmail(string email)
        {
            var emailList = _context.Subscribers.Where(x => x.Email == email).ToList();
            return emailList;
        }
        public Subscribers GetById(int id)
        {
            var subscriber = _context.Subscribers.Find(id);
            if(subscriber != null)
            {
                return subscriber;
            }
            return null;
        }

        public void DeleteSubscribers(int id)
        {
            var existingSubscriber = _context.Subscribers.Find(id);
            if (existingSubscriber != null)
            {
                existingSubscriber.IsDeleted = true;
                existingSubscriber.IdCardNumber = "old_" + DateTime.Now.ToString() + existingSubscriber.IdCardNumber;
                _context.SaveChanges();
            }
        }
    }
}
