using ClinicManager.Domain.Entities;
using ClinicManager.Domain.Repositories;
using ClinicManager.Domain.UnitOfWork;
using ClinicManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ClinicManagerDbContext _context;

        public ServiceRepository(ClinicManagerDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return service.Id;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services
                .SingleOrDefaultAsync(reg => reg.Id == id);
        }
    }
}
