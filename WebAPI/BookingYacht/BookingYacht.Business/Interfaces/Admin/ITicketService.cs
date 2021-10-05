using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Models;

namespace BookingYacht.Business.Interfaces.Admin
{
    public interface ITicketService
    {
        Task<List<TicketViewModel>> SearchTickets(TicketSearchModel model=null);
        Task<List<Ticket>> SearchTicketsNavigation(TicketSearchModel model = null);
        Task<TicketViewModel> GetTicket(Guid id);
        Task<Ticket> GetTicketNavigation(Guid id);
        Task<Guid> AddTicket(TicketViewModel model);
        Task<bool> UpdateTicket(Guid id, TicketViewModel model);
        Task<bool> DeleteTicket(Guid id);
    }
}