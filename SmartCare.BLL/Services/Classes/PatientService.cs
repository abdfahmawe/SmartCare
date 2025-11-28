using Mapster;
using SmartCare.BLL.Services.Interfaces;
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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepositry _patientRepositry;

        public PatientService(IPatientRepositry patientRepositry)
        {
            _patientRepositry = patientRepositry;
        }
        public async Task<PatientProfileResponse> GetPatientByIdAsync(string UserId)
        {
            var result = await _patientRepositry.GetPaitentByIdAsync(UserId);
            var resultAfterAdapt = result.Adapt<PatientProfileResponse>();
            return resultAfterAdapt;
        }
    }
}
