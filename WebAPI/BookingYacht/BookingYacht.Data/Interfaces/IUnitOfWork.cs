using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace BookingYacht.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Admin> AdminRepository { get; }
        IGenericRepository<Agency> AgencyRepository { get; }
        IGenericRepository<Business> BusinessRepository { get; }
        IGenericRepository<BusinessTour> BusinessTourRepository { get; }
        IGenericRepository<Destination> DestinationRepository { get; }
        IGenericRepository<DestinationTour> DestinationTourRepository { get; }
        IGenericRepository<Member> MemberRepository { get; }
        IGenericRepository<Order> OrderRepository { get; }
        IGenericRepository<PlaceType> PlaceTypeRepository { get; }
        IGenericRepository<Ticket> TicketRepository { get; }
        IGenericRepository<TicketType> TicketTypeRepository { get; }
        IGenericRepository<Tour> TourRepository { get; }
        IGenericRepository<Trip> TripRepository { get; }
        IGenericRepository<Vehicle> VehicleRepository { get; }
        IGenericRepository<VehicleType> VehicleTypeRepository { get; }

        DatabaseFacade Database();

        Task SaveChangesAsync();
    }
}
