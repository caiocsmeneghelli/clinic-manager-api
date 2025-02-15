using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.Update
{
    public class UpdateDoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public UpdateDoctorCommandValidator()
        {
            RuleFor(reg => reg.CRM).NotEmpty().WithMessage("CRM não pode ser vazio.")
                .MaximumLength(13).WithMessage("CRM não pode possuir mais que 13 caracteres");
            RuleFor(reg => reg.MedicalEspeciality).NotEmpty().WithMessage("Especialidade médica não pode ser vazia.")
                .MaximumLength(128).WithMessage("Campo Especialidade Médica não pode possuir mais que 128 caracteres.");
            RuleFor(reg => reg.IdDoctor).NotEmpty().WithMessage("Id de médico não pode ser vazio.");

        }
    }
}
