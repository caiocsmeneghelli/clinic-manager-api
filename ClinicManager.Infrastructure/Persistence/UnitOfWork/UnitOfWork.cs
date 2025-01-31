using ClinicManager.Domain.Repositories;
using ClinicManager.Domain.UnitOfWork;
using ClinicManager.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IPatientRepository patients, IDoctorRepository doctors,
            IUserRepository users, ClinicManagerDbContext context, IMedicalAppointmentRepository medicalAppointments, IServiceRepository services)
        {
            Patients = patients;
            Doctors = doctors;
            Users = users;
            MedicalAppointments = medicalAppointments;
            Services = services;
            _context = context;
        }

        public IPatientRepository Patients { get; }

        public IDoctorRepository Doctors { get; }

        public IUserRepository Users { get; }

        public IMedicalAppointmentRepository MedicalAppointments { get; }
        public IServiceRepository Services { get; }



        public readonly ClinicManagerDbContext _context;

        private IDbContextTransaction _transaction;

        public async Task BeginTransaction()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await _transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
