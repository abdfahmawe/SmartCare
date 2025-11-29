using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
using SmartCare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Utils
{
   public class DataSeed : IDataSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataSeed(ApplicationDbContext context , RoleManager<IdentityRole> _roleManager , UserManager<ApplicationUser> _userManager)
        {
            _context = context;
            this._roleManager = _roleManager;
            this._userManager = _userManager;
        }

        public async Task DataSeedingAsync()
        {
            await SeedDepartments();
            await SeedDoctors();
            await SeedPatients();
            await SeedAppointments();
        }
        private async Task SeedDepartments()
        {
            if (await _context.Departments.AnyAsync()) return;

            var cardiology = new Department
            {
                Name = "Cardiology",
                Description = "Heart and cardio-related treatments."
            };

            var dermatology = new Department
            {
                Name = "Dermatology",
                Description = "Skin treatments and diagnosis."
            };

           await _context.Departments.AddRangeAsync(cardiology, dermatology);
            await _context.SaveChangesAsync();
        }

        // =============================================== 
        // ===============  SEED DOCTORS  =================
        private async Task SeedDoctors()
        {
            if (await _context.Doctors.AnyAsync()) return;

            var doctorUser = await _userManager.FindByNameAsync("drmajdi");
            var department = await _context.Departments.FirstAsync();

            var doctor = new Doctor
            {
                Id = doctorUser.Id,
                City = "Ramallah",
                Specialization = "Cardiologist",
                PhoneNumber = "0568837223",
                EmergencyPhone = "0599111111",
                DepartmentId = department.Id,
               
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            // Working Time
            foreach (DayOfWeek d in Enum.GetValues(typeof(DayOfWeek)))
            {
               await _context.WorkingTimes.AddAsync(new WorkingTime
                {
                    DoctorId = doctor.Id,
                    Day = d,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndTime = new TimeSpan(17, 0, 0)
                });
            }

            await _context.SaveChangesAsync();
        }

        // =============================================== 
        // ===============  SEED PATIENTS  ===============
        private async Task SeedPatients()
        {
            if (await _context.Patients.AnyAsync()) return;

            var patientUser = await _userManager.FindByEmailAsync("baraajetawi@gmail.com");

            var patient = new Patient
            {
                Id = patientUser.Id,
                PhoneNumber = "0599555522",
                EmergencyPhone = "0599112233",
                Address = "Nablus",
                Gendar = Gendar.Boy,
                BirthDate = new DateTime(2004,1,1),
                BloodType = BloodType.A_Positive ,
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        // =============================================== 
        // ===============  SEED APPOINTMENTS ============
        private async Task SeedAppointments()
        {
            if (await _context.Appointments.AnyAsync()) return;

            var doctor = await _context.Doctors.FirstAsync();
            var patient = await _context.Patients.FirstAsync();

            // Appointment
            var appointment = new Appointment
            {
                Id = Guid.NewGuid().ToString(),
                DoctorId = doctor.Id,
                PatientId = patient.Id,
                Status = AppointmentStatus.Completed,
                StartAt = DateTime.Now.AddDays(-1).AddHours(10),
                EndAt = DateTime.Now.AddDays(-1).AddHours(10).AddMinutes(30),
                DurationMinutes = 30
            };
           await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();

            // Medical Record
            var record = new MedicalRecord
            {
                Id = Guid.NewGuid().ToString(),
                AppointmentId = appointment.Id,
                Diagnosis = "High blood pressure",
                Symptoms = "Headache, dizziness",
                Notes = "Patient needs regular monitoring.",
                VitalSigns = "BP: 150/95, Pulse: 88",
                TestsNeeded = "Blood test",
                Allergies = "None"
            };
           await _context.MedicalRecords.AddAsync(record);

            // Prescription
            var prescription = new Prescription
            {
                Id = Guid.NewGuid().ToString(),
                AppointmentId = appointment.Id,
                MedicineName = "Panadol",
                Dosage = "500mg",
                Frequency = "3 times a day",
                DurationDays = 5,
                Instructions = "Take after meals."
            };
            await _context.Prescriptions.AddAsync(prescription);

            // Medical File
            var file = new MedicalFile
            {
                Id = Guid.NewGuid().ToString(),
                AppointmentId = appointment.Id,
                FileUrl = "/images/test-result.jpg",
                FileType = "image/jpeg",
                Description = "Blood test result"
            };
            await _context.MedicalFiles.AddAsync(file);

            // Invoice
            var invoice = new Invoice
            {
                Id = Guid.NewGuid().ToString(),
                AppointmentId = appointment.Id,
                Amount = 150,
                PaymentWay = PaymentWay.Cash,
                PaymentStatus = PaymentStatus.Approved,
                IsPaid = true
            };
            await _context.Invoices.AddAsync(invoice);

            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if(!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("Doctor"));
                await _roleManager.CreateAsync(new IdentityRole("Patient"));
            }
            if(!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    FullName = "System Administrator",
                    UserName = "admin",
                    Email = "fahmawe09@gmail.com",
                    EmailConfirmed = true
                };
                var user2 = new ApplicationUser()
                {
                    FullName = "Dr. Majdi Yakoub",
                    UserName = "drmajdi",
                    Email = "majdiyakoub@gmail.com",
                    EmailConfirmed = true
                };
                var user3 = new ApplicationUser()
                {
                    FullName = "Baraa Jetawi",
                    UserName = "baraa",
                    Email = "baraajetawi@gmail.com",
                    EmailConfirmed = true
                };
                // Create Users
                await _userManager.CreateAsync(user1 , "Pass@1212");
                await _userManager.CreateAsync(user2, "Pass@1212");
                await _userManager.CreateAsync(user3, "Pass@1212");
                // Assign Roles
                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "Doctor");
                await _userManager.AddToRoleAsync(user3, "Patient");
            }
            await _context.SaveChangesAsync();
        }
    }
}
