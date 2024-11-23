using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public Task<int> CreateAsync(Patient patient)
        {
            throw new NotImplementedException();
        }

        public Task<List<Doctor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Patient?> GetByIdAsNoTrackingAsync(int idPatient)
        {
            throw new NotImplementedException();
        }

        public Task<Patient?> GetByIdAsync(int idPatient)
        {
            throw new NotImplementedException();
        }

        public Task<Patient?> GetByIdWithMedicalCareAsync(int idPatient)
        {
            throw new NotImplementedException();
        }
    }
}
