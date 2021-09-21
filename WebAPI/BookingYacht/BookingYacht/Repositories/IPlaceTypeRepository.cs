using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingYacht.Model;

namespace BookingYacht.Repositories
{
    interface IPlaceTypeRepository
    {
        IEnumerable<PlaceType> GetPlaceTypes();
        PlaceType GetPlaceTypeById(Guid id);
        void InsertPlaceType(PlaceType placeType);
        void DeletePlaceType(Guid id);
        void UpdatePlaceType(PlaceType placeType);
        void Save();
    }
}
