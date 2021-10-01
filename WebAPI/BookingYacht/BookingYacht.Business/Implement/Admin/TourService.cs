﻿using System;
using BookingYacht.Business.Interfaces.Admin;
using BookingYacht.Business.SearchModels;
using BookingYacht.Business.ViewModels;
using BookingYacht.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BookingYacht.Data.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingYacht.Business.Implement.Admin
{
    public class TourService : BaseService, ITourService
    {

        public TourService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Guid> AddTour(TourViewModel model)
        {
            var tour = new Tour()
            {
                Id = model.Id,
                Title= model.Tittle,
                Descriptions= model.Descriptions,
                Status = model.Status
            };
            tour.Status = (int)Status.ENABLE;
            await _unitOfWork.TourRepository.Add(tour);
            await _unitOfWork.SaveChangesAsync();
            return tour.Id;
        }

        public async Task DeleteTour(Guid id)
        {
            var tour = await _unitOfWork.TourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new Tour()
                {
                    Id = x.Id,
                   Title=x.Title,
                   Descriptions= x.Descriptions,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            tour.Status = (int)Status.DISABLE;
            _unitOfWork.TourRepository.Update(tour);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TourViewModel> GetTour(Guid id)
        {
            var tour = await _unitOfWork.TourRepository.Query()
                .Where(x => x.Id.Equals(id))
                .Select(x => new TourViewModel()
                {
                    Id = x.Id,
                    Tittle= x.Title,
                    Descriptions= x.Descriptions,
                    Status = x.Status
                }).FirstOrDefaultAsync();
            return tour;
        }

        public async Task<List<TourViewModel>> SearchTours(TourSearchModel model = null)
        {
            if (model == null)
            {
                model = new TourSearchModel();
            }
            var tours = await _unitOfWork.TourRepository.Query()
                .Where(x => model.Tittle == null | x.Title.Contains(model.Tittle))
                .Where(x => model.Descriptions == null | x.Descriptions.Contains(model.Descriptions))
                .Where(x => model.Status == Status.ALL | x.Status == (int)model.Status)
                .Select(x => new TourViewModel()
                {
                    Id = x.Id,
                    Tittle= x.Title,
                    Descriptions= x.Descriptions,
                    Status = x.Status
                })
                .OrderBy(x => x.Tittle)
                .Skip(model.AmountItem * ((model.Page != 0) ? (model.Page - 1) : model.Page))
                .Take((model.Page != 0) ? model.AmountItem : _unitOfWork.BusinessRepository.Query().Count())
                .ToListAsync();
            return tours;
        }

        public async Task UpdateTour(Guid id, TourViewModel model)
        {
            var tour = new Tour()
            {
                Id = id,
                Title= model.Tittle,
                Descriptions= model.Descriptions,
                Status = model.Status
            };
            _unitOfWork.TourRepository.Update(tour);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
