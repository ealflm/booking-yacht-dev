﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BookingYacht.Data.Models;

#nullable disable

namespace BookingYacht.Data.Context
{
    public partial class BookingYachtContext : DbContext
    {
        public BookingYachtContext()
        {
        }

        public BookingYachtContext(DbContextOptions<BookingYachtContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessTour> BusinessTours { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<DestinationTour> DestinationTours { get; set; }
        public virtual DbSet<Member> Members { get; set; }
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
#pragma warning disable CS1030 // #warning directive
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=booking-yacht-dev.database.windows.net;Database=BookingYacht;User Id=swd391gr5;Password=Password@3915");
#pragma warning restore CS1030 // #warning directive
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<Agency>(entity =>
            {
                entity.ToTable("Agency");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BusinessTour>(entity =>
            {
                entity.ToTable("BusinessTour");

                entity.HasIndex(e => e.IdBusiness, "IX_BusinessTour_IdBusiness");

                entity.HasIndex(e => e.IdTour, "IX_BusinessTour_IdTour");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.IdBusinessNavigation)
                    .WithMany(p => p.BusinessTours)
                    .HasForeignKey(d => d.IdBusiness)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BusinessT__IdBus__787EE5A0");

                entity.HasOne(d => d.IdTourNavigation)
                    .WithMany(p => p.BusinessTours)
                    .HasForeignKey(d => d.IdTour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BusinessT__IdTou__7A672E12");
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.ToTable("Destination");

                entity.HasIndex(e => e.IdPlaceType, "IX_Destination_IdPlaceType");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.IdPlaceTypeNavigation)
                    .WithMany(p => p.Destinations)
                    .HasForeignKey(d => d.IdPlaceType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Destinati__IdPla__656C112C");
            });

            modelBuilder.Entity<DestinationTour>(entity =>
            {
                entity.ToTable("DestinationTour");

                entity.HasIndex(e => e.IdPier, "IX_DestinationTour_IdPier");

                entity.HasIndex(e => e.IdTour, "IX_DestinationTour_IdTour");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.IdPierNavigation)
                    .WithMany(p => p.DestinationTours)
                    .HasForeignKey(d => d.IdPier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Destinati__IdPie__693CA210");

                entity.HasOne(d => d.IdTourNavigation)
                    .WithMany(p => p.DestinationTours)
                    .HasForeignKey(d => d.IdTour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Destinati__IdTou__6A30C649");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.IdAgency, "IX_Orders_IdAgency");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.AgencyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdAgencyNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdAgency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Orders__IdAgency__4D94879B");
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

                entity.HasIndex(e => e.IdOrder, "IX_Ticket_IdOrder");

                entity.HasIndex(e => e.IdTicketType, "IX_Ticket_IdTicketType");

                entity.HasIndex(e => e.IdTrip, "IX_Ticket_IdTrip");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.NameCustomer)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdOrder)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__IdOrder__72C60C4A");

                entity.HasOne(d => d.IdTicketTypeNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTicketType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__IdTicket__73BA3083");

                entity.HasOne(d => d.IdTripNavigation)
                    .WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.IdTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__IdTrip__74AE54BC");
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.ToTable("TicketType");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.IdBusinessTourNavigation)
                    .WithMany(p => p.TicketTypes)
                    .HasForeignKey(d => d.IdBusinessTour)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TicketType_BusinessTour");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.ToTable("Tour");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descriptions).HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.ToTable("Trip");

                entity.HasIndex(e => e.IdBusiness, "IX_Trip_IdBusiness");

                entity.HasIndex(e => e.IdVehicle, "IX_Trip_IdVehicle");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Time)
                    .IsRequired()
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdBusinessNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.IdBusiness)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trip__IdBusiness__6E01572D");

                entity.HasOne(d => d.IdVehicleNavigation)
                    .WithMany(p => p.Trips)
                    .HasForeignKey(d => d.IdVehicle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Trip__IdVehicle__6EF57B66");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.HasIndex(e => e.IdBusiness, "IX_Vehicle_IdBusiness");

                entity.HasIndex(e => e.IdVehicleType, "IX_Vehicle_IdVehicle");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Descriptions)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdBusinessNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.IdBusiness)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vehicle__IdBusin__5EBF139D");

                entity.HasOne(d => d.IdVehicleTypeNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.IdVehicleType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Vehicle__IdVehic__5DCAEF64");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VehicleType");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
