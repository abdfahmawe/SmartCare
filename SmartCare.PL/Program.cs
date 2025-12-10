
using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
using Scalar.AspNetCore;
using SmartCare.DAL.Utils;
using SmartCare.DAL.Models;
using Microsoft.AspNetCore.Identity;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.BLL.Services.Classes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using SmartCare.PL.Helpers;
using SmartCare.DAL.Repositries.Interfaces;
using SmartCare.DAL.Repositries.Classes;
using SmartCare.BLL.BackgroundJobs;

namespace SmartCare.PL
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString =
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string"
                    + "'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IDataSeed, DataSeed>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IPatientRepositry, PatientRepositry>();
            builder.Services.AddScoped<IPatientAppointmentService, PatientAppointmentService>();
            builder.Services.AddScoped<IPatientAppointmentRepositry, PatientAppointmentRepositry>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IDoctorRepositry, DoctorRepositry>();
            builder.Services.AddScoped<IDoctorWorkingTimeService, DoctorWorkingTimeService>();
            builder.Services.AddScoped<IDoctorWorkingTimeRepositry, DoctorWorkingTimeRepositry>();
            builder.Services.AddScoped<IDoctorAppointmentRepository, DoctorAppointmentRepository>();
            builder.Services.AddScoped<IDoctorAppointmentService, DoctorAppointmentService>();
            builder.Services.AddScoped<IDoctorMedicalRecordRepositry, DoctorMedicalRecordRepositry>();
            builder.Services.AddScoped<IDoctorMedicalRecordservices, DoctorMedicalRecordservices>();
            //
            builder.Services.AddHostedService<AppointmentStatusChecker>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            //
            builder.Services.AddScoped<IAuthenticationUser , AuthenticationUser>();
            builder.Services.AddScoped<IEmailSender,EmailSetting>();

            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.AddControllers();

          
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                           
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions")["SecretKey"]))
                        };
                    });
           


            builder.Services.AddOpenApi();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dataSeed = scope.ServiceProvider.GetRequiredService<IDataSeed>();
                await dataSeed.IdentityDataSeedingAsync();
                await dataSeed.DataSeedingAsync();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();  

            }

            app.UseHttpsRedirection();
        

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
