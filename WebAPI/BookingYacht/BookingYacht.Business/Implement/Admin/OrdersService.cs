using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.NotificationUtils;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Admin
{

    public class OrdersService : BaseService, IOrdersService
    {

        public OrdersService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<OrdersViewModel>> Search(OrdersSearchModel model)
        {
            
            model ??= new OrdersSearchModel();
            var list = await _unitOfWork.OrderRepository.Query()
                .Where(x => model.AgencyName == null || x.AgencyName.Contains(model.AgencyName))
                .Where(x => model.IdAgency == null || x.IdAgency.Equals(model.IdAgency))
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                .Where(x => model.TotalPrice == null || x.TotalPrice == model.TotalPrice)
                .Where(x => model.QuantityOfPerson == null || x.QuantityOfPerson == model.QuantityOfPerson)
                .Where(x => model.Status == null || x.Status == (int) model.Status)
                .Join(
                    _unitOfWork.Context().Trips,
                    order => order.IdTrip,
                    trip => trip.Id, 
                    (order, trip) => new {Trip = trip, order})
                .Join(_unitOfWork.Context().BusinessTours, 
                    arg => arg.Trip.IdBusinessTour,
                    businessTour => businessTour.Id,
                    (__, businessTour) => new {BusinessTour = businessTour, __.order})
                .Join(_unitOfWork.Context().Tours,
                    arg => arg.BusinessTour.IdTour,
                    tour => tour.Id,
                    (__, tour) => new {Tour = tour, __.order})
                .Join(_unitOfWork.Context().Agencies, 
                    arg => arg.order.IdAgency, 
                    agency => agency.Id,
                    (__, agencies) => new {Agency = agencies, __.order, __.Tour})
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.OrderRepository.Query().Count())
                .OrderBy(x => x.Tour.Title)
                .Select( arg => new OrdersViewModel
                {
                    Id = arg.order.Id,
                    Status = (Status)arg.order.Status,
                    AgencyName = arg.order.AgencyName,
                    IdAgency = arg.order.IdAgency,
                    IdTrip = arg.order.IdTrip,
                    OrderDate = arg.order.DateOrder.Value,
                    QuantityOfPerson = arg.order.QuantityOfPerson,
                    TotalPrice = arg.order.TotalPrice ?? 0.00,
                    TourName = arg.Tour.Title,
                    AgencyViewModels = new AgencyViewModels
                    {
                        Address = arg.Agency.Address,
                        EmailAddress = arg.Agency.EmailAddress,
                        Id = arg.Agency.Id,
                        Name = arg.Agency.Name,
                        PhoneNumber = arg.Agency.PhoneNumber,
                        Status = arg.Agency.Status,
                        PhotoUrl = arg.Agency.PhotoUrl
                    }
                })
                .ToListAsync();
            return list;
        }

        public async Task<OrdersViewModel> Get(Guid id)
        {
            return await _unitOfWork.OrderRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new OrdersViewModel()
                {
                    Id = x.Id,
                    AgencyName = x.AgencyName,
                    IdAgency = x.IdAgency,
                    QuantityOfPerson = x.QuantityOfPerson,
                    Status = (Status) x.Status,
                    OrderDate = x.DateOrder.GetValueOrDefault(),
                    TotalPrice = x.TotalPrice ?? 0,
                    IdTrip= x.IdTrip
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Order> GetNavigation(Guid id)
        {
            return await _unitOfWork.Context().Orders
                .Include(x => x.IdAgencyNavigation)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
        }


        public async Task<Order> Add(OrderCreateModel model)
        {
            
            //Add Order to DB
            var agency = await _unitOfWork.AgencyRepository.GetById(model.IdAgency);
            
            var addOrder = new Order()
            {
                
                QuantityOfPerson = model.QuantityOfPerson ?? 0,
                IdAgency = model.IdAgency,
                IdTrip = model.IdTrip!,
                AgencyName = agency.Name,
                DateOrder = model.OrderDate,
                Status = (int)Status.COMPLETELY_PAYMENT,
                TotalPrice = model.TotalPrice 
            };

            var result = await _unitOfWork.Context().Orders.AddAsync(addOrder);
            await _unitOfWork.SaveChangesAsync();
            //Get Token Business:
            var token = await _unitOfWork.TripRepository.Query()
                .Where(x => x.Id.Equals(result.Entity.IdTrip))
                .Join(_unitOfWork.Context().BusinessTours,
                    arg => arg.IdBusinessTour,
                    tour => tour.Id,
                    (arg, tour) => new { BusinessTour = tour })
                .Join(_unitOfWork.Context().Businesses,
                    arg => arg.BusinessTour.IdBusiness,
                    business => business.Id,
                    (__, business) => business.FcmToken)
                .FirstOrDefaultAsync();
        
            //Send notification - FCM: 
            var name = FirebaseApp.DefaultInstance.Name;
            Console.WriteLine("Firebase:" + name);

            await FcmService.SendNotification(result.Entity, token);
            return result.Entity;
        }

      
        
        public async Task<bool> Update(Guid id, OrdersViewModel model)
        {
            var order = await _unitOfWork.OrderRepository.GetById(id);
            if (order == null) return false;
            order.Status =(int)model.Status;
            order.AgencyName = model.AgencyName;
            order.IdAgency = model.IdAgency;
            order.QuantityOfPerson = model.QuantityOfPerson;
            order.TotalPrice = model.TotalPrice;
            order.IdTrip = model.IdTrip;
            _unitOfWork.OrderRepository.Query().Update(order);
            
            await _unitOfWork.SaveChangesAsync();
            return true;

        }

        public async Task<bool> Delete(Guid id)
        {
            var order = _unitOfWork.OrderRepository.GetById(id).Result;
            
            if (order== null) return false;

            order.Status = (int) Status.DISABLE;
            
            _unitOfWork.OrderRepository.Update(order);
            
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatus(Guid id, Status status)
        {
            var order = _unitOfWork.OrderRepository.GetById(id).Result;

            if (order == null) return false;

            order.Status = (int)status;

            _unitOfWork.OrderRepository.Update(order);

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<int> Count()
        {
            return await _unitOfWork.Context().Orders.CountAsync();
        }
    }
}