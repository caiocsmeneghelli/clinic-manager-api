using ClinicManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Repositories
{
    public interface IDoctorRepository
    {
        Task<int> CreateAsync(Doctor doctor);
        Task<Doctor?> GetByIdAsync(int idDoctor);
        Task<Doctor?> GetByIdAsNoTrackingAsync(int idDoctor);
        Task<Doctor?> GetByIdWithMedicalAppointmentAsync(int idDoctor);
        Task<IQueryable<Doctor>> GetAllAsync();
    }
}
