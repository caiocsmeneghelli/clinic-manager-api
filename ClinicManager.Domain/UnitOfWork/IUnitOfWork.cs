using ClinicManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        // repositories
        IPatientRepository Patients { get; }
        IDoctorRepository Doctors { get; }
        IUserRepository Users { get; }
        IMedicalCareRepository MedicalCares { get; }


        Task<int> CompleteAsync();
        Task BeginTransaction();
        Task CommitAsync();
    }
}
