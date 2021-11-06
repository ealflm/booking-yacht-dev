using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Implement;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.NotificationUtils
{
    public class FcmService : BaseService, IFcmService
    {
        public FcmService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task SendNotification(
            Guid idTrip,
            string privateKey,
            Order order)
        {
            var clientToken = await Notification(idTrip);

            if (clientToken is null)
            {
                return;
            }

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(privateKey)
            });

            //Define Message: 
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    { "myData", "data" }
                },
                Token = clientToken,
                Notification = new Notification()
                {
                    Title = "New Order",
                    Body = $"{order.AgencyName} booking {order.DateOrder}"
                }
            };

            //Send message to device correspond 
            var response = FirebaseMessaging.DefaultInstance.SendAsync(message).Result;
            Console.WriteLine(response);
        }

        private async Task<string> Notification(Guid trip)
        {
            var business = await _unitOfWork.TripRepository.Query()
                .Where(x => x.Id.Equals(trip))
                .Join(_unitOfWork.Context().BusinessTours,
                    arg => arg.IdBusinessTour,
                    arg => arg.Id,
                    (tour, arg) => new { BusinessTour = arg })
                .Join(_unitOfWork.Context().Businesses,
                    arg => arg.BusinessTour.IdBusiness,
                    business => business.Id,
                    (__, business) => new { business.FcmToken })
                .FirstOrDefaultAsync();
            return business.FcmToken;
        }
    }
}