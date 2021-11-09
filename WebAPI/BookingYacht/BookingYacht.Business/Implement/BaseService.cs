using BookingYacht.Business.Interfaces;
using BookingYacht.Data.Interfaces;

namespace BookingYacht.Business.Implement
{
    public class BaseService : IBaseService
    {
        protected readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}