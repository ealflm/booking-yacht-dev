using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface IOrdersService
    {
        Task<List<OrdersViewModel>> Search(OrdersSearchModel model);

        Task<List<Order>> SearchNavigation(OrdersSearchModel model);

        Task<OrdersViewModel> Get(Guid id);

        Task<Order> GetNavigation(Guid id);

        Task<Guid> Add(OrdersViewModel model);

        Task<bool> Update(Guid id, OrdersViewModel model);

        Task<bool> Delete(Guid id);
        Task<bool> UpdateStatus(Guid id, Enum.Status status);

    }
}