using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Data
{
    public class ApplicationDbContext
    : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
        {
        }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalFile> MedicalFiles { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<WorkingTime> WorkingTimes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>()
.HasOne(a => a.Patient)
.WithMany(p => p.Appointments)
.HasForeignKey(a => a.PatientId)
.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany(d => d.Appointments)
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Invoice)
            .WithOne(i => i.Appointment)
            .HasForeignKey<Invoice>(i => i.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
            .HasOne(a => a.MedicalRecord)
            .WithOne(m => m.Appointment)
            .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkingTime>()
            .HasOne(w => w.Doctor)
            .WithMany(d => d.WorkingTimes)
            .HasForeignKey(w => w.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

            //identity configurations
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            //ignore
            modelBuilder.Ignore<IdentityUserClaim<string>>();
            modelBuilder.Ignore<IdentityUserLogin<string>>();
            modelBuilder.Ignore<IdentityUserToken<string>>();
            modelBuilder.Ignore<IdentityRoleClaim<string>>();

        }

    }
}
