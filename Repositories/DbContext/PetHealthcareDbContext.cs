using Microsoft.EntityFrameworkCore;
using PetHealthcareSystem.Mode;
using PetHealthcareSystem.Models;

public class PetHealthcareDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Veterinarian> Veterinarians { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }
    public DbSet<BookingPayment> BookingPayments { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<Cage> Cages { get; set; }
    public DbSet<AdmissionRecord> AdmissionRecords { get; set; }
    public DbSet<ServiceOrder> ServiceOrders { get; set; }
    public DbSet<ServicePayment> ServicePayments { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Service> Services { get; set; }

    //public DbSet<PetHealthTracker> PetHealthTracker { get; set; }
    public PetHealthcareDbContext(DbContextOptions<PetHealthcareDbContext> options) : base(options)
    {
        //Database.Migrate();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
