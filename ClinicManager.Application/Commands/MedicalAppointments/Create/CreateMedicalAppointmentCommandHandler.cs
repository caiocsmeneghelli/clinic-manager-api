using ClinicManager.Application.Results;
using ClinicManager.Domain.Entities;
using ClinicManager.Domain.UnitOfWork;
using FluentValidation;
using MediatR;

namespace ClinicManager.Application.Commands.MedicalAppointments.Create
{
    public class CreateMedicalAppointmentCommandHandler : IRequestHandler<CreateMedicalAppointmentCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateMedicalAppointmentCommand> _validator;

        public CreateMedicalAppointmentCommandHandler(IUnitOfWork unitOfWork,
            IValidator<CreateMedicalAppointmentCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<Result> Handle(CreateMedicalAppointmentCommand request, CancellationToken cancellationToken)
        {
            List<string> errors = new List<string>();
            var validationResult = _validator.Validate(request);
            if (validationResult.IsValid == false)
            {
                errors = validationResult.Errors
                       .Select(reg => reg.ErrorMessage)
                       .ToList();
                return Result.BadRequest(errors);
            }

            if (request.End is null) { request.FillEnd(); }

            Patient? patient = await _unitOfWork.Patients.GetByIdAsync(request.IdPatient);
            Doctor? doctor = await _unitOfWork.Doctors.GetByIdAsync(request.IdDoctor);

            if (patient is null) { errors.Add("Paciente não encontrado."); }
            if (doctor is null) { errors.Add("Médico não encontrado."); }

            var appointment = await _unitOfWork.MedicalAppointments
                .GetMedicalAppointmentByDoctorAndDate(request.IdDoctor, request.Start);
            if(appointment != null) { errors.Add("Já existe um atendimento para este médico neste horário."); }

            if (errors.Count > 0) { return Result.BadRequest(errors); }

            Service service = new Service(request.Service, request.Description,
                request.Cost, request.Duration);

            try
            {
                await _unitOfWork.BeginTransaction();

                await _unitOfWork.Services.CreateAsync(service);
                await _unitOfWork.CompleteAsync();

                MedicalAppointment medicalAppointment = new MedicalAppointment(patient.Id, doctor.Id,
                    service.Id, request.HealthPlan, request.Start, request.End.Value);
                await _unitOfWork.MedicalAppointments.CreateAsync(medicalAppointment);
                await _unitOfWork.CompleteAsync();

                await _unitOfWork.CommitAsync();
                return Result.Success(medicalAppointment);
            }
            catch (Exception ex)
            {
                return Result.BadRequest("Falha ao salvar atendimento médico.");
            }
        }
    }
}
