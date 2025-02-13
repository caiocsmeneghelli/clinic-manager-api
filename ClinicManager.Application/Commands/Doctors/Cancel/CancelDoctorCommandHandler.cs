using ClinicManager.Application.Results;
using ClinicManager.Domain.UnitOfWork;
using MediatR;

namespace ClinicManager.Application.Commands.Doctors.Cancel
{
    public class CancelDoctorCommandHandler : IRequestHandler<CancelDoctorCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelDoctorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CancelDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.IdDoctor);
            if(doctor == null) { return Result.NotFound("Médico não encontrado."); }

            var medicalAppointments = await _unitOfWork.MedicalAppointments
                .GetAllByDoctor(request.IdDoctor);

            var services = await _unitOfWork.Services.GetAllByDoctor(request.IdDoctor);

            await _unitOfWork.BeginTransaction();

            doctor.Cancel();
            await _unitOfWork.CompleteAsync();

            foreach(var medicalAppointment in medicalAppointments) { medicalAppointment.Cancel(); }
            await _unitOfWork.CompleteAsync();

            foreach(var service in services) { service.Cancel(); }
            await _unitOfWork.CompleteAsync();

            await _unitOfWork.CommitAsync();

            return Result.Success("Registro do Médico cancelado.");
        }
    }
}
