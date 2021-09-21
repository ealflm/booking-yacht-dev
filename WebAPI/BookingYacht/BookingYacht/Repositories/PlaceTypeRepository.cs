using BookingYacht.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingYacht.Repositories
{
    public class PlaceTypeRepository : IPlaceTypeRepository
    {
        private BookYachtContext bookYachtContext;

        public PlaceTypeRepository(BookYachtContext bookYachtContext)
        {
            this.bookYachtContext = bookYachtContext;
        }

        public void DeletePlaceType(Guid id)
        {
            PlaceType placeType = bookYachtContext.PlaceTypes.Find(id);
            placeType.IsDeleted = true;
            bookYachtContext.Entry<PlaceType>(placeType).State= EntityState.Modified;
        }

        public PlaceType GetPlaceTypeById(Guid id)
        {
            return bookYachtContext.PlaceTypes.Find(id);
        }

        public IEnumerable<PlaceType> GetPlaceTypes()
        {
            return  bookYachtContext.PlaceTypes.Where((placeType)=>(!placeType.IsDeleted));
        }

        public void InsertPlaceType(PlaceType placeType)
        {
            placeType.IsDeleted = false;
            bookYachtContext.PlaceTypes.Add(placeType);
        }

        public void Save()
        {
            bookYachtContext.SaveChanges();
        }

        public void UpdatePlaceType(PlaceType placeType)
        {
            bookYachtContext.Entry<PlaceType>(placeType).State= EntityState.Modified;
        }
    }
}
