using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingYacht.Business.Implement.Admin
{
    internal static class  MapperOrder
    {
        public static async Task<OrdersViewModel> GetViewModel(Order order)
        {
            return await Task.Run(() => new OrdersViewModel()
            {
                Id = order.Id,
                AgencyName = order.AgencyName,
                IdAgency = order.IdAgency,
                QuantityOfPerson = order.QuantityOfPerson,
                Status = (Status) order.Status,
                TotalPrice = order.TotalPrice ?? 0
            });
        }

        public static async Task<Order> GetEntity(OrdersViewModel model)
        {
            return await Task.Run(() => new Order
            {
                Id = model.Id,
                AgencyName = model.AgencyName,
                IdAgency = model.IdAgency,
                QuantityOfPerson = model.QuantityOfPerson,
                Status = (int) model.Status,
                TotalPrice = model.TotalPrice
            });
        }
        
    }
    
    public class OrdersService : BaseService, IOrdersService
    {
        private const int Count = (int)CountElement.Medium;

        public OrdersService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<OrdersViewModel>> Search(OrdersSearchModel model)
        {
            
            model ??= new OrdersSearchModel();
            var list = await _unitOfWork.OrderRepository.Query()
                .Where(x => model.Id == null | x.Id.Equals(model.Id))
                .Where(x => model.AgencyName == null | x.AgencyName.Contains(model.AgencyName))
                .Where(x => model.IdAgency == null | x.IdAgency.Equals(model.IdAgency))
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                .Where(x => model.TotalPrice == null | x.TotalPrice == model.TotalPrice)
                .Where(x => model.QuantityOfPerson == null | x.QuantityOfPerson == model.QuantityOfPerson)
                .Where(x => model.Status == null | x.Status == (int) model.Status)
                .Skip(Count * (model.Paging != 0 ? model.Paging - 1 : 0))
                .Take(model.Paging != 0 ? Count : _unitOfWork.BusinessRepository.Query().Count())
                .OrderBy(x => x.AgencyName)
                .Select(x => MapperOrder.GetViewModel(x).Result)
                .ToListAsync();
            return list;
        }

        public async Task<OrdersViewModel> Get(Guid id)
        {
            return await _unitOfWork.OrderRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => MapperOrder.GetViewModel(x).Result)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> Add(OrdersViewModel model)
        {
            var order = _unitOfWork.OrderRepository.Query()
                .Add(MapperOrder.GetEntity(model).Result);
            await _unitOfWork.SaveChangesAsync();
            return order.Entity.Id;
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
    }
}