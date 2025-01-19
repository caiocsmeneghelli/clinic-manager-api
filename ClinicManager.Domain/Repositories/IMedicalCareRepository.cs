using ClinicManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Repositories
{
    public interface IMedicalCareRepository
    {
        Task<List<MedicalCare>> GetAll();
        Task<List<MedicalCare>> GetAllByDoctor(int doctorId);
        Task<List<MedicalCare>> GetAllByPatient(int patientId);
        Task<MedicalCare?> GetMedicalCareByIdAsync(int id);
        Task<int> CreateAsync(MedicalCare entity);
    }
}
