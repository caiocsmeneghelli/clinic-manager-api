using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using ClinicManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class MedicalAppointmentRepository : IMedicalAppointmentRepository
    {
        private readonly ClinicManagerDbContext _context;

        public MedicalAppointmentRepository(ClinicManagerDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(MedicalAppointment entity)
        {
            await _context.MedicalAppointments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<List<MedicalAppointment>> GetAll()
        {
            return await _context.MedicalAppointments
                .Include(reg => reg.Doctor)
                .Include(reg => reg.Patient)
                .Include(reg => reg.Service)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<MedicalAppointment>> GetAllByDoctor(int doctorId)
        {
            return await _context.MedicalAppointments
                .Where(reg => reg.IdDoctor == doctorId)
                .Include(reg => reg.Doctor)
                .Include(reg => reg.Patient)
                .Include(reg => reg.Service)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<MedicalAppointment>> GetAllByPatient(int patientId)
        {
            return await _context.MedicalAppointments
                .Where(reg => reg.IdPatient == patientId)
                .Include(reg => reg.Doctor)
                .Include(reg => reg.Patient)
                .Include(reg => reg.Service)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MedicalAppointment?> GetMedicalAppointmentByIdAsync(int id)
        {
            return await _context.MedicalAppointments
                .SingleOrDefaultAsync(reg => reg.Id == id);
        }

        public async Task<MedicalAppointment?> GetMedicalAppointmentByDoctorAndDate(int doctorId, DateTime date)
        {
            return await _context.MedicalAppointments
                .Where(reg => reg.IdDoctor == doctorId)
                .SingleOrDefaultAsync(reg => reg.Start == date);
        }

    }
}
