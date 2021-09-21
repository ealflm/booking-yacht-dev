using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BookingYacht.Model
{
    public partial class BookYachtContext : DbContext
    {
        public BookYachtContext()
        {
        }

        public BookYachtContext(DbContextOptions<BookYachtContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessTour> BusinessTours { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<DestinationTour> DestinationTours { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PlaceType> PlaceTypes { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=booking-yacht-dev.eastasia.cloudapp.azure.com;Database=BookYacht;User Id=swd391gr5;Password=swd391@team5;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Agency>(entity =>
            {
                entity.ToTable("Agency");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BusinessTour>(entity =>
            {
                entity.ToTable("BusinessTour");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.IdBusinessNavigation)
                    .WithMany(p => p.BusinessTours)
                    .HasForeignKey(d => d.IdBusiness)
                    .HasConstraintName("FK__BusinessT__IdBus__534D60F1");

                entity.HasOne(d => d.IdTicketTypeNavigation)
                    .WithMany(p => p.BusinessTours)
                    .HasForeignKey(d => d.IdTicketType)
                    .HasConstraintName("FK__BusinessT__IdTic__5441852A");

                entity.HasOne(d => d.IdTourNavigation)
                    .WithMany(p => p.BusinessTours)
                    .HasForeignKey(d => d.IdTour)
                    .HasConstraintName("FK__BusinessT__IdTou__5535A963");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.ToTable("Destination");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPlaceTypeNavigation)
                    .WithMany(p => p.Destinations)
                    .HasForeignKey(d => d.IdPlaceType)
                    .HasConstraintName("FK__Destinati__IdPla__403A8C7D");
            });

            modelBuilder.Entity<DestinationTour>(entity =>
            {
                entity.ToTable("DestinationTour");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.IdPierNavigation)
                    .WithMany(p => p.DestinationTours)
                    .HasForeignKey(d => d.IdPier)
                    .HasConstraintName("FK__Destinati__IdPie__440B1D61");

                entity.HasOne(d => d.IdTourNavigation)
                    .WithMany(p => p.DestinationTours)
                    .HasForeignKey(d => d.IdTour)
                    .HasConstraintName("FK__Destinati__IdTou__44FF419A");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgencyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAgencyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdAgency)
                    .HasConstraintName("FK__Orders__IdAgency__286302EC");
            });

            modelBuilder.Entity<PlaceType>(entity =>
            {
                entity.ToTable("PlaceType");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NameCustomer)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK__Ticket__IdOrder__4D94879B");

                entity.HasOne(d => d.IdTicketTypeNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTicketType)
                    .HasConstraintName("FK__Ticket__IdTicket__4E88ABD4");

                entity.HasOne(d => d.IdTripNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTrip)
                    .HasConstraintName("FK__Ticket__IdTrip__4F7CD00D");
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.ToTable("TicketType");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.IdTourNavigation)
                    .WithMany(p => p.TicketTypes)
                    .HasForeignKey(d => d.IdTour)
                    .HasConstraintName("FK__TicketTyp__IdTou__31EC6D26");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdBusinessNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.IdBusiness)
                    .HasConstraintName("FK__Trip__IdBusiness__48CFD27E");

                entity.HasOne(d => d.IdVehicleNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.IdVehicle)
                    .HasConstraintName("FK__Trip__IdVehicle__49C3F6B7");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdBusinessNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.IdBusiness)
                    .HasConstraintName("FK__Vehicle__IdBusin__398D8EEE");

                entity.HasOne(d => d.IdVehicleNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.IdVehicle)
                    .HasConstraintName("FK__Vehicle__IdVehic__38996AB5");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VehicleType");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Size)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
