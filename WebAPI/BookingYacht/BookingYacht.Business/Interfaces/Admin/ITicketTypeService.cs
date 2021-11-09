using System;
using BookingYacht.Business.ViewModels;
using BookingYacht.Business.SearchModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface ITicketTypeService
    {
        Task<List<TicketType>> SearchNavigation(TicketTypeSearchModel model = null);
        Task Set(Guid id, TicketTypeViewModel model);
        Task<TicketType> GetNavigation(Guid id);
    }
}