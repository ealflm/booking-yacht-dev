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
    public class TickService : BaseService, ITicketService
    {
        public TickService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<TicketViewModel>> SearchTickets(TicketSearchModel model = null)
        {
            model ??= new TicketSearchModel();
            var list = await _unitOfWork.TicketRepository.Query()
                .Where(x => model.NameCustomer == null | x.NameCustomer.Contains(model.NameCustomer))
                .Where(x => model.Phone == null | x.Phone.Equals(model.Phone))
                .Where(x => model.IdOrder == null | x.IdOrder.Equals(model.IdOrder))
                .Where(x => model.IdTrip == null | x.IdTrip.Equals(model.IdTrip))
                .Where(x => model.IdTicketType == null | x.IdTicketType.Equals(model.IdTicketType))
                .Where(x => model.Status == null | x.Status == (int) model.Status)
                .Where(x => model.Price == null | x.Price == model.Price)
                .OrderBy(x => x.NameCustomer)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.TicketRepository.Query().Count())
                .Select(x => new TicketViewModel
                {
                    Id = x.Id,
                    Price = x.Price,
                    IdOrder = x.IdOrder,
                    IdTicketType = x.IdTicketType,
                    IdTrip = x.IdTrip,
                    NameCustomer = x.NameCustomer,
                    Phone = x.Phone,
                    Status = x.Status
                }).ToListAsync();
            return list;
        }

        public async Task<List<Ticket>> SearchTicketsNavigation(TicketSearchModel model = null)
        {
            model ??= new TicketSearchModel();
            var list = await _unitOfWork.Context().Tickets
                .Include(x => x.IdOrderNavigation)
                .Include(x => x.IdTicketTypeNavigation)
                .Include(x => x.IdTripNavigation)
                .Where(x => model.NameCustomer == null | x.NameCustomer.Contains(model.NameCustomer))
                .Where(x => model.Phone == null | x.Phone.Equals(model.Phone))
                .Where(x => model.IdOrder == null | x.IdOrder.Equals(model.IdOrder))
                .Where(x => model.IdTrip == null | x.IdTrip.Equals(model.IdTrip))
                .Where(x => model.IdTicketType == null | x.IdTicketType.Equals(model.IdTicketType))
                .Where(x => model.Status == null | x.Status == (int) model.Status)
                .Where(x => model.Price == null | Equals(x.Price, model.Price))
                .OrderBy(x => x.NameCustomer)
                .Skip(model.AmountItem * (model.Page > 0 ? model.Page - 1 : 0))
                .Take(model.Page > 0 ? model.AmountItem 
                    : !_unitOfWork.TicketRepository.Query().Any() ? 1 
                    : _unitOfWork.TicketRepository.Query().Count())
                .ToListAsync();
            return list;
        }

        public async Task<TicketViewModel> GetTicket(Guid id)
        {
            return await _unitOfWork.TicketRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TicketViewModel()
                {
                    Id = x.Id,
                    Price = x.Price,
                    IdOrder = x.IdOrder,
                    IdTicketType = x.IdTicketType,
                    IdTrip = x.IdTrip,
                    NameCustomer = x.NameCustomer,
                    Phone = x.Phone,
                    Status = x.Status
                }).FirstOrDefaultAsync();
        }

        public async Task<Ticket> GetTicketNavigation(Guid id)
        {
            return await _unitOfWork.Context().Tickets
                .Include(x => x.IdOrderNavigation)
                .Include(x => x.IdTicketTypeNavigation)
                .Include(x => x.IdTripNavigation)
                .ThenInclude(x => x.IdVehicleNavigation)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
        }


        public async Task<Guid> AddTicket(TicketViewModel model)
        {
            var ticketType = _unitOfWork.TicketTypeRepository.Query().Where(x => x.Id.Equals(model.IdTicketType)).Select(x => new TicketType() { Price = x.Price }).FirstOrDefault();
            var entity = new Ticket()
            {
                Id = model.Id,
                Price = ticketType.Price,
                IdOrder = model.IdOrder,
                IdTicketType = model.IdTicketType,
                IdTrip = model.IdTrip,
                NameCustomer = model.NameCustomer,
                Phone = model.Phone,
                Status = (int)Status.NOT_SCANNED
            };
            var ticket = _unitOfWork.TicketRepository.Query().Add(entity);
            Trip trip = _unitOfWork.TripRepository.GetById(entity.IdTrip).Result;
            trip.AmountTicket++;
            _unitOfWork.TripRepository.Update(trip);
            await _unitOfWork.SaveChangesAsync();
            return ticket.Entity.Id;
        }

        public async Task<bool> UpdateTicket(Guid id, TicketViewModel model)
        {
            var ticket = _unitOfWork.TicketRepository.GetById(id);
            //not found entity:
            if (ticket.Result == null) return false;
            //entity existed:
            ticket.Result.Price = model.Price;
            ticket.Result.IdOrder = model.IdOrder;
            ticket.Result.IdTicketType = model.IdTicketType;
            ticket.Result.IdTrip = model.IdTrip;
            ticket.Result.NameCustomer = model.NameCustomer;
            ticket.Result.Phone = model.Phone;
            ticket.Result.Status = model.Status;

            _unitOfWork.TicketRepository.Update(ticket.Result);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTicket(Guid id)
        {
            var ticket = _unitOfWork.TicketRepository.GetById(id);
            //not found entity:
            if (ticket.Result == null) return false;
            //entity existed:

            ticket.Result.Status = (int) Status.DISABLE;

            _unitOfWork.TicketRepository.Update(ticket.Result);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<string> GetQRString(Guid id)
        {
            var ticket = _unitOfWork.TicketRepository.GetById(id);
            string result = "";
            if(ticket.Result != null && ticket.Result.Status==(int) Status.NOT_SCANNED)
            {
                result = ticket.Result.Id + "|" + ticket.Result.IdOrder + "|" + ticket.Result.IdTicketType + "|" + ticket.Result.IdTrip + "|" + ticket.Result.NameCustomer + "|" + ticket.Result.Phone + "|" + ticket.Result.Price + "|" + DateTime.Now;
                result = Base64Encode(result);
            }
            return result;
        }

        public async Task<Ticket> CheckQRString(string qr)
        {
            qr = Base64Decode(qr);
            string[] result= qr.Split('|');
            var ticket = _unitOfWork.TicketRepository.GetById(Guid.Parse(result[0]));
            if(ticket!= null)
            {
                if (!ticket.Result.IdOrder.Equals(Guid.Parse(result[1]))|| !ticket.Result.IdTicketType.Equals(Guid.Parse(result[2]))|| !ticket.Result.IdTrip.Equals(Guid.Parse(result[3]))|| !ticket.Result.NameCustomer.Equals(result[4])|| !ticket.Result.Phone.Equals(result[5])|| !ticket.Result.Price.Equals(double.Parse(result[6])))
                {
                    ticket = null;
                }
                else
                {
                    ticket.Result.Status = (int)Status.SCANNED;
                    _unitOfWork.TicketRepository.Update(ticket.Result);
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return ticket.Result;
        }

        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        
    }
}