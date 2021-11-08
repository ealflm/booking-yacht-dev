using System;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Implement;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.NotificationUtils
{
    public static class FcmService {
        public static async Task SendNotification(Order order, string clientToken)
        {
            //Define Message: 
            var message = new Message()
            {
                
                Token = clientToken,
                Notification = new Notification()
                {
                    
                    Title = "Đặt Tàu",
                    Body = $"{order.AgencyName} đã đặt {order.QuantityOfPerson} vé {order.DateOrder}",
                    ImageUrl = "https://swd3915.blob.core.windows.net/images/steve-lacey-TtsuvND2Ick-unsplash.jpg"
                },
                
            };

            //Send message to device correspond 
            var response =
                await FirebaseMessaging.DefaultInstance.SendAsync(message);
            Console.WriteLine("this " + response);
        }
    }
}