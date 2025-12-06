using Microsoft.EntityFrameworkCore;
using SmartCare.DAL.Data;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.DAL.Repositries.Classes
{
   public class DoctorAppointmentRepository : IDoctorAppointmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DoctorAppointmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

     
    }
}
