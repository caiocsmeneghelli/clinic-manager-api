using ClinicManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<int> CreateAsync(Patient patient);
        Task<Patient?> GetByIdAsync(int idPatient);
        Task<Patient?> GetByIdAsNoTrackingAsync(int idPatient);
        Task<Patient?> GetByIdWithMedicalCareAsync(int idPatient);
        Task<List<Doctor>> GetAllAsync();
    }
}
