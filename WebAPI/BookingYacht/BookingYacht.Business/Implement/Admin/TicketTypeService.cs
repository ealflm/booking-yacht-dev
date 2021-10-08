using System;
using BookingYacht.Data.Models;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.Implement.Admin
{
    public class TicketTypeService : BaseService, ITicketTypeService
    {

        public TicketTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<TicketType> GetNavigation(Guid id)
        {
            var ticketType = await _unitOfWork.Context().TicketTypes
                .Include(x => x.IdBusinessTourNavigation)
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            return ticketType;
        }

        public async Task<List<TicketType>> SearchNavigation(TicketTypeSearchModel model = null)
        {
            model ??= new TicketTypeSearchModel();
            var ticketTypes = await _unitOfWork.Context().TicketTypes
                .Include(x => x.IdBusinessTourNavigation)
                .Where(x => model.Status == Status.ALL | x.Status == (int)model.Status)
                .OrderBy(x => x.Status)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.BusinessRepository.Query().Count())
                .ToListAsync();
            return ticketTypes;
        }

        public async Task Set(Guid id, TicketTypeViewModel model)
        {
            var ticketType = await _unitOfWork.TicketTypeRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TicketType()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Status = x.Status,
                    IdBusinessTour= x.IdBusinessTour,
                    Price= x.Price,
                    ServiceFeePercentage=x.ServiceFeePercentage
                }).FirstOrDefaultAsync();
            ticketType.Status = model.Status;
            _unitOfWork.TicketTypeRepository.Update(ticketType);
            await _unitOfWork.SaveChangesAsync();
        }

 
    }
}
