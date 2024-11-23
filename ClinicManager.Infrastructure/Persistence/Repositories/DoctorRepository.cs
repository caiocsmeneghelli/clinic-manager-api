using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        public Task<int> CreateAsync(Doctor doctor)
        {
            throw new NotImplementedException();
        }

        public Task<List<Doctor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Doctor?> GetByIdAsNoTrackingAsync(int idDoctor)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor?> GetByIdAsync(int idDoctor)
        {
            throw new NotImplementedException();
        }

        public Task<Doctor?> GetByIdWithMedicalCareAsync(int idDoctor)
        {
            throw new NotImplementedException();
        }
    }
}
