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
    public class MedicalCareRepository : IMedicalCareRepository
    {
        private readonly ClinicManagerDbContext _context;

        public MedicalCareRepository(ClinicManagerDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(MedicalCare entity)
        {
            await _context.MedicalCares.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<List<MedicalCare>> GetAll()
        {
            return await _context.MedicalCares
                .Include(reg => reg.Doctor)
                .Include(reg => reg.Patient)
                .Include(reg => reg.Service)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<MedicalCare?> GetMedicalCareByIdAsync(int id)
        {
            return await _context.MedicalCares
                .SingleOrDefaultAsync(reg => reg.Id == id);
        }
    }
}
