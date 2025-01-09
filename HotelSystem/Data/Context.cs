using HotelSystem.Models;
using HotelSystem.Models.GuestMenagment;
using HotelSystem.Models.PaymentProcessing;
using HotelSystem.Models.ReservationManagement;
using HotelSystem.Models.RoomManagement;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HotelSystem.Data;

public class Context(IConfiguration config) : DbContext
{
    private readonly IConfiguration config = config;
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<ReservationRoom> ReservationRooms { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Facility> Facilities{ get; set; }
    public DbSet<RoomFacility> RoomFacilities{ get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
