using System;
using BookingYacht.Business.Interfaces.Business;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingYacht.Business.Enum;

namespace BookingYacht.Business.Implement.Business
{
    public class TicketTypeService : BaseService, ITicketTypeService
    {
        public TicketTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Guid> AddTicketType(TicketTypeViewModel model)
        {

            var businessTour = await _unitOfWork.BusinessTourRepository.Query()
                .Where(x => x.IdBusiness.Equals(model.IdBusiness))
                .Where(x => x.IdTour.Equals(model.IdTour))
                .FirstOrDefaultAsync();
            if (businessTour == null)
            {
                businessTour = new BusinessTour
                {
                    IdTour = model.IdTour,
                    IdBusiness = model.IdBusiness,
                    Status = (int)Status.ENABLE
                };
                _unitOfWork.BusinessTourRepository.Query().Add(businessTour);
            }
            await _unitOfWork.SaveChangesAsync();
            var ticketType = new TicketType()
            {
                Id = model.Id,
                Price = model.Price,
                Name= model.Name,
                CommissionFeePercentage = model.CommissionFeePercentage,
                ServiceFeePercentage = model.ServiceFeePercentage,
                IdBusinessTour = businessTour.Id,
                Status = model.Status
            };
            ticketType.Status = (int)Status.WAITING;
            await _unitOfWork.TicketTypeRepository.Add(ticketType);
            await _unitOfWork.SaveChangesAsync();
            return ticketType.Id;
        }

        public async Task DeleteTicketType(Guid id)
        {
            var ticketType = await _unitOfWork.TicketTypeRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TicketType()
                {
                    Id = x.Id,
                    Price = x.Price,
                    ServiceFeePercentage = x.ServiceFeePercentage,
                    IdBusinessTour = x.IdBusinessTour,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            ticketType.Status = (int)Status.CANCEL;
            _unitOfWork.TicketTypeRepository.Update(ticketType);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TicketTypeViewModel> GetTicketType(Guid id)
        {
            var ticketType = await _unitOfWork.TicketTypeRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TicketTypeViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    CommissionFeePercentage = x.CommissionFeePercentage,
                    ServiceFeePercentage = x.ServiceFeePercentage,
                    IdBusinessTour = x.IdBusinessTour,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return ticketType;
        }

        public async Task<List<TicketTypeViewModel>> SearchTicketTypes(TicketTypeSearchModel model = null)
        {
            model ??= new TicketTypeSearchModel();
            var ticketType = await _unitOfWork.TicketTypeRepository.Query()
                .Where(x => model.Price == null || Equals(x.Price, model.Price))
                .Where(x => model.CommissionFeePercentage == null || x.CommissionFeePercentage == model.CommissionFeePercentage)
                .Where(x => model.ServiceFeePercentage == null || x.ServiceFeePercentage == model.ServiceFeePercentage)
                .Where(x => model.IdBusinessTour == null || x.IdBusinessTour.Equals(model.IdBusinessTour))
                .Where(x => model.Status == Status.ALL || x.Status == (int)model.Status)
                .Join(_unitOfWork.Context().BusinessTours, 
                        type => type.IdBusinessTour,
                        tour => tour.Id,
                        (type, tour) => new {TicketType = type, BusinessTour = tour})
                .Join(_unitOfWork.Context().Tours, 
                    arg => arg.BusinessTour.IdTour,
                    tour => tour.Id,
                    (__, tour) => new {__.TicketType, tour.Id, tour.Title})
                .Select(x => new TicketTypeViewModel()
                {
                    Id = x.TicketType.Id,
                    Name = x.TicketType.Name,
                    Price = x.TicketType.Price,
                    CommissionFeePercentage = x.TicketType.CommissionFeePercentage,
                    ServiceFeePercentage = x.TicketType.ServiceFeePercentage,
                    IdTour = x.Id,
                    TourName = x.Title,
                    IdBusinessTour = x.TicketType.IdBusinessTour,
                    Status = x.TicketType.Status,
                })
                .OrderBy(x => x.Name)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.TicketTypeRepository.Query().Count())
                .ToListAsync();
            
            return ticketType;
        }

        public async Task UpdateTicketType(Guid id, TicketTypeViewModel model)
        {
            var ticketType = new TicketType()
            {
                Id = id,
                Name = model.Name,
                Price = model.Price,
                ServiceFeePercentage = model.ServiceFeePercentage,
                IdBusinessTour = model.IdBusinessTour,
                Status = model.Status
            };
            _unitOfWork.TicketTypeRepository.Update(ticketType);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
