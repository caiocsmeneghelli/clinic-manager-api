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

            doctor.Cancel();
            await _unitOfWork.CompleteAsync();

            return Result.Success();
        }
    }
}
