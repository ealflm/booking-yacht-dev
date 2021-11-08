using System;
using System.Threading.Tasks;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.NotificationUtils
{
    public interface IFcmService
    {
        public Task SendNotification(Order order);
    }
}