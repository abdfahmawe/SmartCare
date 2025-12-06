using Microsoft.EntityFrameworkCore;
using SmartCare.BLL.Services.Interfaces;
using SmartCare.DAL.Data;
using SmartCare.DAL.DTO.Responses;
using SmartCare.DAL.Models;
using SmartCare.DAL.Repositries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCare.BLL.Services.Classes
{
   public class DoctorAppointmentService : IDoctorAppointmentService
    {
        private readonly IDoctorAppointmentRepository _repo;
        public DoctorAppointmentService(IDoctorAppointmentRepository repo)
        {
            _repo = repo;
        }
    }
}
