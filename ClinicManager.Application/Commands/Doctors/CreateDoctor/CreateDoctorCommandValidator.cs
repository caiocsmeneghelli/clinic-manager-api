using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicManager.Application.Commands.Doctors.CreateDoctor
{
    public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Nome não pode ser vazio.")
                .MaximumLength(128).WithMessage("Nome não pode ter mais de 128 caracteres.");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Nome não pode ser vazio.")
                .MaximumLength(128).WithMessage("Nome não pode ter mais de 128 caracteres.");
        }
    }
}
