using BookingYacht.Data.Context;
using BookingYacht.Data.Interfaces;
using BookingYacht.Data.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace BookingYacht.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingYachtContext _dbContext;

        public IGenericRepository<Admin> AdminRepository { get; }
        public IGenericRepository<Agency> AgencyRepository { get; }
        public IGenericRepository<Business> BusinessRepository { get; }
        public IGenericRepository<BusinessTour> BusinessTourRepository { get; }
        public IGenericRepository<Destination> DestinationRepository { get; }
        public IGenericRepository<DestinationTour> DestinationTourRepository { get; }
        public IGenericRepository<Member> MemberRepository { get; }
        public IGenericRepository<Order> OrderRepository { get; }
        public IGenericRepository<PlaceType> PlaceTypeRepository { get; }
        public IGenericRepository<Ticket> TicketRepository { get; }
        public IGenericRepository<TicketType> TicketTypeRepository { get; }
        public IGenericRepository<Tour> TourRepository { get; }
        public IGenericRepository<Trip> TripRepository { get; }
        public IGenericRepository<Vehicle> VehicleRepository { get; }
        public IGenericRepository<VehicleType> VehicleTypeRepository { get; }

        public UnitOfWork(BookingYachtContext dbContext,
                          IGenericRepository<Admin> adminRepository,
                          IGenericRepository<Agency> agencyRepository,
                          IGenericRepository<Business> businessRepository,
                          IGenericRepository<BusinessTour> businessTourRepository,
                          IGenericRepository<Destination> destinationRepository,
                          IGenericRepository<DestinationTour> destinationTourRepository,
                          IGenericRepository<Member> memberRepository,
                          IGenericRepository<Order> orderRepository,
                          IGenericRepository<PlaceType> placeTypeRepository,
                          IGenericRepository<Ticket> ticketRepository,
                          IGenericRepository<TicketType> ticketTypeRepository,
                          IGenericRepository<Tour> tourRepository,
                          IGenericRepository<Trip> tripRepository,
                          IGenericRepository<Vehicle> vehicleRepository,
                          IGenericRepository<VehicleType> vehicleTypeRepository)
        {
            _dbContext = dbContext;

            AdminRepository = adminRepository;
            AgencyRepository = agencyRepository;
            BusinessRepository = businessRepository;
            BusinessTourRepository = businessTourRepository;
            DestinationRepository = destinationRepository;
            DestinationTourRepository = destinationTourRepository;
            MemberRepository = memberRepository;
            OrderRepository = orderRepository;
            PlaceTypeRepository = placeTypeRepository;
            TicketRepository = ticketRepository;
            TicketTypeRepository = ticketTypeRepository;
            TourRepository = tourRepository;
            TripRepository = tripRepository;
            VehicleRepository = vehicleRepository;
            VehicleTypeRepository = vehicleTypeRepository;
        }

        public DatabaseFacade Database()
        {
            return _dbContext.Database;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

    }
}
