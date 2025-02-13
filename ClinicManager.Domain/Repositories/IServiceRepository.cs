using ClinicManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Repositories
{
    public interface IServiceRepository
    {
        Task<int> CreateAsync(Service service);
        Task<Service?> GetByIdAsync(int id);
        Task<List<Service>> GetAllByIdDoctor(int id);
        Task<List<Service>> GetAllAsync();
    }
}
