using ParkingLotManagement.Models;
namespace ParkingLotManagement.Repositories

{
    public class LogsRepository
    {
        private readonly ParkingDbContext _context;

        public LogsRepository(ParkingDbContext context)
        {
            _context = context;
        }
        public void CreateLog(Logs log)
        {
            var logCodeList = GetLogsByCode(log.Code).ToList();
            if (logCodeList.Count > 0)
            {
                throw new Exception("Log with the same code already exists");
            }

            var pricingPlanWeekend = _context.PricingPlans.Where(i => i.Type == PricingPlansType.Weekend).FirstOrDefault();

            var pricingPlanWeekday = _context.PricingPlans.Where(i => i.Type == PricingPlansType.Weekday).FirstOrDefault();

            if (log.SubscriptionsId == null)
            {
                if (log.CheckOutTime == null)
                {
                    log.Price = 0;
                }
                else
                {
                    

                    TimeSpan timeSpan = (log.CheckOutTime.Value - log.CheckInTime);

                    if (timeSpan.Minutes <= 15)
                    {
                        log.Price = 0;
                    }

                    else if (timeSpan.Hours <= pricingPlanWeekday.MinimumHours)
                    {
                        if (log.CheckOutTime.Value.DayOfWeek.ToString() == "Saturday" || log.CheckOutTime.Value.DayOfWeek.ToString() == "Sunday")
                        {
                            var timeExactHours = (double)timeSpan.Minutes / 60;
                            log.Price = timeExactHours * pricingPlanWeekend.HourlyPricing;
                        }
                        else
                        {
                            var timeExactHours = (double)timeSpan.Minutes / 60;
                            log.Price = timeExactHours * pricingPlanWeekday.HourlyPricing;

                        }

                    }

                    else
                    {
                        if (timeSpan.Hours <= 24)
                        {
                            if (log.CheckOutTime.Value.DayOfWeek.ToString() == "Saturday" || log.CheckOutTime.Value.DayOfWeek.ToString() == "Sunday")
                            {
                                log.Price = pricingPlanWeekend.DailyPricing;
                            }
                            else
                            {
                                log.Price = pricingPlanWeekday.DailyPricing;

                            }

                        }

                        else
                        {
                            int days = timeSpan.Days;


                            var timeExactHours = (double)timeSpan.Minutes / 60 - days * 24;



                            if (timeExactHours < pricingPlanWeekday.MinimumHours)
                            {
                                if (log.CheckOutTime.Value.DayOfWeek.ToString() == "Saturday" || log.CheckOutTime.Value.DayOfWeek.ToString() == "Sunday")
                                {
                                    log.Price = days * pricingPlanWeekday.DailyPricing + timeExactHours * pricingPlanWeekend.HourlyPricing;

                                }
                                else
                                {
                                    log.Price = days * pricingPlanWeekday.DailyPricing + timeExactHours * pricingPlanWeekday.HourlyPricing;

                                }
                            }

                            log.Price = (days + 1) * pricingPlanWeekday.DailyPricing;


                        }

                    }


                }


            }
            else
            {
                log.Price = 0;
            }

            _context.Logs.Add(log);
            _context.SaveChanges();
        }

        public List<Logs> GetAllLogs()
        {

            var logList = _context.Logs.ToList();
            return logList;
        }

        public List<Logs> GetLogsByDay(DateTime day)
        {

            var logList = _context.Logs.Where(x => x.CheckInTime.Date == day.Date).ToList();
            return logList;
        }

        public List<Logs> GetLogsByCode(string code)
        {
            var logList = _context.Logs.Where(x => x.Code == code).ToList();
            return logList;

        }

        public List<Logs> GetLogsBySubscriberName(string subscriberName)
        {
            //var subscriberNameList = _context.Subscribers.Where(x=> x.FirstName == subscriberName).Select(x=>x.Id).ToList();
            var repository = new SubscriptionsRepository(_context);

            var list = repository.GetSubscriptionsBySubscribersName(subscriberName).Select(x => x.Id);

            var logList = new List<Logs>();

            foreach (var listElement in list)
            {
                logList.Add(_context.Logs.Where(x => x.SubscriptionsId == listElement).FirstOrDefault());
            }

            return logList;

        }

        //Will not be needed, is not in the LogsController
        public void UpdateLogs(Logs logs)
        {

            throw new Exception("Not allowed to update logs!");
        }


        public void DeleteLogs(int id)
        {

            var log = _context.Logs.FirstOrDefault(x => x.Id == id);
            if (log != null)
            {
                _context.Logs.Remove(log);
                _context.SaveChanges();
            }

            else
            {
                throw new Exception("Log does not exist.");
            }
        }


    }
}
