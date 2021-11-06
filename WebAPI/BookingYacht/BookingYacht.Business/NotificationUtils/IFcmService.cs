using System;
using System.Threading.Tasks;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.NotificationUtils
{
    public interface IFcmService
    {
        public Task SendNotification(
            Guid idTrip,
            string privateKey,
            Order order);
        
        
        
    }
}