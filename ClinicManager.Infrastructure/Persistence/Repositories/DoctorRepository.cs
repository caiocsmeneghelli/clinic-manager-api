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
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ClinicManagerDbContext _context;

        public DoctorRepository(ClinicManagerDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            return doctor.Id;
        }

        public async Task<IQueryable<Doctor>> GetAllAsync()
        {
            return _context.Doctors
                .AsQueryable();
        }

        public async Task<Doctor?> GetByIdAsNoTrackingAsync(int idDoctor)
        {
            return await _context.Doctors
                .AsNoTracking()
                .SingleOrDefaultAsync(reg => reg.Id == idDoctor);
        }

        public async Task<Doctor?> GetByIdAsync(int idDoctor)
        {
            return await _context.Doctors
                .SingleOrDefaultAsync(reg => reg.Id == idDoctor);
        }

        public async Task<Doctor?> GetByIdWithMedicalAppointmentAsync(int idDoctor)
        {
            // Add medical Care
            return await _context.Doctors
                .Include(reg => reg.MedicalAppointments)
                .SingleOrDefaultAsync(reg => reg.Id == idDoctor);
        }
    }
}
