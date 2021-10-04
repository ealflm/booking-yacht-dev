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
        public Task<List<OrdersViewModel>> Search(OrdersSearchModel model);

        public Task<OrdersViewModel> Get(Guid id);

        public Task<Guid> Add(OrdersViewModel model);

        public Task<bool> Update(Guid id, OrdersViewModel model);

        public Task<bool> Delete(Guid id);

    }
}