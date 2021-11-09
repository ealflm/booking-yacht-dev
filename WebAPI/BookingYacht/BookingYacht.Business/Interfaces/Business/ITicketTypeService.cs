using System;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Interfaces.Business
{
    public interface ITicketTypeService
    {
        Task<List<TicketTypeViewModel>> SearchTicketTypes(TicketTypeSearchModel model = null);
        Task<TicketTypeViewModel> GetTicketType(Guid id);
        Task<Guid> AddTicketType(TicketTypeViewModel model);
        Task UpdateTicketType(Guid id, TicketTypeViewModel model);
        Task DeleteTicketType(Guid id);
    }
}