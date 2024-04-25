using Microsoft.EntityFrameworkCore;
using Project.Api.Models;
using System.Data;
using System.Drawing;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Clinics> Clinics { get; set; }
    public DbSet<Appointments> Appointments { get; set; }
    public DbSet<AppointmentHistory> AppointmentHistory { get; set; }
    public DbSet<Doctors> Doctors { get; set; }
    public DbSet<Services> Services { get; set; }
    public DbSet<Specialties> Specialties { get; set; }
    public DbSet<MedicalRecords> MedicalRecords { get; set; }




}
