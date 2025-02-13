using ClinicManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Domain.Repositories
{
    public interface IMedicalAppointmentRepository
    {
        Task<MedicalAppointment?> GetMedicalAppointmentByDoctorAndDate(int doctorId, DateTime date);
        Task<List<MedicalAppointment>> GetAll();
        Task<List<MedicalAppointment>> GetAllByDoctor(int doctorId);
        Task<List<MedicalAppointment>> GetAllByDoctorAsNoTracking(int doctorId);
        Task<List<MedicalAppointment>> GetAllByPatient(int patientId);
        Task<MedicalAppointment?> GetMedicalAppointmentByIdAsync(int id);
        Task<int> CreateAsync(MedicalAppointment entity);
    }
}
