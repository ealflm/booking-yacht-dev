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
    public class BusinessTourService : BaseService, IBusinessTourService
    {
        public BusinessTourService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<BusinessTourViewModel>> SearchAgenciesNavigation(BusinessTourSearchModel model = null)
        {
            model ??= new BusinessTourSearchModel();
            var list = await _unitOfWork.BusinessTourRepository.Query()
                .Include(x => x.IdBusinessNavigation)
                .Include(x => x.IdBusinessNavigation)
                .Where(x => model == null | x.IdBusiness.Equals(model.idBusiness))
                .Where(x => model == null | x.IdTour.Equals(model.idTour))
                .Where(x => model == null | x.Status == (int) model.Status)
                .OrderBy(x => x.Status)
                .Skip(model.AmountItem * (model.Page != 0 ? model.Page - 1 : 0))
                .Take(model.Page != 0 ? model.AmountItem : _unitOfWork.BusinessRepository.Query().Count())
                .Select(x => new BusinessTourViewModel()
                {
                    id = x.Id,
                    idBusiness = x.IdBusiness,
                    idTour = x.IdTour,
                    Status = x.Status 
                })
                .ToListAsync();
            return list;
        }

        public async Task<BusinessTourViewModel> GetBusinessTour(Guid id)
        {
            return await _unitOfWork.BusinessTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new BusinessTourViewModel()
                {
                    id = x.Id,
                    idBusiness = x.IdBusiness,
                    idTour = x.IdTour,
                    Status = x.Status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> AddBusinessTour(BusinessTourViewModel model)
        {
            var businessTour = _unitOfWork.BusinessTourRepository.Query().Add(new BusinessTour()
            {
                Id = model.id,
                IdBusiness = model.idBusiness,
                IdTour = model.idTour,
                Status = model.Status
            });
            await _unitOfWork.SaveChangesAsync();
            return businessTour.Entity.Id;
        }

        public async Task<bool> UpdateBusinessTour(Guid id, BusinessTourViewModel model)
        {
            var result = await _unitOfWork.BusinessTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }

            result.Status = model.Status;
            result.IdBusiness = model.idBusiness;
            result.IdTour = model.idTour;
            _unitOfWork.BusinessTourRepository.Update(result);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBusinessTour(Guid id)
        {
            var result = await _unitOfWork.BusinessTourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            result.Status = (int)Status.DISABLE;
            _unitOfWork.BusinessTourRepository.Update(result);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}