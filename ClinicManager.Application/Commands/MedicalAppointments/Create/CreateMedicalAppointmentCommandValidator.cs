using FluentValidation;

namespace ClinicManager.Application.Commands.MedicalAppointments.Create
{
    public class CreateMedicalAppointmentCommandValidator : AbstractValidator<CreateMedicalAppointmentCommand>
    {
        public CreateMedicalAppointmentCommandValidator()
        {
            RuleFor(m => m.IdDoctor).NotEmpty().WithMessage("Campo de médico não pode ser vazio.");
            RuleFor(m => m.IdPatient).NotEmpty().WithMessage("Campo de paciente não pode ser vazio.");
            RuleFor(m => m.HealthPlan).NotEmpty().WithMessage("Campo de Plano de Saúde não pode ser vazio.");
            RuleFor(m => m.Service).NotEmpty().WithMessage("Campo de Serviço não pode ser vazio.");
            RuleFor(m => m.Description).NotEmpty().WithMessage("Campo de Descrição não pode ser vazio.");
            RuleFor(m => m.Cost).GreaterThan(0).WithMessage("Campo de Custo precisar ser maior que zero.");
            RuleFor(m => m.Duration).GreaterThan(0).WithMessage("Campo de Duração precisar ser maior que zero.");

            RuleFor(m => m.Start).Must(ValidateStartDate)
                .WithMessage("Data de inicio do atendimento precisa ser no futuro.");
            When(m => m.End != null, () =>
            {
                RuleFor(m => m.End)
                    .Must((mp, end) => end >= mp.Start)
                    .WithMessage("A data de encerramento do atendimento precisa ser maior que a de inicio.");
            });

        }

        private bool ValidateStartDate(DateTime start)
        {
            return start > DateTime.Now;
        }
    }
}
