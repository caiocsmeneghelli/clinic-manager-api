using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using ClinicManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ClinicManagerDbContext _context;

        public PatientRepository(ClinicManagerDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            return patient.Id;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients
                .Include(p => p.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Patient?> GetByIdAsNoTrackingAsync(int idPatient)
        {
            return await _context
                .Patients
                .AsNoTracking()
                .SingleOrDefaultAsync(reg => reg.Id == idPatient);
        }

        public async Task<Patient?> GetByIdAsync(int idPatient)
        {
            return await _context
                .Patients
                .SingleOrDefaultAsync(reg => reg.Id == idPatient);
        }

        public async Task<Patient?> GetByIdWithMedicalCareAsync(int idPatient)
        {
            return await _context
                 .Patients
                 .SingleOrDefaultAsync(reg => reg.Id == idPatient);
        }
    }
}
